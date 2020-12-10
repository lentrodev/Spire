#region

using System;
using System.Reflection;
using Spire.Core.Commands.Abstractions.Processing;
using Spire.Core.Commands.Abstractions.Processing.Attributes;
using Spire.Core.Processing;

#endregion

namespace Spire.Core.Commands.Processing
{
    /// <summary>
    /// Default implementation of <see cref="ICommandHandlerDescriptor{TCommandHandlerAttribute}"/>.
    /// </summary>
    /// <typeparam name="TCommandHandlerAttribute"></typeparam>
    public class CommandHandlerDescriptor<TCommandHandlerAttribute> : Descriptor<TCommandHandlerAttribute>,
        ICommandHandlerDescriptor<TCommandHandlerAttribute>
        where TCommandHandlerAttribute : CommandHandlerAttributeBase
    {
        /// <summary>
        /// Creates new <see cref="CommandHandlerDescriptor{TCommandHandlerAttribute}"/> with specified id, method, declaring type, and required attribute.
        /// </summary>
        /// <param name="id">Descriptor id.</param>
        /// <param name="method">Handler method.</param>
        /// <param name="declaringType">Method declaring type.</param>
        /// <param name="attribute">Required method attribute.</param>
        public CommandHandlerDescriptor(Guid id, MethodInfo method, Type declaringType,
            TCommandHandlerAttribute attribute) : base(id, method, declaringType, attribute)
        {
        }
    }
}