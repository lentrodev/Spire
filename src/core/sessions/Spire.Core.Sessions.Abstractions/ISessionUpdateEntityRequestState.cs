#region

using System.Collections.Generic;

#endregion

namespace Spire.Core.Sessions.Abstractions
{
    /// <summary>
    /// Base interface for implementing session update entity request state.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface ISessionUpdateEntityRequestState<TEntity>
    {
        /// <summary>
        /// Current request attempt.
        /// </summary>
        int CurrentAttempt { get; }

        /// <summary>
        /// Last entity.
        /// </summary>
        TEntity LastEntity { get; }

        /// <summary>
        /// Collection of entities from previous request steps.
        /// </summary>
        IReadOnlyCollection<TEntity> PreviousEntities { get; }
    }
}