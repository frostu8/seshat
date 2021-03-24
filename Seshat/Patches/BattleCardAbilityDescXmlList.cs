using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOR_XML;
using MonoMod;
using Registrar = Seshat.API.Registrar;
using Seshat.API;

[RemoveConstructor]
class patch_BattleCardAbilityDescXmlList : BattleCardAbilityDescXmlList
{
    // disable unused field warnings, we know what we're doing!
    // And no, we really don't need a keyword cache.
#pragma warning disable CS0169
    [MonoModRemove]
    private Dictionary<string, BattleCardAbilityDesc> _dictionary;

    [MonoModRemove]
    private Dictionary<string, List<string>> _dictionaryKeywordCache = new Dictionary<string, List<string>>();
#pragma warning restore CS0169

    [MonoModReplace]
    public new void Init(Dictionary<string, BattleCardAbilityDesc> dictionary)
    {
        // also append vanilla domain so the definitions match up in
        // DiceCardAbilityRegistrar
        foreach (var desc in dictionary.Values)
        {
            // TODO: write this so that the presence of abilities are checked,
            // instead of making two copies. Not too much overhead, but still
            // annoying to fall asleep at night knowing this exists.
            Registrar.Localize.DiceAbility.AddVanilla(desc.Clone());
            Registrar.Localize.CardAbility.AddVanilla(desc);
        }
    }

    [MonoModReplace]
    public new List<string> GetAbilityDesc(string id)
    {
        BattleCardAbilityDesc desc = GetGenericAbility(id);

        // we need to make a copy of this list
        if (desc != null)
            return new List<string>(desc.desc);
        else
            return new List<string>();
    }

    // PMoon mixed card and dice abilities around so we have to try both of the
    // registrars until we find a match. We might need to patch this in the
    // future.
    private BattleCardAbilityDesc GetGenericAbility(string id)
    {
        return
            Registrar.Localize.DiceAbility.Get(id) ??
            Registrar.Localize.CardAbility.Get(id);
    }

    [MonoModReplace]
    public new List<string> GetAbilityKeywords(string scriptName)
    {
        // TODO: fix this please :(
        return new List<string>();
    }
}
