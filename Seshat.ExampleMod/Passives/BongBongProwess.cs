using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seshat.Attribute;
using Registrar = Seshat.API.Registrar;

[PassiveAbility("bong_bong_prowess", Rarity.Unique, 10)]
public class BongBongProwess : PassiveAbilityBase
{
    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
        var diceAbility = ExampleMod.ExampleMod.Instance
            .GetDiceAbility("bong_bong_prowess_dice");

        if (diceAbility == null)
        {
            Seshat.Logger.Debug(ExampleMod.ExampleMod.Instance.Metadata.id, "failed to instantiate prowess dice");
            return;
        }

        foreach (var behaviour in curCard.cardBehaviorQueue)
        {
            // apply effect only to attack
            if (behaviour.behaviourInCard.Type == LOR_DiceSystem.BehaviourType.Atk)
            {
                behaviour.AddAbility(diceAbility.Instantiate());
            }
        }
    }
}
