using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOR_DiceSystem;
using LOR_XML;

namespace Seshat.API
{
    public static class DiceCardLocalizeRegistrar
    {
        private static ModelRegistrar<BattleCardDesc> _registrar;
        private static ModelRegistrar<BattleCardDesc> Registrar
        {
            get 
            { 
                if (_registrar == null) 
                    _registrar = new ModelRegistrar<BattleCardDesc>(); 
                return _registrar; 
            }
        }

        internal static void Add(BattleCardDesc card)
        {
            NormalizeCardId(card);

            Registrar.Add(card.cardID, card, card.GetSID());
        }

        // It is acceptable to have no string id, but it is not acceptable to
        // have no integer id.
        private static void NormalizeCardId(BattleCardDesc card)
        {
            if (!card.HasIntegerID())
            {
                if (card.GetSID() == null)
                    throw new ArgumentException("Model must have a string or numeric id!", "card");

                // check if a sister registrar already has this card
                DiceCardXmlInfo otherCard = DiceCardRegistrar.Get(card.GetSID());
                if (otherCard != null)
                {
                    card.cardID = otherCard.id;
                }
                else
                {
                    int generatedId = IdGen.NextFree(id => !Registrar.ModelDict.ContainsKey(id));
                    card.cardID = generatedId;
                }
            }
        }

        public static BattleCardDesc Get(int id)
            => Registrar.Get(id);
        public static BattleCardDesc Get(string sid)
            => Registrar.Get(sid);
        public static IEnumerable<BattleCardDesc> ByDomain(string domain)
            => Registrar.ByDomain(domain);
        public static IEnumerable<BattleCardDesc> All()
            => Registrar.All();
    }
}
