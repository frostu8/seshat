using System.Collections.Generic;
using System.Linq;
using System;
using LOR_DiceSystem;
using MonoMod;
using Seshat.API;

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
        list.ForEach(card => DiceCardRegistrar.Register(card));
    }

    [MonoModReplace]
    public List<DiceCardXmlInfo> GetCardList()
    {
        // References to this function are as follows:
        // CardListViewer.Start();
        // CardResourceManager.InitializeAllArtworks();
        // might want to go ahead and just patch them but i'm lazy
        return DiceCardRegistrar.All().ToList();
    }

    [MonoModReplace]
    public List<DiceCardXmlInfo> GetBasicCardList()
    {
        return DiceCardRegistrar.All()
            .Where(card => card.optionList.Contains(CardOption.Basic))
            .ToList();
    }

    [MonoModReplace]
    public DiceCardXmlInfo GetCardItem(int id)
    {
        return DiceCardRegistrar.Get(id);
    }
}
