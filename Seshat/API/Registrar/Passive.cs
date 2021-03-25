using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seshat.Attribute;

namespace Seshat.API.Registrar
{
    public static class Passive
    {
        private static ModelDictionary<PassiveXmlInfo> _passives
            = new ModelDictionary<PassiveXmlInfo>();
        private static Dictionary<int, PassiveXmlInfo> _passivesByNum
            = new Dictionary<int, PassiveXmlInfo>();

        internal static void AddVanilla(PassiveXmlInfo passive)
        {
            // hash up a string id for the passive
            passive.SetId(Seshat.HashNumericId(passive.id));

            Add(passive);
        }

        internal static void AddModded(PassiveXmlInfo passive)
        {
            // generate a unique id for the card
            int generatedId = IdGen.NextFree(id => !_passivesByNum.ContainsKey(id));
            passive.id = generatedId;

            Add(passive);
        }

        internal static void AddModdedType(Type type)
        {
            if (!type.IsSubclassOf(typeof(PassiveAbilityBase)))
                throw new ArgumentException($"Type {type.Name} does not extend PassiveAbilityBase.");

            PassiveXmlInfo passive = PassiveAbilityAttribute.GetSpec(type);

            AddModded(passive);
        }

        private static void Add(PassiveXmlInfo passive)
        {
            _passives.Add(passive.GetId(), passive);
            _passivesByNum.Add(passive.id, passive);
        }

        public static PassiveAbilityBase GetNew(string id)
            => GetNew(Get(id));
        public static PassiveAbilityBase GetNew(int id)
            => GetNew(Get(id));

        public static PassiveAbilityBase GetNew(PassiveXmlInfo passive)
        {
            // if passive could not be found, then return null
            if (passive == null)
                return null;

            PassiveAbilityBase passiveImplementor;
            if (passive.GetImplementor() != null)
            {
                passiveImplementor =
                    (PassiveAbilityBase)Activator.CreateInstance(passive.GetImplementor());
            } 
            else
            {
                // some passives are display-only
                passiveImplementor = new PassiveAbilityBase();
            }

            passiveImplementor.name = Localize.Passive.Get(passive.GetId()).name;
            passiveImplementor.desc = Localize.Passive.Get(passive.GetId()).desc;

            passiveImplementor.rare = passive.rare;

            return passiveImplementor;
        }

        public static PassiveXmlInfo Get(string id)
            => _passives.Get(id);
        public static PassiveXmlInfo Get(int id)
            => _passivesByNum.TryGetValue(id, out var passive) ? passive : null;
    }
}
