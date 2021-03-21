using Seshat.Attribute;

namespace Seshat.ExampleMod
{
    [DiceAbility("sixty_nine")]
    public class SixtyNine : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            //base.card.target.TakeDamage((int)base.card.target.hp, DamageType.Card_Ability, base.owner);
            base.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 143, base.owner);
        }
    }
}
