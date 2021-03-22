using System.ComponentModel;
using MonoMod;

namespace LOR_XML
{
    // funnily enough, we don't have to do anything to this.
    // We still do want to define these extension methods to provide a
    // consistent api.
    public static class BattleCardAbilityDescExt
    {
        /// <summary>
        /// Get the string ID of this BattleCardAbilityDesc.
        /// </summary>
        /// <seealso cref="Seshat.StringId"/>
        public static string GetSID(this BattleCardAbilityDesc self)
            => self.id;

        /// <summary>
        /// Set the string ID of this BattleCardAbilityDesc.
        /// </summary>
        /// <seealso cref="Seshat.StringId"/>
        public static BattleCardAbilityDesc SetSID(this BattleCardAbilityDesc self, string sid)
        {
            self.id = sid;
            return self;
        }
    }
}
