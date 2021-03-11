using System.Collections.Generic;
using System.Linq;
using System;
using LOR_DiceSystem;
using MonoMod;
using Seshat.API;

[MonoModNoNew]
class patch_ItemXmlDataList
{
    // disable unused field warnings, we know what we're doing!
#pragma warning disable CS0169, IDE0051, IDE0044
    [MonoModRemove]
	private List<DiceCardXmlInfo> _cardInfoList;

	[MonoModRemove]
	private List<DiceCardXmlInfo> _basicCardList;

	[MonoModRemove]
	private Dictionary<int, DiceCardXmlInfo> _cardInfoTable;
#pragma warning restore CS0169, IDE0051, IDE0044

    [MonoModReplace]
    public void InitCardInfo_V2(List<DiceCardXmlInfo> list)
    {
        throw new NotImplementedException();
    }

    [MonoModReplace]
    public List<DiceCardXmlInfo> GetCardList()
    {
        throw new NotImplementedException();
    }

    [MonoModReplace]
    public List<DiceCardXmlInfo> GetBasicCardList()
    {
        throw new NotImplementedException();
    }

    [MonoModReplace]
    public DiceCardXmlInfo GetCardItem(int id)
    {
        throw new NotImplementedException();
    }
}
