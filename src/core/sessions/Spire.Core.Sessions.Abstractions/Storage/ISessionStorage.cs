#region

using System.Collections.Generic;

#endregion

namespace Spire.Core.Sessions.Abstractions.Storage
{
    /// <summary>
    /// Base interface for implementing session storage.
    /// </summary>
    public interface ISessionStorage
    {
        /// <summary>
        /// Gets all storage entries, related to the current session.
        /// </summary>
        /// <returns>Storage entries.</returns>
        IReadOnlyDictionary<string, object> GetEntries();
        
        #region Add

        /// <summary>
        /// Tries to add entry to the storage.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value</param>
        /// <typeparam name="TValue">Entry value type.</typeparam>
        /// <returns>True, if entry was successfully added.</returns>
        bool Add<TValue>(string key, TValue value);

        #endregion
        
        #region Get

        /// <summary>
        /// Tries to get entry from the storage.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <returns><see cref="object"/>, if entry exists. Otherwise, returns <see langword="null"/>.</returns>
        object Get(string key);
        
        /// <summary>
        /// Tries to get entry from the storage and convert it to <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <typeparam name="TValue">Entry value type.</typeparam>
        /// <returns>Entry value.</returns>
        TValue Get<TValue>(string key);

        #endregion
        
        #region IsExists

        /// <summary>
        /// Checks entry existence.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <returns>Entry existence.</returns>
        bool IsExists(string key);
        
        /// <summary>
        /// Checks entry existence.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="existingValue">Existing value.</param>
        /// <returns>Entry existence.</returns>
        bool IsExists(string key, out object existingValue);
        
        /// <summary>
        /// Checks entry existence.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="existingValue">Existing value.</param>
        /// <typeparam name="TValue">Entry value type.</typeparam>
        /// <returns>Entry existence.</returns>
        bool IsExists<TValue>(string key, out TValue existingValue);

        #endregion
        
        #region Update

        /// <summary>
        /// Tries to update entry value.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value.</param>
        /// <param name="existingValue">Existing value.</param>
        /// <returns>Entry updating result.</returns>
        bool Update(string key, object value, object existingValue);
        
        /// <summary>
        /// Tries to update entry value.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value.</param>
        /// <typeparam name="TValue">Entry value type.</typeparam>
        /// <returns>Entry updating result.</returns>
        bool Update<TValue>(string key, TValue value);

        #endregion

        #region Remove

        /// <summary>
        /// Tries to remove existing entry.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="removedValue">Removed value.</param>
        /// <returns>Entry removing result.</returns>
        bool Remove(string key, out object removedValue);

        /// <summary>
        /// Tries to remove existing entry.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="removedValue">Removed value.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>Entry removing result.</returns>
        bool Remove<TValue>(string key, out TValue removedValue);

        #endregion
    }
}