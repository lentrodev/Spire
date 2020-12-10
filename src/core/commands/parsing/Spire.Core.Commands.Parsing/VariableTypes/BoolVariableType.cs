#region

using Spire.Core.Commands.Parsing.Abstractions;

#endregion

namespace Spire.Core.Commands.Parsing.VariableTypes
{
    public class BoolVariableType : IVariableType
    {
        public string Pattern => "(true|false)";
        public string Name => "bool";
    }
}