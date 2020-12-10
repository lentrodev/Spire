#region

using System.Collections.Generic;
using Spire.Core.Commands.Parsing.Abstractions;

#endregion

namespace Spire.Core.Commands.Parsing
{
    /// <summary>
    /// Default implementation of <see cref="IVariable"/>.
    /// </summary>
    public sealed class Variable : IVariable
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
        public IDictionary<string, string> Options { get; }

        /// <summary>
        /// Indicated, should be variable type automatically detected or not.
        /// </summary>
        public bool AutoType { get; }

        /// <summary>
        /// Creates new variable with specified name, regex patters, type, and other options.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="originalPattern">Regex pattern.</param>
        /// <param name="type">Variable type.</param>
        /// <param name="isOptional">Is optional.</param>
        /// <param name="autoType">Should be type detected automatically.</param>
        /// <param name="options">Variable options.</param>
        public Variable(string name, string originalPattern, IVariableType type, bool isOptional = false,
            bool autoType = false, IDictionary<string, string> options = null)
        {
            Name = name;
            OriginalPattern = originalPattern;
            Type = type;
            IsOptional = isOptional;
            AutoType = autoType;
            Options = options;
        }

        public bool Equals(IVariable other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Variable) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name != null ? Name.GetHashCode() : 0) * 397;
            }
        }
    }
}