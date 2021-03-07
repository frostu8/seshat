using LOR_DiceSystem;

using MonoMod;

namespace Seshat.API
{
    /// <summary>
    /// A specialization of <see cref="ModelRegistrar{T}"/> for
    /// <see cref="DiceCardXmlInfo"/>.
    /// 
    /// <para>
    /// Like most models in Seshat, combat pages are stored in a special
    /// registrar. This class introduces some helper methods to make modding
    /// easier.
    /// </para>
    /// </summary>
    public class DiceCardRegistrar : ModelRegistrar<DiceCardXmlInfo>
    { 

    }
}
