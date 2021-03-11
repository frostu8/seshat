using System;

namespace Seshat.Module
{
    /// <summary>
    /// Interopability class for BaseMods.
    /// </summary>
    internal class BaseModInteropModule : SeshatModule
    {
        private Type _patchType;

        public BaseModInteropModule(SeshatModuleMetadata metadata, Type patchType)
        {
            this.Metadata = metadata;
            this._patchType = patchType;
        }

        public override void Load()
        {
            Activator.CreateInstance(this._patchType);
        }

        public override void Unload() { }
    }
}
