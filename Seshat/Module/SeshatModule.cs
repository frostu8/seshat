using System;
using System.Reflection;
using GameSave;
using Registrar = Seshat.API.Registrar;
using Seshat.API;
using Seshat.Attribute;

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
        /// The bundle related to the module. Use this to access mod content.
        /// </summary>
        public SeshatBundle Bundle { get { return bundle; } }
        internal SeshatBundle bundle;

        public ModuleRegisterHelper Register 
        { get => new ModuleRegisterHelper(this); }

        /// <summary>
        /// Called after the game has loaded all assets, and Seshat has loaded
        /// all other modules.
        /// </summary>
        public virtual void Load()
        {
            // get parent assembly
            Assembly asm = Assembly.GetCallingAssembly();

            // load all implemented types
            foreach (Type type in asm.GetTypes())
            {
                if (System.Attribute.IsDefined(type, typeof(DiceAbilityAttribute)))
                    RegisterDiceAbility(type);
                if (System.Attribute.IsDefined(type, typeof(CardAbilityAttribute)))
                    RegisterCardAbility(type);
                if (System.Attribute.IsDefined(type, typeof(PassiveAbilityAttribute)))
                    RegisterPassiveAbility(type);
            }
        }

        /// <summary>
        /// Called after the game has recieved a close signal. Unallocate any
        /// unmanaged resources here.
        /// </summary>
        public virtual void Unload()
        {

        }

        /// <summary>
        /// Called directly when Seshat has loaded the module, before any game
        /// initialization can be completed.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Called after the game has loaded the save data, and classes like
        /// <see cref="InventoryModel"/> have loaded.
        /// </summary>
        public virtual void GameDataLoad(SaveData save) { }

        /// <summary>
        /// Called right before the game has started to save data to disk.
        /// </summary>
        public virtual void GameDataSave(SaveData save) { }

        protected void RegisterDiceAbility(Type type)
        {
            if (!type.IsSubclassOf(typeof(DiceCardAbilityBase)))
            {
                Logger.Warn(Metadata.id, $"Type {type.FullName} attributed with " +
                    "DiceAbility does not extend DiceCardAbilityBase!");
                return;
            }

            DiceAbilityAttribute attr =
                (DiceAbilityAttribute)System.Attribute.GetCustomAttribute(type, typeof(DiceAbilityAttribute));

            if (attr == null)
            {
                Logger.Warn(Metadata.id, $"Type {type.FullName} is not " +
                    "attributed with DiceAbility!");
                return;
            }

            // normalize id
            string id = StringId.HasDomainOr(attr.id, Metadata.Domain);

            Logger.Debug(Metadata.id, $"Loading dice ability {type.Name} as {id}");

            Registrar.DiceAbility.AddModded(id, type);
        }

        protected void RegisterCardAbility(Type type)
        {
            if (!type.IsSubclassOf(typeof(DiceCardSelfAbilityBase)))
            {
                Logger.Warn(Metadata.id, $"Type {type.FullName} attributed with " +
                    "CardAbility does not extend DiceCardSelfAbilityBase!");
                return;
            }

            CardAbilityAttribute attr =
                (CardAbilityAttribute)System.Attribute.GetCustomAttribute(type, typeof(CardAbilityAttribute));

            if (attr == null)
            {
                Logger.Warn(Metadata.id, $"Type {type.FullName} is not " +
                    "attributed with CardAbility!");
                return;
            }

            // normalize id
            string id = StringId.HasDomainOr(attr.id, Metadata.Domain);

            Logger.Debug(Metadata.id, $"Loading card ability {type.Name} as {id}");

            Registrar.CardAbility.AddModded(id, type);
        }

        protected void RegisterPassiveAbility(Type type)
        {
            try
            {
                PassiveXmlInfo info = PassiveAbilityAttribute.GetSpec(type);

                if (info.GetId() == null)
                {
                    Logger.Warn(Metadata.id, $"Type {type.FullName} is missing an id!");
                    return;
                }

                // normalize id
                Logger.Debug(Metadata.id, $"Loading passive {type.Name} as {info.id}");

                Registrar.Passive.AddModded(info);
            } 
            catch (ArgumentException e)
            {
                Logger.Warn(Metadata.id, e.Message);
            }
        }
    }
}
