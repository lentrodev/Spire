#region

using Spire.Core.Commands.Parsing.Abstractions;

#endregion

namespace Spire.Core.Commands.Parsing.VariableTypes
{
    public class DoubleVariableType : IVariableType
    {
        public string Pattern => @"-?\d+(?:\.\d+)?";
        public string Name => "double";
    }
}