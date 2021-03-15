namespace Seshat.Module
{
    /// <summary>
    /// Dummy module for content mods.
    /// </summary>
    internal class NullModule : SeshatModule
    {
        public NullModule(SeshatModuleMetadata meta)
        {
            this.Metadata = meta;
        }

        public override void Load() { }
        public override void Unload() { }
    }
}
