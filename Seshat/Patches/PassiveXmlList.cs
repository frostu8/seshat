using System;
using System.Collections.Generic;
using MonoMod;

class patch_PassiveXmlList : PassiveXmlList
{
    // disable unused field warnings, we know what we're doing!
#pragma warning disable CS0169
    [MonoModRemove]
    private List<PassiveXmlInfo> _list;
#pragma warning restore CS0169

    [MonoModReplace]
    public new void Init(List<PassiveXmlInfo> list)
    {
        foreach (var passive in list)
        {
            Type passiveType = Type.GetType("PassiveAbility_" + passive.id);

            if (passiveType != null)
            {
                passive.SetImplementor(passiveType);
            }

            Seshat.API.Registrar.Passive.AddVanilla(passive);
        }
    }

    [MonoModReplace]
    public new PassiveXmlInfo GetData(int id)
        => Seshat.API.Registrar.Passive.Get(id);

    [MonoModRemove]
    public new PassiveXmlInfo GetRandomPassive(int level)
        => throw new NotImplementedException("patch");

    [MonoModRemove]
    public new List<PassiveXmlInfo> GetDataAll()
        => throw new NotImplementedException("patch");

    [MonoModConstructor]
    [MonoModReplace]
    public void ctor() { }
}
