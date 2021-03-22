using Seshat.Module;
using Seshat;
using System.IO;
using GameSave;

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
            // IMPORTANT! CALL base.Load(); TO LOAD OUR CUSTOM TYPES.
            base.Load();

            Logger.Info(Metadata.id, $"Load(); called for mod {Metadata.name}");

            // load our custom card
            this.Register.CombatPages("card.xml");

            // load our custom localizations
            this.Register.LocalizeCombatPages("card_localize.xml");
            this.Register.LocalizeDiceAbilities("card_ability_localize.xml");
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

        public override void GameDataLoad(SaveData save)
        {
            Logger.Info(Metadata.id, $"GameDataLoad(); called for mod {Metadata.name}");

            // add our new combat page to the inventory
            var customPage = this.Register.GetCombatPage("troll_69");

            if (InventoryModel.Instance.GetCardCount(customPage.id) <= 0)
                InventoryModel.Instance.AddCard(customPage.id, 69);
        }

        public override void GameDataSave(SaveData save)
        {
            Logger.Info(Metadata.id, $"GameDataSave(); called for mod {Metadata.name}");
        }

        /// This is the metadata of the module. This can be overriden to
        /// provide dynamic data. See <see cref="SeshatModuleMetadata"/> for
        /// metadata entries.
        public override SeshatModuleMetadata Metadata 
        { get => base.Metadata; set => base.Metadata = value; }
    }
}
