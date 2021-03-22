using System;
using System.Collections;
using System.Collections.Generic;
using LOR_DiceSystem;
using LOR_XML;

namespace Seshat.API.Registrar
{
    public static class CombatPage
    {
        private static Dictionary<int, DiceCardXmlInfo> _cards = 
            new Dictionary<int, DiceCardXmlInfo>();
        private static ModelDictionary<DiceCardXmlInfo> _cardsBySid = 
            new ModelDictionary<DiceCardXmlInfo>();

        internal static void AddVanilla(DiceCardXmlInfo card)
        {
            // hash up a string id for the card
            card.SetSID(Seshat.HashNumericId(card.id));

            Add(card);
        }

        internal static void AddModded(DiceCardXmlInfo card)
        {
            // generate a unique id for the card
            int generatedId = IdGen.NextFree(id => !_cards.ContainsKey(id));
            card.id = generatedId;

            Add(card);
        }

        private static void Add(DiceCardXmlInfo card)
        {
            NormalizeCardReferences(card);

            _cards.Add(card.id, card);
            _cardsBySid.Add(card.GetSID(), card);
        }

        private static void NormalizeCardReferences(DiceCardXmlInfo card)
        {
            string domain = StringId.GetDomain(card.GetSID());

            // normalize card .Script
            if (!string.IsNullOrEmpty(card.Script))
                card.Script = StringId.HasDomainOr(card.Script, domain);

            // normalize dices .Script
            foreach (var dice in card.DiceBehaviourList)
                if (!string.IsNullOrEmpty(dice.Script))
                    dice.Script = StringId.HasDomainOr(dice.Script, domain);
        }

        public static DiceCardXmlInfo Get(int id)
            => _cards.TryGetValue(id, out var card) ? card : null;
        public static DiceCardXmlInfo Get(string sid)
            => _cardsBySid.Get(sid);
        public static IEnumerable<DiceCardXmlInfo> ByDomain(string domain)
            => _cardsBySid.ByDomain(domain);
        public static IEnumerable<DiceCardXmlInfo> All()
            => _cardsBySid.Values;
    }
}
