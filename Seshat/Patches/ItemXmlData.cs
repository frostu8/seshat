using MonoMod;
using System.Xml;
using System.Xml.Serialization;

class patch_ItemXmlData : ItemXmlData
{
    /// <summary>
    /// The string ID of the item.
    /// </summary>
    /// <seealso cref="Seshat.StringId"/>
    [XmlAttribute("SID")]
    public string sid;
}

public static class ItemXmlDataExt
{
    /// <summary>
    /// Get the string ID of this ItemXmlData.
    /// </summary>
    public static string GetSID(this ItemXmlData self)
        => ((patch_ItemXmlData)self).sid;
    
    /// <summary>
    /// Set the string ID of this ItemXmlData.
    /// </summary>
    public static ItemXmlData SetSID(this ItemXmlData self, string sid)
    {
        ((patch_ItemXmlData)self).sid = sid;
        return self;
    }
}
