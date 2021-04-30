using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Registrar = Seshat.API.Registrar;

class patch_BookEquipEffect : BookEquipEffect
{
    [XmlElement("ModPassive")]
    public List<string> passiveModList = new List<string>();
}

public static class BookEquipEffectExt
{
    /// <summary>
    /// Gets a resolved list of all of the passives this equip effect carries.
    /// 
    /// <para>
    /// The returned iterator will throw <see cref="InvalidOperationException"/>
    /// if a passive is invalid.
    /// </para>
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> of passives.</returns>
    public static IEnumerable<PassiveXmlInfo> FetchPassives(this BookEquipEffect equip)
    {
        return equip.PassiveList
            .Select((passive) => {
                PassiveXmlInfo p = Registrar.Passive.Get(passive.passiveId);

                if (p == null) throw new ArgumentException(
                    $"Passive {passive.passiveId} does not exist.");
                else return p;
            })
            .Concat(
                ((patch_BookEquipEffect)equip).passiveModList
                    .Select((passiveId) => {
                        PassiveXmlInfo p = Registrar.Passive.Get(passiveId);

                        if (p == null) throw new ArgumentException(
                            $"Passive \"{passiveId}\" does not exist.");
                        else return p;
                    })
            );
    }
}
