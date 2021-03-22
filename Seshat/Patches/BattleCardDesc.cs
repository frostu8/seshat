using System.ComponentModel;
using System.Xml.Serialization;

namespace LOR_XML
{
    class patch_BattleCardDesc : BattleCardDesc
    {
        // a mod won't have this defined, so set it to -1 to indicate it isn't
        [DefaultValue(-1)]
        public new int cardID;

        [XmlAttribute("SID")]
        public string sid;
    }

    public static class BattleCardDescExt
    {
        /// <summary>
        /// Get the string ID of this BattleCardDesc.
        /// </summary>
        /// <seealso cref="Seshat.StringId"/>
        public static string GetSID(this BattleCardDesc self)
            => ((patch_BattleCardDesc)self).sid;
        
        /// <summary>
        /// Set the string ID of this ItemXmlData.
        /// </summary>
        /// <seealso cref="Seshat.StringId"/>
        public static BattleCardDesc SetSID(this BattleCardDesc self, string sid)
        {
            ((patch_BattleCardDesc)self).sid = sid;
            return self;
        }

        internal static bool HasIntegerID(this BattleCardDesc self)
            => self.cardID >= 0;
    }
}
