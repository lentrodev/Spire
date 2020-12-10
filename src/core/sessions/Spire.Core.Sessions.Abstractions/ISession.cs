#region

using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

#endregion

namespace Spire.Core.Sessions.Abstractions
{
    /// <summary>
    /// Base interface for implementing session
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Session owner.
        /// </summary>
        User Owner { get; }

        /// <summary>
        /// Session entity type.
        /// </summary>
        UpdateType EntityType { get; }
    }

    /// <summary>
    /// Base interface for implementing session
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface ISession<TEntity> : ISession
    {
        /// <summary>
        /// Indicated update entity request status.
        /// </summary>
        bool IsUpdateEntityRequested { get; }

        /// <summary>
        /// Channel writer, used to writing received entities if <see cref="IsUpdateEntityRequested"/> is set to <see langword="true"/>.
        /// </summary>
        ChannelWriter<TEntity> UpdateEntityChannelWriter { get; }

        /// <summary>
        /// Requests an entity.
        /// </summary>
        /// <param name="sessionRequestOptions">Session entity request options.</param>
        /// <param name="cancellationToken">Cancellation token for task cancellation.</param>
        /// <returns>Requested entity.</returns>
        ValueTask<ISessionUpdateEntityRequestResult<TEntity>> RequestEntityAsync(
            ISessionUpdateEntityRequestOptions<TEntity> sessionRequestOptions,
            CancellationToken cancellationToken = default);
    }
}