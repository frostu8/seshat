using Seshat.Module;
using Seshat;

namespace ExampleMod
{
    public class ExampleMod : SeshatModule
    {
        /// This function is called after all of the vanilla game data is done
        /// loading. Register any key pages/combat pages/abnormality pages/etc
        /// here, and make any patches here.
        /// 
        /// In the future, key pages/combat pages/xml data will be registered
        /// automatically based on the file structure of the mod.
        public override void Load()
        { 
            Logger.Info(Metadata.id, $"Load(); called for mod {Metadata.name}");
        }

        /// This function is called to undo any patches or release any unmanaged
        /// resources. Deregistration will be performed automatically.
        public override void Unload()
        { 
            Logger.Info(Metadata.id, $"Unload(); called for mod {Metadata.name}");
        }

        /// This function is called to initialize any data, like a constructor
        /// would.
        public override void Initialize()
        {
            Logger.Info(Metadata.id, $"Initialize(); called for mod {Metadata.name}");
        }

        /// This is the metadata of the module. This can be overriden to
        /// provide dynamic data. See <see cref="SeshatModuleMetadata"/> for
        /// metadata entries.
        public override SeshatModuleMetadata Metadata 
        { get => base.Metadata; set => base.Metadata = value; }
    }
}
