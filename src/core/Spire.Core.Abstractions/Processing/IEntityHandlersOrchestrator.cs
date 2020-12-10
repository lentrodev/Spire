#region

using System.Collections.Generic;
using Spire.Core.Abstractions.Processing.Attributes;

#endregion

namespace Spire.Core.Abstractions.Processing
{
    /// <summary>
    /// Base interface for implementing entity handler descriptors orchestration (sorting).
    /// </summary>
    /// <typeparam name="TUpdateEntityHandlerAttribute">Update entity handler attribute.</typeparam>
    public interface IEntityHandlersOrchestrator<TUpdateEntityHandlerAttribute>
        where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
    {
        /// <summary>
        /// Orchestrates (sorts) update entity handlers descriptors.
        /// </summary>
        /// <param name="unOrchestratedEntityHandlersDescriptors">Collection of unsorted update entity handlers descriptors.</param>
        /// <returns>Collection of unsorted update entity handlers descriptors.</returns>
        IEnumerable<IActivatedUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>> Orchestrate(
            IEnumerable<IActivatedUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>>
                unOrchestratedEntityHandlersDescriptors);
    }
}