#region

using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion

namespace Spire.Examples.Shared.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ICollection<FeedbackInformation> _feedbacks;

        public FeedbackService()
        {
            _feedbacks = new Collection<FeedbackInformation>();
        }

        public IEnumerable<FeedbackInformation> GetFeedbacks()
        {
            return _feedbacks;
        }

        public void ProvideFeedback(int feedbackIdentifier, string feedbackMessage)
        {
            FeedbackInformation feedbackInformation = new FeedbackInformation(feedbackIdentifier, feedbackMessage);

            _feedbacks.Add(feedbackInformation);
        }
    }
}