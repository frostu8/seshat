using System;

namespace Seshat.Attribute
{
    public class PassiveAbilityAttribute : System.Attribute
    {
        public string id;
        public int cost;

        public Rarity rarity = Rarity.Common;

        /// <summary>
        /// Determines if the passive can be transfered to another page.
        /// </summary>
        public bool attributable = true;

        public PassiveAbilityAttribute(string id, int cost)
        {
            this.id = id;
            this.cost = cost;
        }

        public PassiveAbilityAttribute(string id, Rarity rarity, int cost)
            : this(id, cost)
        {
            this.rarity = rarity;
        }

        public PassiveXmlInfo CreateSpec()
        {
            PassiveXmlInfo info = new PassiveXmlInfo()
            {
                cost = this.cost,
                rare = this.rarity,
                CanGivePassive = this.attributable,
            };

            info.SetId(this.id);
            return info;
        }

        public static PassiveXmlInfo GetSpec(Type type)
        {
            if (!type.IsSubclassOf(typeof(PassiveAbilityBase)))
                throw new ArgumentException($"Type {type.Name} does not extend PassiveAbilityBase.");

            PassiveAbilityAttribute attr =
                (PassiveAbilityAttribute)GetCustomAttribute(type, typeof(PassiveAbilityAttribute))
                ?? throw new ArgumentException($"Type {type.Name} is not " +
                "attributed with PassiveAbilityAttribute!", "type");

            PassiveXmlInfo spec = attr.CreateSpec();
            spec.SetImplementor(type);
            return spec;
        }
    }
}
