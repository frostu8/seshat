using System.Collections.Generic;
using System.Collections.ObjectModel;
using Seshat.Module;

namespace Seshat
{
    public static class Seshat
    {
        /// <summary>
        /// All modules loaded by Seshat.
        /// </summary>
        public static ReadOnlyCollection<SeshatModule> Modules => _modules.AsReadOnly();
        private static List<SeshatModule> _modules = new List<SeshatModule>();

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
    }
}
