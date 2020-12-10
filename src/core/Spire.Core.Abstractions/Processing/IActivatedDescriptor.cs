#region

using System;

#endregion

namespace Spire.Core.Abstractions.Processing
{
    /// <summary>
    /// Base interface for implementing activated descriptor.
    /// </summary>
    /// <typeparam name="TAttribute">Required attribute type.</typeparam>
    public interface IActivatedDescriptor<TAttribute> : IDescriptor<TAttribute>
        where TAttribute : Attribute
    {
        /// <summary>
        /// Instance of <see cref="IDescriptor{TAttribute}.DeclaringType"/>.
        /// </summary>
        object DeclaringTypeInstance { get; }
    }
}