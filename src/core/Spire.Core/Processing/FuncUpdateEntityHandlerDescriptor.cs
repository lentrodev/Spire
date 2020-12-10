#region

using System;
using Spire.Core.Abstractions.Processing.Attributes;

#endregion

namespace Spire.Core.Processing
{
    /// <summary>
    /// Custom, <see cref="Func{TResult}"/>-based update entity handler descriptor.
    /// </summary>
    /// <typeparam name="TUpdateEntityHandlerAttribute">Update entity handler attribute.</typeparam>
    public class
        FuncUpdateEntityHandlerDescriptor<TUpdateEntityHandlerAttribute> : ActivatedUpdateEntityHandlerDescriptor<
            TUpdateEntityHandlerAttribute>
        where TUpdateEntityHandlerAttribute : UpdateEntityHandlerAttributeBase
    {
        /// <summary>
        /// Creates new <see cref="FuncUpdateEntityHandlerDescriptor{TUpdateEntityHandlerAttribute}"/> with specified id, method, and required attribute.
        /// </summary>
        /// <param name="id">Descriptor id.</param>
        /// <param name="method">Method.</param>
        /// <param name="attribute">Required attribute.</param>
        public FuncUpdateEntityHandlerDescriptor(Guid id,
            Delegate method, TUpdateEntityHandlerAttribute attribute)
            : base(id, method.Method, method.Target, method.Method.DeclaringType, attribute)
        {
        }
    }
}