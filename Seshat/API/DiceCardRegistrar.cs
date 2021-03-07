using LOR_DiceSystem;

using MonoMod;

namespace Seshat.API
{
    [MonoModPatch("DiceCardRegistrar")]
    public class DiceCardRegistrar : Singleton<ModelRegistrar<DiceCardXmlInfo>>
    { }
}
