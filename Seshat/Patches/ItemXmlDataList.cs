using System.Collections.Generic;
using System.Linq;
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
        Singleton<DiceCardRegistrar>.Instance.GetMainDomain()
            .AddAll(list, card => card.id);
    }

    [MonoModReplace]
    public List<DiceCardXmlInfo> GetCardList()
    {
        return Singleton<DiceCardRegistrar>.Instance.GetMainDomain()
            .SelectAll().ToList();
    }

    [MonoModReplace]
    public List<DiceCardXmlInfo> GetBasicCardList()
    {
        return Singleton<DiceCardRegistrar>.Instance.GetMainDomain()
            .Select(card => card.optionList.Contains(CardOption.Basic))
            .ToList();
    }

    [MonoModReplace]
    public DiceCardXmlInfo GetCardItem(int id)
    {
        return Singleton<DiceCardRegistrar>.Instance.GetMainDomain().Get(id);
    }
}
