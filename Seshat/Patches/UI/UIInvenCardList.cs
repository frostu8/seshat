using System.Collections.Generic;
using System.Linq;
using MonoMod;

using AbilityDesc = BattleCardAbilityDescXmlList;

namespace UI
{
    public class patch_UIInvenCardList : UIInvenCardList
    {
        // WOAH NELLY! THIS FUNCTION IS SUPER OVERCOMPLICATED! Lets LINQ it up!
        [MonoModReplace]
        public new List<DiceCardItemModel> GetCardsByDetailFilterUI(List<DiceCardItemModel> cards)
        {
            // get filters
            UICardListDetailFilterPopup filter = CardFilter.GetDetailFilter();
            var rarityFilter = filter.CheckRarityDetailFilter();
            var diceFilter = filter.CheckDiceDetailFilter();
            var bufFilter = filter.CheckBufDetailFilter();
            var abilityFilter = filter.CheckAbilityDetailFilter();
            var diceCountFilter = filter.CheckDiceCountDetailFilter();

            return cards
                .Where(card => rarityFilter.Count <= 0 ||
                    rarityFilter.Contains(card.GetRarity().ToString()))
                .Where(card => diceFilter.Count <= 0 ||
                    card.GetBehaviourList().Any(dice => diceFilter.Contains(dice.Detail.ToString()) || diceFilter.Contains(dice.Type.ToString())))
                .Where(card => bufFilter.Count <= 0 ||
                    AbilityDesc.Instance.GetAbilityKeywords(card.ClassInfo.Script).Any(k => bufFilter.Contains(k)) ||
                    card.GetBehaviourList().Any(dice => AbilityDesc.Instance.GetAbilityKeywords(dice.Script).Any(k => bufFilter.Contains(k))))
                .Where(card => abilityFilter.Count <= 0 ||
                    AbilityDesc.Instance.GetAbilityKeywords(card.ClassInfo.Script).Any(k => abilityFilter.Contains(k)) ||
                    card.GetBehaviourList().Any(dice => AbilityDesc.Instance.GetAbilityKeywords(dice.Script).Any(k => abilityFilter.Contains(k))))
                .Where(card => diceCountFilter.Count <= 0 ||
                    diceCountFilter.Contains(card.GetBehaviourList().Count))
                .ToList();
        }
    }
}
