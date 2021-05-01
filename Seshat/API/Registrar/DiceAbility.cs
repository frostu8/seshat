using System;
using System.Collections.Generic;
using System.Reflection;

namespace Seshat.API.Registrar
{
    /// <summary>
    /// Responsible for keeping track of all <see cref="DiceCardAbilityBase"/>.
    /// </summary>
    public static class DiceAbility
    {
        private static ModelDictionary<DiceAbilityInfo> _abilities
            = new ModelDictionary<DiceAbilityInfo>();

        internal static void Add(DiceAbilityInfo info)
        {
            _abilities.Add(info.id, info);
        }

        /// <summary>
        /// Gets a <see cref="DiceCardAbilityBase"/> from the registrar.
        /// </summary>
        /// <param name="sid">The string ID of the ability.</param>
        /// <returns>The type, or <c>null</c> if it couldn't be found.</returns>
        public static DiceAbilityInfo Get(string sid)
            => _abilities.Get(sid);

        public static List<string> GetKeywords(string sid)
        {
            List<string> keywords = new List<string>();

            if (!string.IsNullOrEmpty(sid))
            {

                DiceCardAbilityBase ability = Get(sid).Instantiate();
                if (ability != null)
                    keywords.AddRange(ability.Keywords);
            }

            return keywords;
        }

        internal static void LoadVanilla()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in types)
            {
                const string prefix = "DiceCardAbility_";

                if (type.Name.StartsWith(prefix))
                {
                    string id = StringId.Concat(
                        Seshat.VanillaDomain, 
                        type.Name.Substring(prefix.Length));

                    Add(new DiceAbilityInfo(id, type));
                }
            }
        }
    }
}
