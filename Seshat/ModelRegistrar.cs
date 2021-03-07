using System.Collections.Generic;

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
    public class ModelRegistrar<T> {
        /// <summary>
        /// The domain that contains all vanilla items.
        /// </summary>
        public const string MainDomain = "com.projectmoon.libraryofruina";

        private Dictionary<string, ModelRegistrarDomain<T>> _domains = 
            new Dictionary<string, ModelRegistrarDomain<T>>();

        /// <summary>
        /// Attempts to get the item with the specified `id` from the 
        /// MainDomain.
        /// </summary>
        /// <inheritdoc cref="Get(string, int)"/>
        public T Get(int id) {
            return Get(MainDomain, id);
        }

        /// <summary>
        /// Attempts to get the item with the specified `id` from the specified
        /// `domainId`.
        /// </summary>
        /// <exception cref="KeyNotFoundException">
        /// The item with `id` does not exist in the specified `domainId`.
        /// </exception>
        public T Get(string domainId, int id) {
            if (_domains.TryGetValue(domainId, out var domain)) {
                // we found a domain, so get model and return
                if (domain.TryGet(id, out T model)) {
                    // we found it! return.
                    return model;
                }
            }

            // throw verbose exception if we couldn't find it.
            throw new KeyNotFoundException($"model not found: {domainId}:{id}");
        }

        /// <inheritdoc cref="ModelRegistrarDomain{T}.Add(int, T)"/>
        public void Add(string domainId, int id, T model) {
            GetOrAddDomain(domainId).Add(id, model);
        }

        /// <summary>
        /// Attempts to add an item with the specified `id` to the MainDomain.
        /// </summary>
        /// <inheritdoc cref="Add(string, int, T)"/>
        public void Add(int id, T model)
        {
            Add(MainDomain, id, model);
        }

        /// <summary>
        /// Attempts to get the domain at `domainId`.
        /// </summary>
        /// <exception cref="KeyNotFoundException">
        /// The domain does not exist.
        /// </exception>
        public ModelRegistrarDomain<T> GetDomain(string domainId) {
            return _domains[domainId];
        }

        /// <summary>
        /// Gets the MainDomain, or creates it if it does not exist.
        /// </summary>
        public ModelRegistrarDomain<T> GetMainDomain()
        {
            return GetDomain(MainDomain);
        }

        /// <summary>
        /// An unfailable version of `GetDomain` that simply creates the domain 
        /// if it does not exist already.
        /// </summary>
        public ModelRegistrarDomain<T> GetOrAddDomain(string domainId) {
            ModelRegistrarDomain<T> domain;

            if (!_domains.TryGetValue(domainId, out domain)) {
                // we weren't able to find the domain!
                // make a domain
                domain = new ModelRegistrarDomain<T>();
                _domains.Add(domainId, domain);
            }

            return domain;
        }
    }
}
