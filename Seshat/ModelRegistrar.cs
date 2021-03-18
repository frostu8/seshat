using System;
using System.Collections.Generic;
using System.Linq;
using Seshat.API;

namespace Seshat {
    /// <summary>
    /// A mod-friendly storage of models used in the game.
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

        private static Dictionary<string, T> _models = new Dictionary<string, T>();

        private static Dictionary<int, WeakReference<T>> _intIdentified = new Dictionary<int, WeakReference<T>>();

        /// <summary>
        /// Gets a model by its string ID.
        /// </summary>
        /// <param name="sid">The string id of the model.</param>
        /// <returns>
        /// The model, or null if no matching model could be found.
        /// </returns>
        public static T Get(string sid)
        {
            return _models.TryGetValue(sid, out T value) ? value : null;
        }

        /// <summary>
        /// Gets a model by its ID. Only use this to fetch vanilla models.
        /// </summary>
        /// <param name="id">The id of the model.</param>
        /// <returns>
        /// The model, or null if no matching model could be found.
        /// </returns>
        public static T Get(int id)
        {
            // don't think we need to remove any weakreferences because if the
            // dictionary is written properly it shouldn't need that.
            if (_intIdentified.TryGetValue(id, out WeakReference<T> value))
                if (value.TryGetTarget(out T target))
                    return target;

            return null;
        }

        /// <summary>
        /// Adds a new model to the registrar, throwing if a model already
        /// exists with the same sid.
        /// </summary>
        /// <param name="sid">The string id of the model.</param>
        /// <param name="model">The model.</param>
        /// <exception cref="System.ArgumentException">
        /// A model with the specified string ID already exists.
        /// </exception>
        public static void Add(string sid, T model)
        {
            _models.Add(sid, model);
        }

        /// <summary>
        /// Adds a new model to the registrar, providing a integer Id, throwing
        /// if a model already exists with the same id or sid.
        /// </summary>
        /// <param name="sid">The string id of the model.</param>
        /// <param name="id">The integer id of the model.</param>
        /// <param name="model">The model.</param>
        /// <exception cref="System.ArgumentException">
        /// A model with the specified string ID or integer ID already exists.
        /// </exception>
        public static void Add(string sid, int id, T model)
        {
            _models.Add(sid, model);
            _intIdentified.Add(id, new WeakReference<T>(model));
        }

        internal static void AddVanilla(int id, T model)
            => Add(StringId.Concat(MainDomain, id.ToString()), id, model);

        /// <summary>
        /// Gets all models in a domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> of all models in the domain.
        /// </returns>
        public static IEnumerable<T> ByDomain(string domain)
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
        public static IEnumerable<T> All()
        {
            return _models.Values;
        }
    }
}
