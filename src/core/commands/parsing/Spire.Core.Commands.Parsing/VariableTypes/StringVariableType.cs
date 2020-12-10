#region

using Spire.Core.Commands.Parsing.Abstractions;

#endregion

namespace Spire.Core.Commands.Parsing.VariableTypes
{
    public class StringVariableType : IVariableType
    {
        public string Pattern => ".+";
        public string Name => "string";
    }
}