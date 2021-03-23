using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOR_XML;

namespace Seshat.API.Registrar.Localize
{
    public static class DiceAbility
    {
        private static ModelDictionary<BattleCardAbilityDesc> _abilities
            = new ModelDictionary<BattleCardAbilityDesc>();

        internal static void AddVanilla(BattleCardAbilityDesc desc)
        {
            desc.SetId(StringId.Concat(Seshat.VanillaDomain, desc.GetId()));

            AddModded(desc);
        }
        internal static void AddModded(BattleCardAbilityDesc desc)
            => _abilities.Add(desc.GetId(), desc);

        public static BattleCardAbilityDesc Get(string sid)
            => _abilities.Get(sid);
    }
}
