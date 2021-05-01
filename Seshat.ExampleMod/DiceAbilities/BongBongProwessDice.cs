using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seshat.Attribute;

[DiceAbility("bong_bong_prowess_dice")]
public class BongBongProwessDice : DiceCardAbilityBase
{
    private int count = 0;

    public override void OnSucceedAttack()
    {
        if (count++ < 5)
            this.ActivateBonusAttackDice();
    }
}
