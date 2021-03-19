using LOR_DiceSystem;

using MonoMod;

namespace Seshat.API
{
    /// <summary>
    /// A specialization of <see cref="ModelRegistrar{T}"/> for
    /// <see cref="DiceCardXmlInfo"/>.
    /// </summary>
    public class DiceCardRegistrar : ModelRegistrar<DiceCardXmlInfo>
    { 
        /// <summary>
        /// Directly registers a card to the registrar, giving it an unallocated
        /// number ID.
        /// </summary>
        /// <param name="card">The card to register.</param>
        /// <exception cref="System.ArgumentException">
        /// The string id has already been taken.
        /// </exception>
        /// <remarks>
        /// This function does not perform any domain translations.
        /// </remarks>
        public static void Register(DiceCardXmlInfo card)
        {
            int generatedId = IdGen.NextFree(id => !_models.ContainsKey(id));

            card.id = generatedId;
            Add(generatedId, card.GetSID(), card);
        }
    }
}
