using System.ComponentModel;
using MonoMod;

namespace LOR_XML
{
    // funnily enough, we don't have to do anything to this.
    // We still do want to define these extension methods to provide a
    // consistent api.
    public class patch_BattleCardAbilityDesc : BattleCardAbilityDesc
    {
        internal patch_BattleCardAbilityDesc Clone()
            => (patch_BattleCardAbilityDesc)MemberwiseClone();
    }

    public static class BattleCardAbilityDescExt
    {
        /// <summary>
        /// Get the string ID of this BattleCardAbilityDesc.
        /// </summary>
        /// <seealso cref="Seshat.StringId"/>
        public static string GetId(this BattleCardAbilityDesc self)
            => self.id;

        /// <summary>
        /// Set the string ID of this BattleCardAbilityDesc.
        /// </summary>
        /// <seealso cref="Seshat.StringId"/>
        public static BattleCardAbilityDesc SetId(this BattleCardAbilityDesc self, string sid)
        {
            self.id = sid;
            return self;
        }

        internal static BattleCardAbilityDesc Clone(this BattleCardAbilityDesc self)
            => ((patch_BattleCardAbilityDesc)self).Clone();
    }
}
