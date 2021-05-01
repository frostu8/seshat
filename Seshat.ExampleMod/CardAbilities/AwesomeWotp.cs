using System.Collections.Generic;
using System.Linq;
using Seshat.Attribute;
using LOR_DiceSystem;
using UnityEngine;

namespace Seshat.ExampleMod.CardAbilities
{
    [CardAbility("awesome_wotp/card")]
    public class AwesomeWotp : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            BattleUnitModel target = card.target;

            if (target == null)
                return;

            switch (owner.Book.ClassInfo.RangeType)
            {
                case EquipRangeType.Melee:
                    DrawCards(target.allyCardDetail.GetAllDeck().Where(c => c.GetSpec().Ranged != CardRange.Far));
                    break;
                case EquipRangeType.Range:
                    DrawCards(target.allyCardDetail.GetAllDeck().Where(c => c.GetSpec().Ranged != CardRange.Near));
                    break;
                case EquipRangeType.Hybrid:
                    DrawCards(target.allyCardDetail.GetAllDeck());
                    break;
            }
        }

        public void DrawCards(IEnumerable<BattleDiceCardModel> cards)
        {
            BattleDiceCardModel[] cardArray = cards.ToArray();

            for (int i = 0; i < 3; i++)
            {
                int cardIdx = Random.Range(0, cardArray.Length);

                BattleDiceCardModel card = owner.allyCardDetail.AddNewCard(cardArray[cardIdx].GetID(), false);
                card.exhaust = true;
                card.SetCurrentCost(0);
            }
        }
    }
}
