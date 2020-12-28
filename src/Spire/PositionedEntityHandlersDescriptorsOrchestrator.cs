#region

using System.Collections.Generic;
using System.Linq;
using Spire.Attributes;
using Spire.Core.Abstractions.Processing;

#endregion

namespace Spire
{
    public class
        PositionedEntityHandlersDescriptorsOrchestrator : IEntityHandlersOrchestrator<PositionedEntityHandlerAttribute>
    {
        public IEnumerable<IActivatedUpdateEntityHandlerDescriptor<PositionedEntityHandlerAttribute>> Orchestrate(
            IEnumerable<IActivatedUpdateEntityHandlerDescriptor<PositionedEntityHandlerAttribute>>
                unOrchestratedEntityHandlersDescriptors)
        {
            return unOrchestratedEntityHandlersDescriptors.OrderBy(x => x.Attribute.Position);
        }
    }
}