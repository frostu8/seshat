using System.Collections.Generic;
using LOR_DiceSystem;
using LOR_XML;
using MonoMod;
using Seshat.API;

class patch_BattleCardDescXmlList : BattleCardDescXmlList
{
    // disable unused field warnings, we know what we're doing!
#pragma warning disable CS0169, IDE0051, IDE0044
    [MonoModRemove]
    private Dictionary<int, BattleCardDesc> _dictionary;
#pragma warning restore CS0169, IDE0051, IDE0044

    [MonoModReplace]
    public new void Init(Dictionary<int, BattleCardDesc> dictionary)
    {
        foreach (var card in dictionary.Values)
            DiceCardLocalizeRegistrar.AddVanilla(card);
    }

    [MonoModReplace]
    public new BattleCardDesc GetCardDesc(int cardID)
    {
        BattleCardDesc card = DiceCardLocalizeRegistrar.Get(cardID);
        if (card == null)
        {
            card = new BattleCardDesc();
            card.cardID = cardID;
			card.cardName = "";
			card.behaviourDescList = new List<CardBehaviourDesc>();
			card.behaviourDescList.Add(new CardBehaviourDesc
			{
				behaviourID = -1,
				behaviourDesc = "Unknown"
			});
        }

        return card;
    }

    [MonoModReplace]
    public new string GetCardName(int cardID)
        => DiceCardLocalizeRegistrar.Get(cardID)?.cardName ?? "Not Found";

    [MonoModReplace]
    public new string GetAbilityDesc(int cardID)
        => DiceCardLocalizeRegistrar.Get(cardID)?.ability ?? string.Empty;

    [MonoModReplace]
    public new string GetBehaviourDesc(int cardID, int behaviourIdx)
    {
        return DiceCardLocalizeRegistrar.Get(cardID)?.behaviourDescList
            .Find(x => x.behaviourID == behaviourIdx)?.behaviourDesc 
            ?? string.Empty;

    }
}
