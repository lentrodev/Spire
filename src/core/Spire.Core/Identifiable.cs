#region

using System.Diagnostics.CodeAnalysis;
using Spire.Core.Abstractions;

#endregion

namespace Spire.Core
{
    /// <summary>
    /// Default implementation of <see cref="IIdentifiable{TId}"/>.
    /// </summary>
    /// <typeparam name="TId">Identifier type.</typeparam>
    public abstract class Identifiable<TId> : IIdentifiable<TId>
        where TId : notnull
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public TId Id { get; }

        /// <summary>
        /// Creates new instance of <see cref="Identifiable{TId}"/> with specified id.
        /// </summary>
        /// <param name="id"></param>
        protected Identifiable([NotNull] TId id)
        {
            Id = id;
        }
    }
}