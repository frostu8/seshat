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

        protected static Dictionary<int, T> _models = new Dictionary<int, T>();

        protected static Dictionary<string, T> _modelsBySid = new Dictionary<string, T>();

        /// <summary>
        /// Gets a model by its string ID.
        /// </summary>
        /// <param name="sid">The string id of the model.</param>
        /// <returns>
        /// The model, or null if no matching model could be found.
        /// </returns>
        public static T Get(string sid)
        {
            return _modelsBySid.TryGetValue(sid, out T value) ? value : null;
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
            return _models.TryGetValue(id, out T value) ? value : null;
        }

        /// <exception cref="ArgumentException">Id already taken.</exception>
        internal static void AddVanilla(int id, T model)
        {
            _models.Add(id, model);
        }

        /// <exception cref="ArgumentException">Id already taken.</exception>
        internal static void Add(int id, string sid, T model)
        {
            _models.Add(id, model);
            _modelsBySid.Add(sid, model);
        }

        /// <summary>
        /// Gets all models in a domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> of all models in the domain.
        /// </returns>
        public static IEnumerable<T> ByDomain(string domain)
        {
            return _modelsBySid
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
