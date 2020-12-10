#region

using Spire.Core.Abstractions.Processing;
using Spire.Core.Commands.Abstractions.Processing.Attributes;

#endregion

namespace Spire.Core.Commands.Abstractions.Processing
{
    /// <summary>
    /// Base interface for implementing activated command handler descriptor.
    /// </summary>
    /// <typeparam name="TCommandHandlerAttribute">Command handler attribute.</typeparam>
    public interface IActivatedCommandHandlerDescriptor<TCommandHandlerAttribute> :
        ICommandHandlerDescriptor<TCommandHandlerAttribute>, IActivatedDescriptor<TCommandHandlerAttribute>
        where TCommandHandlerAttribute : CommandHandlerAttributeBase
    {
    }
}