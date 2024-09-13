using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Review;

namespace WriterTycoon.WorkCreation.TimeEstimation
{
    public class TimeEstimationPayload : Payload<int>
    {
        public override int Content { get; set; }
        public override void Visit<T>(T visitable)
        {
            if (visitable is IdeaReviewer ideaReviewer) ideaReviewer.SetTimeEstimate(Content);
        }
    }
}
