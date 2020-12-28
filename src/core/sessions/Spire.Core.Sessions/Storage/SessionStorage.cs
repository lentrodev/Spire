#region

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Spire.Core.Sessions.Abstractions.Storage;

#endregion

namespace Spire.Core.Sessions.Storage
{
    /// <summary>
    /// Default implementation of <see cref="ISessionStorage"/>.
    /// </summary>
    public class SessionStorage : ISessionStorage
    {
        private readonly ConcurrentDictionary<string, object> _storageEntries;

        /// <summary>
        /// Creates new <see cref="SessionStorage"/>.
        /// </summary>
        public SessionStorage()
        {
            _storageEntries = new ConcurrentDictionary<string, object>();
        }

        /// <summary>
        /// Gets all storage entries, related to the current session.
        /// </summary>
        /// <returns>Storage entries.</returns>
        public IReadOnlyDictionary<string, object> GetEntries()
        {
            return new ReadOnlyDictionary<string, object>(_storageEntries);
        }

        /// <summary>
        /// Tries to add entry to the storage.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value</param>
        /// <typeparam name="TValue">Entry value type.</typeparam>
        /// <returns>True, if entry was successfully added.</returns>
        public bool Add<TValue>(string key, TValue value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return _storageEntries.TryAdd(key, value);
        }

        /// <summary>
        /// Tries to get entry from the storage.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <returns><see cref="object"/>, if entry exists. Otherwise, returns <see langword="null"/>.</returns>
        public object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (_storageEntries.TryGetValue(key, out object value))
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// Tries to get entry from the storage and convert it to <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <typeparam name="TValue">Entry value type.</typeparam>
        /// <returns>Entry value.</returns>
        public TValue Get<TValue>(string key)
        {
            object value = Get(key);

            if (value != null)
            {
                try
                {
                    return (TValue) value;
                }
                catch
                {
                }

                try
                {
                    return (TValue) Convert.ChangeType(value, typeof(TValue));
                }
                catch
                {
                }
            }

            return default;
        }

        /// <summary>
        /// Tries to update entry in the storage.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry new value.</param>
        /// <typeparam name="TValue">Entry value type.</typeparam>
        /// <returns>Updating result.</returns>
        public bool Update<TValue>(string key, TValue value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (_storageEntries.TryGetValue(key, out object existingValue))
            {
                return _storageEntries.TryUpdate(key, value, existingValue);
            }

            return false;
        }

        /// <summary>
        /// Tries to remove entry from the storage.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <returns>Removing result.</returns>
        public bool Remove(string key)
        {
            if (_storageEntries.TryGetValue(key, out object valueToRemove))
            {
                return _storageEntries.TryRemove(key, out object removedValue) && Equals(valueToRemove, removedValue);
            }

            return false;
        }
    }
}