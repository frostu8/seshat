using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoMod;
using Seshat;

class patch_BookModel : BookModel
{
    [MonoModReplace]
    public new List<PassiveAbilityBase> CreatePassiveList()
    {
        TryGainUniquePassive();

        List<PassiveAbilityBase> passives = new List<PassiveAbilityBase>();
        foreach (var passive in GetPassiveInfoList())
        {
            if (passive.passive.isLock || !passive.isActivated)
                continue;

            try
            {
                passives.Add(Seshat.API.Registrar.Passive.GetNew(passive.passive));
            }
            catch (Exception e)
            {
                Logger.Error("seshat.keypage", $"Failed to add passive {passive.passive.GetId()}:");
                Logger.Error("seshat.keypage", e.Message);
            }
        }

        return passives;
    }
}
