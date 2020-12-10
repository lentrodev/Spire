#region

using System;
using System.Reflection;
using Spire.Core.Abstractions.Processing;

#endregion

namespace Spire.Core.Processing
{
    /// <summary>
    /// Default implementation of <see cref="IDescriptor{TAttribute}"/>.
    /// </summary>
    /// <typeparam name="TAttribute">Required attribute type.</typeparam>
    public abstract class Descriptor<TAttribute> : Identifiable<Guid>, IDescriptor<TAttribute>
        where TAttribute : Attribute
    {
        /// <summary>
        /// Type, <see cref="Method"/> declared in.
        /// </summary>
        public Type DeclaringType { get; }

        /// <summary>
        /// Method.
        /// </summary>
        public MethodInfo Method { get; }

        /// <summary>
        /// Required attribute.
        /// </summary>
        public TAttribute Attribute { get; }

        /// <summary>
        /// Creates new <see cref="Descriptor{TAttribute}"/> with specified id, method, declaring type, and required attribute.
        /// </summary>
        /// <param name="id">Descriptor identifier.</param>
        /// <param name="method">Method.</param>
        /// <param name="declaringType">Type, <see cref="Method"/> declared in.</param>
        /// <param name="attribute">Required attribute.</param>
        protected Descriptor(Guid id, MethodInfo method, Type declaringType, TAttribute attribute) : base(id)
        {
            Method = method;

            DeclaringType = declaringType;

            Attribute = attribute;
        }
    }
}