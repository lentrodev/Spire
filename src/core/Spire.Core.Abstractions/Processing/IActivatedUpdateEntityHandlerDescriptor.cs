#region

using Spire.Core.Abstractions.Processing.Attributes;

#endregion

namespace Spire.Core.Abstractions.Processing
{
    /// <summary>
    /// Base interface for implementing activated update entity handler descriptor.
    /// </summary>
    /// <typeparam name="TUpdateEntityHandlerAttribute">Update entity handler attribute.</typeparam>
    public interface IActivatedUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute> :
        IUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>,
        IActivatedDescriptor<TUpdateEntityHandlerAttribute>
        where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
    {
    }
}