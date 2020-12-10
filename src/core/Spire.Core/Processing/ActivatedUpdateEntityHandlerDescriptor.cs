#region

using System;
using System.Reflection;
using Spire.Core.Abstractions.Processing;
using Spire.Core.Abstractions.Processing.Attributes;

#endregion

namespace Spire.Core.Processing
{
    /// <summary>
    /// Activated update entity handler descriptor.
    /// </summary>
    /// <typeparam name="TUpdateEntityHandlerAttribute">Update entity handler attribute.</typeparam>
    public class ActivatedUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute> :
        UpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>,
        IActivatedUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute>
        where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
    {
        /// <summary>
        /// Declaring type instance.
        /// </summary>
        public object DeclaringTypeInstance { get; }

        /// <summary>
        /// Creates new <see cref="ActivatedUpdateEntityHandlerDescriptor{TUpdateEntityHandlerAttribute}"/> with specified id, method, declaring type, declaring type instance, and required attribute.
        /// </summary>
        /// <param name="id">Descriptor identifier.</param>
        /// <param name="method">Method.</param>
        /// <param name="declaringTypeInstance">Instance of <see cref="Descriptor{TAttribute}.DeclaringType"/>.</param>
        /// <param name="declaringType">Type, <see cref="UpdateEntityHandlerDescriptor{TUpdateEntityHandlerAttribute}.Method"/> declared in.</param>
        /// <param name="attribute">Required attribute.</param>        
        public ActivatedUpdateEntityHandlerDescriptor(Guid id, MethodInfo method, object declaringTypeInstance,
            Type declaringType, TUpdateEntityHandlerAttribute attribute) : base(id, method, declaringType, attribute)
        {
            DeclaringTypeInstance = declaringTypeInstance;
        }
    }
}