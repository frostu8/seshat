using System;

namespace Seshat.Module
{
    /// <summary>
    /// Interopability class for BaseMods.
    /// </summary>
    internal class BaseModInteropModule : SeshatModule
    {
        private Type[] _patches;

        public BaseModInteropModule(SeshatModuleMetadata metadata, Type[] patches)
        {
            this.Metadata = metadata;
            this._patches = patches;
        }

        public override void Load()
        {
            foreach (Type patch in this._patches)
            {
                Activator.CreateInstance(patch);
            }
        }

        public override void Unload() { }
    }
}
