#region

using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Sessions.Abstractions.Storage
{
    /// <summary>
    /// Base interface for implementing session storage manager.
    /// </summary>
    public interface ISessionStorageManager
    {
        /// <summary>
        /// Storage manager mode.
        /// </summary>
        SessionStorageMode Mode { get; }

        /// <summary>
        /// Gets all existing storages.
        /// </summary>
        /// <returns>Existing storages.</returns>
        IReadOnlyDictionary<SessionStorageDescriptor, ISessionStorage> GetStorages();

        /// <summary>
        /// Gets or creates session storage.
        /// </summary>
        /// <param name="owner">Session owner.</param>
        /// <param name="entityType">Session entity type.</param>
        /// <typeparam name="TEntity">Session entity type.</typeparam>
        /// <returns>Session storage.</returns>
        ISessionStorage GetOrCreateStorage<TEntity>(User owner, UpdateType entityType);
    }
}