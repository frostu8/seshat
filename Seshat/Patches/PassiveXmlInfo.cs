using System;
using Localize = Seshat.API.Registrar.Localize;

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
        if (newType != null)
        {
            if (!newType.IsSubclassOf(typeof(PassiveAbilityBase)))
            {
                throw new ArgumentException($"Type {newType.FullName} does" +
                    "not extend PassiveAbilityBase!");
            }
        }
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

    /// <summary>
    /// Instantiates a passive.
    /// </summary>
    /// <returns>The passive instance.</returns>
    public static PassiveAbilityBase Instantiate(this PassiveXmlInfo passive)
    {
        // if passive could not be found, then return null
        if (passive == null)
            throw new ArgumentNullException("passive");

        PassiveAbilityBase passiveImplementor;
        if (passive.GetImplementor() != null)
        {
            passiveImplementor =
                (PassiveAbilityBase)Activator.CreateInstance(passive.GetImplementor());
        } 
        else
        {
            // some passives are display-only
            passiveImplementor = new PassiveAbilityBase();
        }

        passiveImplementor.name = Localize.Passive.Get(passive.GetId())?.name;
        passiveImplementor.desc = Localize.Passive.Get(passive.GetId())?.desc;

        passiveImplementor.rare = passive.rare;

        return passiveImplementor;
    }
}
