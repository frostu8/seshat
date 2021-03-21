using System;
using Seshat.Attribute;

namespace Seshat.ExampleMod
{
    [DiceAbility("sixty_nine")]
    public class SixtyNine : DiceCardAbilityBase
    {
        public override void OnRollDice()
        {
            base.behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmgRate = -9999,
                breakRate = -9999
            });
        }

        public override void OnSucceedAttack()
        {
            base.card.target.TakeDamage((int)Math.Ceiling(base.card.target.hp), DamageType.Card_Ability, base.owner);
        }
    }
}
