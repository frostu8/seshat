using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Seshat.Module;

namespace Seshat
{
    public static class Seshat
    {
        /// <summary>
        /// The version of Seshat being run.
        /// </summary>
        public static Version Version
        {
            get
            {
                return new Version(VersionString);
            }
        }
        public const string VersionString = "0.1.0.0";

        /// <summary>
        /// All modules loaded by Seshat.
        /// </summary>
        public static ReadOnlyCollection<SeshatModule> Modules => _modules.AsReadOnly();
        private static List<SeshatModule> _modules = new List<SeshatModule>();

        internal static void RunLoad()
        {
            _modules.ForEach(module => {
                try { module.Load(); }
                catch (Exception e)
                {
                    Logger.Error("seshat", $"Failed to run Load(); callback for mod {module.Metadata}!");
                    Logger.Error("seshat", "SEE BELOW FOR EXCEPTION DETAILS:");
                    e.LogException();
                }
            });
        }

        internal static void RunUnload()
        {
            _modules.ForEach(module => {
                try { module.Unload(); }
                catch (Exception e)
                {
                    Logger.Error("seshat", $"Failed to run Unload(); callback for mod {module.Metadata}!");
                    Logger.Error("seshat", "SEE BELOW FOR EXCEPTION DETAILS:");
                    e.LogException();
                }
            });
        }

        /// <summary>
        /// Registers a module to Seshat.
        /// </summary>
        /// <param name="module">The module to register.</param>
        public static void Register(this SeshatModule module) 
        {
            // add module
            lock (_modules) _modules.Add(module);

            // call module's initialize function after all types were checked
            module.Initialize();
        }


        internal static bool HasId(string id)
            => _modules.Any(mod => mod.Metadata.id == id);
    }
}
