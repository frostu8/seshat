using MonoMod;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

class patch_ItemXmlData : ItemXmlData
{
    // a mod won't have this defined, so set it to -1 to indicate it isn't
    [DefaultValue(-1)]
    public new int id;

    [XmlAttribute("SID")]
    public string sid;
}

public static class ItemXmlDataExt
{
    /// <summary>
    /// Get the string ID of this ItemXmlData.
    /// </summary>
    /// <seealso cref="Seshat.StringId"/>
    public static string GetSID(this ItemXmlData self)
        => ((patch_ItemXmlData)self).sid;
    
    /// <summary>
    /// Set the string ID of this ItemXmlData.
    /// </summary>
    /// <seealso cref="Seshat.StringId"/>
    public static ItemXmlData SetSID(this ItemXmlData self, string sid)
    {
        ((patch_ItemXmlData)self).sid = sid;
        return self;
    }

    internal static bool HasIntegerID(this ItemXmlData self)
        => self.id >= 0;
}
