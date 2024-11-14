using GhostWriter.Patterns.Mediator;
using GhostWriter.WorkCreation.Development.Tracker;
using GhostWriter.WorkCreation.Ideation.Review;

namespace GhostWriter.WorkCreation.Ideation.TimeEstimation
{
    public class TimeEstimationPayload : Payload<TimeEstimates>
    {
        public override TimeEstimates Content { get; set; }
        public override void Visit<T>(T visitable)
        {
            if (visitable is IdeaReviewer ideaReviewer) ideaReviewer.SetTimeEstimate(Content);
        }
    }
}
