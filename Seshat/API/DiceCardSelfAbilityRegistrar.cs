using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Seshat.API
{
    class DiceCardSelfAbilityRegistrar
    {
        private static ModelDictionary<Type> _abilities
            = new ModelDictionary<Type>();

        internal static void AddVanilla(string sid, Type type)
            => AddModded(StringId.Concat(Seshat.VanillaDomain, sid), type);
        internal static void AddModded(string sid, Type type)
            => _abilities.Add(sid, type);

        /// <summary>
        /// Gets a <see cref="DiceCardSelfAbilityBase"/> from the registrar,
        /// instantiates it and returns it.
        /// </summary>
        /// <param name="sid">The string ID of the ability.</param>
        /// <returns>The ability, or <c>null</c> if it couldn't be found.</returns>
        public static DiceCardSelfAbilityBase GetNew(string sid)
        {
            Type type = Get(sid);

            if (type != null)
                return (DiceCardSelfAbilityBase)Activator.CreateInstance(type);
            else
                return null;
        }

        /// <summary>
        /// Gets a <see cref="DiceCardSelfAbilityBase"/> from the registrar.
        /// </summary>
        /// <param name="sid">The string ID of the ability.</param>
        /// <returns>The type, or <c>null</c> if it couldn't be found.</returns>
        public static Type Get(string sid)
            => _abilities.Get(sid);

        public static List<string> GetKeywords(string sid)
        {
            List<string> keywords = new List<string>();

            if (!string.IsNullOrEmpty(sid))
            {

                DiceCardSelfAbilityBase ability = GetNew(sid);
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
                    AddVanilla(type.Name.Substring(prefix.Length), type);
            }
        }
    }
}
