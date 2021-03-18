using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Seshat.Module;
using UnityEngine;

namespace Seshat
{
    public static class SeshatLoader
    {
        public const string ModMetaFile = "seshat.toml";

        public static string ModsPath => Path.Combine(Application.dataPath, "Mods/");

        /// <summary>
        /// Loads all mods from the <see cref="SeshatLoader.ModsPath"/>.
        /// </summary>
        internal static void LoadAuto()
            => LoadAll(ModsPath);

        /// <summary>
        /// Loads all mods from a directory.
        /// </summary>
        /// <param name="modsPath">The absolute path of the directory of mods.</param>
        internal static void LoadAll(string modsPath)
        {
            DirectoryInfo modsDir = new DirectoryInfo(modsPath);

            if (!modsDir.Exists)
                modsDir.Create();

            foreach (var dir in modsDir.GetDirectories())
            {
                try { LoadMod(dir.FullName); }
                catch (Exception e) 
                {
                    Logger.Error("loader", "An error occured during mod loading!");
                    e.LogException();
                }
            }
        }

        /// <summary>
        /// Loads a mod from the filesystem.
        /// </summary>
        /// <param name="path">The absolute path of the mod to load.</param>
        internal static void LoadMod(string path)
        {
            // check if the directory exists
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"{path} could not be found!");

            SeshatBundle bundle = new SeshatBundle(path);

            // check if a seshat.toml exists
            if (bundle.Exists(ModMetaFile))
                LoadSeshatBundle(bundle);
            else
                LoadBaseMod(bundle);
        }

        private static void LoadBaseMod(SeshatBundle bundle)
        {
            throw new System.NotImplementedException("BaseMods are not supported!");
        }

        private static void LoadSeshatBundle(SeshatBundle bundle)
        {
            // read seshat.toml
            SeshatModuleMetadata[] metas = SeshatModuleMetadata.DeserializeAll(
                new StreamReader(bundle.GetFile(ModMetaFile)));

            foreach (var meta in metas)
                LoadSeshatMeta(bundle, meta);
        }

        private static void LoadSeshatMeta(SeshatBundle bundle, SeshatModuleMetadata meta)
        {
            // exit early if the mod has a duplicate id
            if (Seshat.HasId(meta.id))
            {
                Logger.Error("loader", $"Mod {meta} has a conflicting id!");
                return;
            }

            // find dll
            if (meta.dll == null)
            {
                // register null module if a dll couldn't be found
                NullModule mod = new NullModule(meta);
                mod.bundle = bundle;
                mod.Register();
                return;
            }

            // attempt to load assembly
            Assembly asm;
            try 
            {
                asm = bundle.LoadAssembly(meta.dll);
            } 
            catch (Exception e) 
            {
                Logger.Error("loader", $"Failed to load assembly {meta.dll} of mod {meta}");
                e.LogException();
                return;
            }

            LoadSeshatAssembly(bundle, meta, asm);
        }

        private static void LoadSeshatAssembly(SeshatBundle bundle, SeshatModuleMetadata meta, Assembly asm)
        {
            Type[] types;
            try
            {
                types = GetTypesSafe(asm);
            }
            catch (Exception e)
            {
                Logger.Error("loader", $"Failed reading assembly of mod {meta}");
                e.LogException();
                return;
            }

           
            foreach (Type type in types)
            {
                if (typeof(SeshatModule).IsAssignableFrom(type)
                    && !type.IsAbstract 
                    && !typeof(NullModule).IsAssignableFrom(type))
                {
                    Logger.Debug("loader", $"Loading type {type.FullName} from {asm.FullName} in {meta}");
                    SeshatModule mod = (SeshatModule)Activator.CreateInstance(type);
                    mod.Metadata = meta;
                    mod.bundle = bundle;
                    mod.Register();
                }
            }
        }
        private static Type[] GetTypesSafe(Assembly asm)
        {
            try
            {
                return asm.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                foreach (var ex in e.LoaderExceptions)
                {
                    Logger.Error("loader", $"Failed to load some types in assembly {asm.FullName}.");
                    ex.LogException();
                }

                return e.Types.Where(t => t != null).ToArray();
            }
        }
    }
}
