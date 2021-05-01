using System.Collections.Generic;
using LOR_XML;

namespace Seshat.API.Registrar.Localize
{
    public static class Passive
    {
        private static ModelDictionary<PassiveDesc> _passives
            = new ModelDictionary<PassiveDesc>();

        internal static void AddVanilla(PassiveDesc desc)
        {
            // hash a new string id for the passive
            desc.SetId(Seshat.HashNumericId(desc.ID));

            _passives.Add(desc.GetId(), desc);
        }

        internal static void AddModded(PassiveDesc desc) 
            => _passives.Add(desc.GetId(), desc);

        public static PassiveDesc Get(int id)
        {
            // same deal here with Registrar.Localize.CombatPage, except
            // red mist passive needs to be explicitly checked here.
            if (id == 250522)
                return Get(StringId.Concat(Seshat.VanillaDomain, "250522"));

            PassiveXmlInfo passive = Registrar.Passive.Get(id);

            if (passive?.GetId() != null)
                return Get(passive.GetId());
            else
                return null;
        }

        public static PassiveDesc Get(string sid)
            => _passives.Get(sid);
        public static IEnumerable<PassiveDesc> ByDomain(string domain)
            => _passives.ByDomain(domain);
        public static IEnumerable<PassiveDesc> All()
            => _passives.Values;
    }
}
