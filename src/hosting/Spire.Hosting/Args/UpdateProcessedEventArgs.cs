#region

using System;
using Spire.Core.Abstractions.Processing.Results;

#endregion

namespace Spire.Hosting.Args
{
    public class UpdateProcessedEventArgs : EventArgs
    {
        public IUpdateEntityProcessingResult ProcessingResult { get; }

        public UpdateProcessedEventArgs(IUpdateEntityProcessingResult updateEntityProcessingResult)
        {
            ProcessingResult = updateEntityProcessingResult;
        }
    }
}