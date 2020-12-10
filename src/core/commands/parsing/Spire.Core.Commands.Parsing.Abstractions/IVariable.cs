#region

using System;
using System.Collections.Generic;

#endregion

namespace Spire.Core.Commands.Parsing.Abstractions
{
    /// <summary>
    /// Base interface for implementing variable.
    /// </summary>
    public interface IVariable : IEquatable<IVariable>
    {
        /// <summary>
        /// Variable name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Variable regex pattern. 
        /// </summary>
        public string OriginalPattern { get; }

        /// <summary>
        /// Indicates variable optionality.
        /// </summary>
        public bool IsOptional { get; }

        /// <summary>
        /// Variable type.
        /// </summary>
        public IVariableType Type { get; }

        /// <summary>
        /// Variable options.
        /// </summary>
        IDictionary<string, string> Options { get; }

        /// <summary>
        /// Indicated, should be variable type automatically detected or not.
        /// </summary>
        public bool AutoType { get; }
    }
}