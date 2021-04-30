using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoMod;
using Seshat;

class patch_BookModel : BookModel
{
    private List<PassiveModel> _activatedAllPassives = new List<PassiveModel>();
    private BookXmlInfo _classInfo;

    [MonoModReplace]
    public new List<PassiveAbilityBase> CreatePassiveList()
    {
        // this fills in the passives from the book xml data... nice.
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

    MonoModReplace]
    public new bool TryGainUniquePassive()
    {
        try
        {
            // load any passives the page has
            foreach (var passive in this._classInfo.EquipEffect.FetchPassives())
            {
                // if the passive does not exist already in the list
                if (this._activatedAllPassives.All(p => p.originpassive?.id != passive.id))
                {
                    // ..set it
                    this._activatedAllPassives.Add(new PassiveModel(passive.id, instanceId));
                }
            }
        } 
        catch (Exception e)
        {
            Logger.Error("seshat.keypage", "Failed to load keypage's passives:");
            Logger.Error("seshat.keypage", e.Message);
        }

        return true;
    }
}
