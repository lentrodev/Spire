#region

using System;
using System.Reflection;
using Spire.Core.Commands.Abstractions.Processing;
using Spire.Core.Commands.Abstractions.Processing.Attributes;

#endregion

namespace Spire.Core.Commands.Processing
{
    /// <summary>
    /// Default implementation of <see cref="IActivatedCommandHandlerDescriptor{TCommandHandlerAttribute}"/>.
    /// </summary>
    /// <typeparam name="TCommandHandlerAttribute">Command handler attribute.</typeparam>
    public class ActivatedCommandHandlerDescriptor<TCommandHandlerAttribute> :
        CommandHandlerDescriptor<TCommandHandlerAttribute>, IActivatedCommandHandlerDescriptor<TCommandHandlerAttribute>
        where TCommandHandlerAttribute : CommandHandlerAttributeBase
    {
        /// <summary>
        /// Instance of <see cref="IActivatedCommandHandlerDescriptor{TCommandHandlerAttribute}.DeclaringType"/>.
        /// </summary>
        public object DeclaringTypeInstance { get; }

        /// <summary>
        /// Creates new <see cref="ActivatedCommandHandlerDescriptor{TCommandHandlerAttribute}"/> with specified id, method, declaring type and declaring type instance, and required attribute.
        /// </summary>
        /// <param name="id">Descriptor id.</param>
        /// <param name="method">Handler method.</param>
        /// <param name="declaringTypeInstance">Declaring type instance.</param>
        /// <param name="declaringType">Method declaring type.</param>
        /// <param name="attribute">Required method attribute.</param>
        public ActivatedCommandHandlerDescriptor(Guid id, MethodInfo method, object declaringTypeInstance,
            Type declaringType, TCommandHandlerAttribute attribute) : base(id, method, declaringType, attribute)
        {
            DeclaringTypeInstance = declaringTypeInstance;
        }
    }
}