using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Seshat.API.Registrar
{
    public static class CardAbility
    {
        private static ModelDictionary<CardAbilityInfo> _abilities
            = new ModelDictionary<CardAbilityInfo>();

        internal static void Add(CardAbilityInfo card)
            => _abilities.Add(card.id, card);

        /// <summary>
        /// Gets a <see cref="DiceCardSelfAbilityBase"/> from the registrar.
        /// </summary>
        /// <param name="sid">The string ID of the ability.</param>
        /// <returns>The type, or <c>null</c> if it couldn't be found.</returns>
        public static CardAbilityInfo Get(string sid)
            => _abilities.Get(sid);

        public static List<string> GetKeywords(string sid)
        {
            List<string> keywords = new List<string>();

            if (!string.IsNullOrEmpty(sid))
            {

                DiceCardSelfAbilityBase ability = Get(sid)?.Instantiate();
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
                const string prefix = "DiceCardSelfAbility_";

                if (type.Name.StartsWith(prefix))
                {
                    string id = StringId.Concat(
                        Seshat.VanillaDomain,
                        type.Name.Substring(prefix.Length));

                    try { Add(new CardAbilityInfo(id, type)); }
                    // discard any argumentexception errors from bad types.
                    catch (ArgumentException) { }
                }
            }
        }
    }
}
