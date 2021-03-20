using System.Collections;
using System.Collections.Generic;
using LOR_DiceSystem;

namespace Seshat.API
{
    /// <summary>
    /// A specialization of <see cref="ModelRegistrar{T}"/> for
    /// <see cref="DiceCardXmlInfo"/>.
    /// </summary>
    public static class DiceCardRegistrar
    {
        private static ModelRegistrar<DiceCardXmlInfo> _instance;
        private static ModelRegistrar<DiceCardXmlInfo> Instance
        {
            get 
            { 
                if (_instance == null) 
                    _instance = new ModelRegistrar<DiceCardXmlInfo>(); 
                return _instance; 
            }
        }

        /// <summary>
        /// Directly registers a card to the registrar, giving it an unallocated
        /// number ID.
        /// </summary>
        /// <param name="card">The card to register.</param>
        /// <exception cref="System.ArgumentException">
        /// The string id has already been taken.
        /// </exception>
        /// <remarks>
        /// Any cards that are passed to this will be given an integer ID if
        /// it doesn't already have one, and any references to any other SID
        /// identified models will be normalized (if they do not have domains,
        /// they will be given the domain of the SID of the card).
        /// </remarks>
        public static void Register(DiceCardXmlInfo card)
        {
            NormalizeCardId(card);
            NormalizeCardReferences(card);

            Instance.Add(card.id, card, card.GetSID());
        }

        // It is acceptable to have no string id, but it is not acceptable to
        // have no integer id.
        private static void NormalizeCardId(DiceCardXmlInfo card)
        {
            if (!card.HasIntegerID())
            {
                int generatedId = IdGen.NextFree(id => !Instance.ModelDict.ContainsKey(id));

                card.id = generatedId;
            }
        }

        private static void NormalizeCardReferences(DiceCardXmlInfo card)
        {
            string domain = card.GetSID() == null ? 
                Seshat.VanillaDomain : StringId.GetDomain(card.GetSID());

            // normalize dices .Script
            foreach (var dice in card.DiceBehaviourList)
                if (!string.IsNullOrEmpty(dice.Script))
                    dice.Script = StringId.HasDomainOr(dice.Script, domain);
        }

        public static DiceCardXmlInfo Get(int id)
            => Instance.Get(id);
        public static DiceCardXmlInfo Get(string sid)
            => Instance.Get(sid);
        public static IEnumerable<DiceCardXmlInfo> ByDomain(string domain)
            => Instance.ByDomain(domain);
        public static IEnumerable<DiceCardXmlInfo> All()
            => Instance.All();
    }
}
