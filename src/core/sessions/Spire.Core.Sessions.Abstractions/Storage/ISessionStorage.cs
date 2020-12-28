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

        /// <summary>
        /// Tries to add entry to the storage.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value</param>
        /// <typeparam name="TValue">Entry value type.</typeparam>
        /// <returns>True, if entry was successfully added.</returns>
        bool Add<TValue>(string key, TValue value);

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

        /// <summary>
        /// Tries to update entry in the storage.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry new value.</param>
        /// <typeparam name="TValue">Entry value type.</typeparam>
        /// <returns>Updating result.</returns>
        bool Update<TValue>(string key, TValue value);

        /// <summary>
        /// Tries to remove entry from the storage.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <returns>Removing result.</returns>
        bool Remove(string key);
    }
}