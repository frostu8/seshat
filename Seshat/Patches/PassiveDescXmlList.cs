using System.Collections.Generic;
using LOR_XML;
using MonoMod;

using Registrar = Seshat.API.Registrar;

class patch_PassiveDescXmlList : PassiveDescXmlList
{
#pragma warning disable CS0649
    [MonoModRemove]
    private Dictionary<int, PassiveDesc> _dictionary = new Dictionary<int, PassiveDesc>();

    [MonoModRemove]
    private System.Random random = new System.Random();
#pragma warning restore CS0649

    [MonoModReplace]
    public new void Init(PassiveDescRoot descRoot)
        => Add(descRoot);

    [MonoModReplace]
    public new void Add(PassiveDescRoot descRoot)
    {
        foreach (var desc in descRoot.descList)
            Registrar.Localize.Passive.AddVanilla(desc);
    }

    [MonoModReplace]
    public new string GetName(int id)
        => Registrar.Localize.Passive.Get(id)?.name ?? string.Empty;

    [MonoModReplace]
    public new string GetDesc(int id)
        => Registrar.Localize.Passive.Get(id)?.desc ?? string.Empty;

    [MonoModConstructor]
    [MonoModReplace]
    public void ctor() { }
}

