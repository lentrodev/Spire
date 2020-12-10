#region

using Spire.Core.Abstractions.Processing.Attributes;

#endregion

namespace Spire.Core.Abstractions.Processing
{
    /// <summary>
    /// Base interface for implementing update entity handler descriptor.
    /// </summary>
    /// <typeparam name="TUpdateEntityHandlerAttribute">Update entity handler attribute.</typeparam>
    public interface
        IUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute> : IDescriptor<TUpdateEntityHandlerAttribute>
        where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
    {
    }
}