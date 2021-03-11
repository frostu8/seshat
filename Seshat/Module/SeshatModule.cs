namespace Seshat.Module
{
    /// <summary>
    /// The class all modules must inhereit.
    /// </summary>
    public abstract class SeshatModule
    {
        /// <summary>
        /// The module's metadata.
        /// 
        /// <para>
        /// This is set by Seshat when reading the module. This can be overriden
        /// to provide dynamic metadata at runtime.
        /// </para>
        /// </summary>
        public virtual SeshatModuleMetadata Metadata { get; set; }

        /// <summary>
        /// Called after the game has loaded all assets, and Seshat has loaded
        /// all other modules.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Called after the game has recieved a close signal. Unallocate any
        /// unmanaged resources here.
        /// </summary>
        public abstract void Unload();

        /// <summary>
        /// Called directly when Seshat has loaded the module, before any game
        /// initialization can be completed.
        /// </summary>
        public virtual void Initialize() { }
    }
}
