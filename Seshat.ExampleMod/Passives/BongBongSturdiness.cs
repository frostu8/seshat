using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seshat.Attribute;

[PassiveAbility("bong_bong_sturdiness", Rarity.Unique, 10)]
public class BongBongSturdiness : PassiveAbilityBase
{
    public override float BreakDmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
    {
        return 0.5f;
    }
}
