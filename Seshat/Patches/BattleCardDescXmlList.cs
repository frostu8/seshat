using System.Collections.Generic;
using LOR_DiceSystem;
using LOR_XML;
using MonoMod;
using Registrar = Seshat.API.Registrar;
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
            Registrar.Localize.CombatPage.AddVanilla(card);
    }

    [MonoModReplace]
    public new BattleCardDesc GetCardDesc(int cardID)
    {
        BattleCardDesc card = Registrar.Localize.CombatPage.Get(cardID);
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
        => Registrar.Localize.CombatPage.Get(cardID)?.cardName ?? "Not Found";

    [MonoModReplace]
    public new string GetAbilityDesc(int cardID)
        => Registrar.Localize.CombatPage.Get(cardID)?.ability ?? string.Empty;

    [MonoModReplace]
    public new string GetBehaviourDesc(int cardID, int behaviourIdx)
    {
        return Registrar.Localize.CombatPage.Get(cardID)?.behaviourDescList
            .Find(x => x.behaviourID == behaviourIdx)?.behaviourDesc 
            ?? string.Empty;

    }
}
