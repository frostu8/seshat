using LOR_XML;

namespace Seshat.API.Registrar.Localize
{
    public static class CardAbility
    {
        private static ModelDictionary<BattleCardAbilityDesc> _abilities
            = new ModelDictionary<BattleCardAbilityDesc>();

        internal static void AddVanilla(BattleCardAbilityDesc desc)
        {
            desc.SetSID(StringId.Concat(Seshat.VanillaDomain, desc.GetSID()));

            AddModded(desc);
        }
        internal static void AddModded(BattleCardAbilityDesc desc)
            => _abilities.Add(desc.GetSID(), desc);

        public static BattleCardAbilityDesc Get(string sid)
            => _abilities.Get(sid);
    }
}
