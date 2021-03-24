using System;

class patch_PassiveXmlInfo : PassiveXmlInfo
{
    public string sid;

    public Type implementor;
}

public static class PassiveXmlInfoExt
{
    public static Type GetImplementor(this PassiveXmlInfo passive)
        => ((patch_PassiveXmlInfo)passive).implementor;

    public static PassiveXmlInfo SetImplementor(this PassiveXmlInfo passive, Type newType)
    {
        ((patch_PassiveXmlInfo)passive).implementor = newType;
        return passive;
    }

    public static string GetId(this PassiveXmlInfo passive)
        => ((patch_PassiveXmlInfo)passive).sid;

    public static PassiveXmlInfo SetId(this PassiveXmlInfo passive, string sid)
    {
        ((patch_PassiveXmlInfo)passive).sid = sid;
        return passive;
    }
}
