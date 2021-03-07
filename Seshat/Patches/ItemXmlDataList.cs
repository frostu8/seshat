using System.Collections.Generic;
using System.Linq;
using LOR_DiceSystem;
using MonoMod;
using Seshat.API;

[MonoModPatch("ItemXmlDataList")]
public class ItemXmlDataList
{
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
