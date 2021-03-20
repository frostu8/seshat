using System;
using System.Collections.Generic;
using System.Reflection;

namespace Seshat.API
{
    /// <summary>
    /// Responsible for keeping track of all <see cref="DiceCardAbilityBase"/>.
    /// </summary>
    public static class DiceCardAbilityRegistrar
    {
        private static Dictionary<string, Type> _abilities = new Dictionary<string, Type>();

        /// <summary>
        /// Gets a <see cref="DiceCardAbilityBase"/> from the registrar,
        /// instantiates it and returns it.
        /// </summary>
        /// <param name="sid">The string ID of the ability.</param>
        /// <returns>The ability, or <c>null</c> if it couldn't be found.</returns>
        public static DiceCardAbilityBase GetNew(string sid)
        {
            Type type = Get(sid);

            if (type != null)
                return (DiceCardAbilityBase)Activator.CreateInstance(type);
            else
                return null;
        }

        /// <summary>
        /// Gets a <see cref="DiceCardAbilityBase"/> from the registrar.
        /// </summary>
        /// <param name="sid">The string ID of the ability.</param>
        /// <returns>The type, or <c>null</c> if it couldn't be found.</returns>
        public static Type Get(string sid)
        {
            if (_abilities.TryGetValue(sid, out Type type))
                return type;
            return null;
        }

        public static List<string> GetKeywords(string sid)
        {
            List<string> keywords = new List<string>();

            if (!string.IsNullOrEmpty(sid))
            {

                DiceCardAbilityBase ability = GetNew(sid);
                if (ability != null)
                    keywords.AddRange(ability.Keywords);
            }

            return keywords;
        }

        /// <summary>
        /// Adds a new <see cref="DiceCardAbilityBase"/> to the registrar.
        /// </summary>
        /// <param name="sid">The string ID of the ability.</param>
        /// <param name="type">The ability class.</param>
        /// <exception cref="ArgumentException">
        /// The type does not derive from <see cref="DiceCardAbilityBase"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// A type already exists with the same sid.
        /// </exception>
        public static void Add(string sid, Type type)
        {
            if (!type.IsSubclassOf(typeof(DiceCardAbilityBase)))
                throw new ArgumentException("Type must derive from DiceCardAbilityBase!", "type");

            _abilities.Add(sid, type);
        }

        internal static void LoadVanilla()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in types)
            {
                const string prefix = "DiceCardAbility_";

                if (type.Name.StartsWith(prefix))
                    Add(StringId.VanillaSid(type.Name.Substring(prefix.Length)), type);
            }
        }
    }
}
