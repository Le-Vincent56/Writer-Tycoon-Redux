using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.Ideation.Review;

namespace WriterTycoon.WorkCreation.Ideation.TimeEstimation
{
    public class TimeEstimationPayload : Payload<int>
    {
        public override int Content { get; set; }
        public override void Visit<T>(T visitable)
        {
            if (visitable is IdeaReviewer ideaReviewer) ideaReviewer.SetTimeEstimate(Content);
            if (visitable is WorkTracker workTracker) workTracker.SetTimeEstimate(Content);
        }
    }
}
