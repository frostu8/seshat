using System;
using System.Collections.Generic;
using MonoMod;
using Seshat;

class patch_GiftModel : GiftModel
{
#pragma warning disable CS0649
    private GiftXmlInfo _classInfo;
#pragma warning restore CS0649

    public patch_GiftModel(GiftXmlInfo classInfo) : base(classInfo) { }

    public new List<PassiveAbilityBase> CreateScripts()
    {
        List<PassiveAbilityBase> passives = new List<PassiveAbilityBase>();

        foreach (var script in _classInfo.ScriptList)
        {
            PassiveXmlInfo passive = Seshat.API.Registrar.Passive.Get(script);
            if (passive == null)
            {
                Logger.Error("seshat.gift", $"Passive with id {script} not found.");
                continue;
            }

            try
            {
                PassiveAbilityBase ability = Seshat.API.Registrar.Passive.GetNew(passive);
                ability.name = GetName();
                ability.desc = GiftDesc;
                passives.Add(ability);
            }
            catch (Exception e)
            {
                Logger.Error("seshat.gift", $"Failed to add passive {passive.GetId()}:");
                Logger.Error("seshat.gift", e.Message);
            }
        }

        return passives;
    }
}
