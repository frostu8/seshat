using System;

namespace Seshat.API
{
    public class CardAbilityInfo
    {
        public string id;

        public Type type;

        public CardAbilityInfo(string id, Type type)
        {
            if (!type.IsSubclassOf(typeof(DiceCardSelfAbilityBase)))
                throw new ArgumentException(
                    $"Type {type.FullName} does not extend " +
                    "DiceCardSelfAbilityBase.", "type");

            this.id = id;
            this.type = type;
        }

        public DiceCardSelfAbilityBase Instantiate()
        {
            return (DiceCardSelfAbilityBase)Activator.CreateInstance(type);
        }
    }
}
