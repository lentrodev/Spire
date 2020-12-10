#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Spire.Core.Sessions.Abstractions;

#endregion

namespace Spire.Core.Sessions
{
    /// <summary>
    /// Base implementation of <see cref="ISessionUpdateEntityRequestState{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public class SessionUpdateEntityRequestState<TEntity> : ISessionUpdateEntityRequestState<TEntity>
    {
        /// <summary>
        /// Current request attempt.
        /// </summary>
        public int CurrentAttempt { get; private set; }

        /// <summary>
        /// Last entity.
        /// </summary>
        public TEntity LastEntity => PreviousEntities.Last();

        /// <summary>
        /// Collection of entities from previous request steps.
        /// </summary>
        public IReadOnlyCollection<TEntity> PreviousEntities => new ReadOnlyCollection<TEntity>(_previousEntities);

        private readonly IList<TEntity> _previousEntities;

        /// <summary>
        /// Creates new session update entity request state.
        /// </summary>
        public SessionUpdateEntityRequestState()
        {
            _previousEntities = new List<TEntity>();
        }

        internal void Next(TEntity entity)
        {
            _previousEntities.Add(entity);
            CurrentAttempt++;
        }
    }
}