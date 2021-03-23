using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOR_DiceSystem;
using LOR_XML;

namespace Seshat.API.Registrar.Localize
{
    public static class CombatPage
    {
        private static ModelDictionary<BattleCardDesc> _desc =
            new ModelDictionary<BattleCardDesc>();

        internal static void AddVanilla(BattleCardDesc card)
        {
            // hash a string id for the card
            card.SetId(Seshat.HashNumericId(card.cardID));

            _desc.Add(card.GetId(), card);
        }

        internal static void AddModded(BattleCardDesc card)
        {
            _desc.Add(card.GetId(), card);
        }

        public static BattleCardDesc Get(int id)
        {
            // We can actually safely assume that if this function is being
            // called by vanilla code, it means that this card is actually
            // identified in the database. If C# devs know what they're doing
            // with dictionaries, this should have an absolute minimum overhead
            // for easier programming.
            DiceCardXmlInfo card = Registrar.CombatPage.Get(id);

            if (card?.GetId() != null)
                return _desc.Get(card.GetId());
            else
                return null;
        }

        public static BattleCardDesc Get(string sid)
            => _desc.Get(sid);
        public static IEnumerable<BattleCardDesc> ByDomain(string domain)
            => _desc.ByDomain(domain);
        public static IEnumerable<BattleCardDesc> All()
            => _desc.Values;
    }
}
