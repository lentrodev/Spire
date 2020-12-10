#region

using System;
using System.Reflection;
using Spire.Core.Abstractions.Processing;
using Spire.Core.Abstractions.Processing.Attributes;

#endregion

namespace Spire.Core.Processing
{
    /// <summary>
    /// Update entity handler descriptor.
    /// </summary>
    /// <typeparam name="TUpdateEntityHandlerAttribute">Update entity handler attribute.</typeparam>
    public class UpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute> :
        Descriptor<TUpdateEntityHandlerAttribute>, IUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>
        where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
    {
        /// <summary>
        /// Creates new <see cref="UpdateEntityHandlerDescriptor{TUpdateEntityHandlerAttribute}"/> with specified id, method, declaring type and required attribute.
        /// </summary>
        /// <param name="id">Descriptor identifier.</param>
        /// <param name="method">Method.</param>
        /// <param name="declaringType">Type, <see cref="Descriptor{TUpdateEntityHandlerAttribute}.Method"/> declared in.</param>
        /// <param name="attribute">Required attribute</param>
        public UpdateEntityHandlerDescriptor(Guid id, MethodInfo method, Type declaringType,
            TUpdateEntityHandlerAttribute attribute) : base(id, method, declaringType, attribute)
        {
        }
    }
}