#region

using Spire.Core.Abstractions.Processing;
using Spire.Core.Commands.Abstractions.Processing.Attributes;

#endregion

namespace Spire.Core.Commands.Abstractions.Processing
{
    /// <summary>
    /// Base interface for implementing command handler descriptor.
    /// </summary>
    /// <typeparam name="TCommandHandlerAttribute">Command handler attribute.</typeparam>
    public interface ICommandHandlerDescriptor<TCommandHandlerAttribute> : IDescriptor<TCommandHandlerAttribute>
        where TCommandHandlerAttribute : CommandHandlerAttributeBase
    {
    }
}