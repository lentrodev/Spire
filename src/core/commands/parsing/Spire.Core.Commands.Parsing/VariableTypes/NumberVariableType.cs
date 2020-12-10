#region

using Spire.Core.Commands.Parsing.Abstractions;

#endregion

namespace Spire.Core.Commands.Parsing.VariableTypes
{
    public class NumberVariableType : IVariableType
    {
        public string Pattern => "([0]{1}|-?[1-9]{1}[0-9]{0,18})";
        public string Name => "number";
    }
}