#region

using System;
using System.Reflection;

#endregion

namespace Spire.Core.Abstractions.Processing
{
    /// <summary>
    /// Base interface for implementing descriptor.
    /// </summary>
    /// <typeparam name="TAttribute">Required attribute type.</typeparam>
    public interface IDescriptor<TAttribute> : IIdentifiable<Guid>
        where TAttribute : Attribute
    {
        /// <summary>
        /// Type, <see cref="Method"/> declared in.
        /// </summary>
        Type DeclaringType { get; }

        /// <summary>
        /// Method.
        /// </summary>
        MethodInfo Method { get; }

        /// <summary>
        /// Required attribute.
        /// </summary>
        TAttribute Attribute { get; }
    }
}