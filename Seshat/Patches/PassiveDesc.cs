using System.Xml.Serialization;

namespace LOR_XML
{
    class patch_PassiveDesc : PassiveDesc
    {
        [XmlAttribute("SID")]
        public string sid;
    }

    public static class PassiveDescExt
    {
        public static string GetId(this PassiveDesc desc)
            => ((patch_PassiveDesc)desc).sid;

        public static PassiveDesc SetId(this PassiveDesc desc, string sid)
        {
            ((patch_PassiveDesc)desc).sid = sid;
            return desc;
        }
    }
}
