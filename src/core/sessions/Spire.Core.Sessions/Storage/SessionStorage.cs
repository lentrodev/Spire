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

        public bool Add<TValue>(string key, TValue value)
        {
            return _storageEntries.TryAdd(key, value);
        }

        public object Get(string key)
        {
            if (_storageEntries.TryGetValue(key, out object existingValue))
            {
                return existingValue;
            }

            return default;
        }

        public TValue Get<TValue>(string key)
        {
            try
            {
                return (TValue) Get(key);
            }
            catch{}
            
            try
            {
                return (TValue) Convert.ChangeType(Get(key), typeof(TValue));
            }
            catch{}

            return default;
        }

        public bool IsExists(string key)
        {
            return _storageEntries.ContainsKey(key);
        }

        public bool IsExists(string key, out object existingValue)
        {
            return _storageEntries.TryGetValue(key, out existingValue);
        }

        public bool IsExists<TValue>(string key, out TValue existingValue)
        {
            bool existence = IsExists(key, out object existingValueObject);

            try
            {
                existingValue = (TValue) existingValueObject;
                return true;
            }
            catch { }
                
            try
            {
                existingValue = (TValue) Convert.ChangeType(existingValueObject, typeof(TValue));
                return true;
            }
            catch { }

            existingValue = default;
            return existence;
        }

        public bool Update(string key, object value, object existingValue)
        {
            return _storageEntries.TryUpdate(key, value, existingValue);
        }

        public bool Update<TValue>(string key, TValue value)
        {
            if (_storageEntries.TryGetValue(key, out object existingValue))
            {
                return Update(key, value, existingValue);
            }
            return false;
        }
        public bool Remove(string key, out object removedValue)
        {
            return _storageEntries.TryRemove(key, out removedValue);
        }

        public bool Remove<TValue>(string key, out TValue removedValue)
        {
            if (Remove(key, out object removedValueObject))
            {
                try
                {
                    removedValue = (TValue) removedValueObject;
                    return true;
                }
                catch { }
                
                try
                {
                    removedValue = (TValue) Convert.ChangeType(removedValueObject, typeof(TValue));
                    return true;
                }
                catch { }
            }
            
            removedValue = default;
            return false;
        }
    }
}