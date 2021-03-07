using System;
using System.Linq;
using System.Collections.Generic;

namespace Seshat {
    /// <summary>
    /// A domain of a Model Registrar.
    ///
    /// If you want to use advanced features, you might want to use this class
    /// directly.
    /// </summary>
    public class ModelRegistrarDomain<T> {
        private Dictionary<int, T> _models = new Dictionary<int, T>();

        /// <summary>
        /// Attempts to retrieve a model with the id specified.
        /// </summary>
        /// <exception cref="KeyNotFoundException">
        /// The item with `id` does not exist in this domain.
        /// </exception>
        public T Get(int id) {
            return _models[id];
        }

        /// <summary>
        /// A version of `Get` that does not throw a `KeyNotFoundException` when
        /// a model is not available.
        /// </summary>
        public bool TryGet(int id, out T model) {
            return _models.TryGetValue(id, out model);
        }

        /// <summary>
        /// Attempts to put a new model in the domain.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// A model with the id `id` already exists in this domain.
        /// </exception>
        public void Add(int id, T model) {
            _models.Add(id, model);
        }

        /// <summary>
        /// Attempts to put a collection of models into the domain.
        /// </summary>
        /// <param name="collection">A enumerable of models to put.</param>
        /// <param name="idSelector">Returns the id of an input model.</param>
        public void AddAll(IEnumerable<T> collection, Func<T, int> idSelector)
        {
            foreach (T model in collection)
                _models.Add(idSelector(model), model);
        }

        /// <summary>
        /// Returns an enumerable of models that satisfy a predicate.
        /// </summary>
        public IEnumerable<T> Select(Func<T, Boolean> predicate) {
            return _models.Values.Where(predicate);
        }

        /// <summary>
        /// Returns an enumerable of all models.
        /// </summary>
        public IEnumerable<T> SelectAll()
        {
            return _models.Values;
        }
    }
}
