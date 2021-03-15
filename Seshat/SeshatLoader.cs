using System;
using System.IO;
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
        public static void LoadAuto()
            => LoadAll(ModsPath);

        /// <summary>
        /// Loads all mods from a directory.
        /// </summary>
        /// <param name="modsPath">The absolute path of the directory of mods.</param>
        public static void LoadAll(string modsPath)
        {
            DirectoryInfo modsDir = new DirectoryInfo(modsPath);

            if (!modsDir.Exists)
                modsDir.Create();

            foreach (var dir in modsDir.GetDirectories())
            {
                try { LoadMod(dir.FullName); }
                catch (Exception e)
                {
                    // print exception details
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        /// <summary>
        /// Loads a mod from the filesystem.
        /// </summary>
        /// <param name="path">The absolute path of the mod to load.</param>
        public static void LoadMod(string path)
        {
            // check if the directory exists
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"{path} could not be found!");

            SeshatBundle bundle = new SeshatBundle(path);

            // check if a seshat.toml exists
            if (bundle.FileExists(ModMetaFile))
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
            {
                // exit early if the mod has a duplicate id
                if (Seshat.HasId(meta.id))
                    throw new DuplicateModException(meta.id);

                // find dll
                if (meta.dll == null)
                {
                    // register null module if a dll couldn't be found
                    new NullModule(meta).Register();
                }
            }
        }
    }
}
