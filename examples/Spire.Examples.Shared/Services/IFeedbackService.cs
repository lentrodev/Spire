#region

using System.Collections.Generic;

#endregion

namespace Spire.Examples.Shared.Services
{
    public interface IFeedbackService
    {
        IEnumerable<FeedbackInformation> GetFeedbacks();

        void ProvideFeedback(int feedbackIdentifier, string feedbackMessage);
    }

    public class FeedbackInformation
    {
        public int Identifier { get; }

        public string Message { get; }

        public FeedbackInformation(int identifier, string message)
        {
            Identifier = identifier;
            Message = message;
        }
    }
}