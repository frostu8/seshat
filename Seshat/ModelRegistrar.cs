using System.Collections.Generic;
using System.Linq;

namespace Seshat {
    /// <summary>
    /// A mod-friendly storage of models used in the game.
    ///
    /// <para>
    /// This applies to books, combat pages, equip pages, gifts (battle symbols)
    /// and others. This class namespaces these models to prevent unnecessary id
    /// clashes and other nonsense.
    /// </para>
    /// </summary>
    public class ModelRegistrar<T> where T: class {
        /// <summary>
        /// The domain that contains all vanilla items.
        /// </summary>
        public const string MainDomain = "com.projectmoon.libraryofruina";

        private Dictionary<string, T> _models = new Dictionary<string, T>();

        /// <summary>
        /// Gets a model by its string ID.
        /// </summary>
        /// <param name="sid">The string id of the model.</param>
        /// <returns>
        /// The model, or null if no matching model could be found.
        /// </returns>
        public T Get(string sid)
        {
            return _models.TryGetValue(sid, out T value) ? value : null;
        }

        /// <summary>
        /// Adds a new model to the registrar.
        /// </summary>
        /// <param name="sid">The string id of the model.</param>
        /// <param name="model">The model.</param>
        /// <exception cref="System.ArgumentException">
        /// A model with the specified string ID already exists.
        /// </exception>
        public void Add(string sid, T model)
        {
            _models.Add(sid, model);
        }

        /// <summary>
        /// Gets all models in a domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> of all models in the domain.
        /// </returns>
        public IEnumerable<T> ByDomain(string domain)
        {
            return _models
                .Where(kvp => StringId.InDomain(kvp.Key, domain))
                .Select(kvp => kvp.Value);
        }

        /// <summary>
        /// Gets all models in the registrar.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> of all models in the domain.
        /// </returns>
        public IEnumerable<T> All()
        {
            return _models.Values;
        }
    }
}
