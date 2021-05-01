using System;

namespace Seshat.API
{
    public class DiceAbilityInfo
    {
        public string id;

        public Type type;

        public DiceAbilityInfo(string id, Type type)
        {
            if (!type.IsSubclassOf(typeof(DiceCardAbilityBase)))
                throw new ArgumentException(
                    $"Type {type.FullName} does not extend " +
                    "DiceCardAbilityBase.", "type");

            this.id = id;
            this.type = type;
        }

        public DiceCardAbilityBase Instantiate()
        {
            return (DiceCardAbilityBase)Activator.CreateInstance(type);
        }
    }
}
