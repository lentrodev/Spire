#region

using System;

#endregion

namespace Spire.Hosting.Args
{
    public class ErrorOccuredEventArgs : EventArgs
    {
        public Exception Exception { get; }

        public ErrorOccuredEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}