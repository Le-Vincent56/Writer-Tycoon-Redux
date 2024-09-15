using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Ideation.Review;
using WriterTycoon.WorkCreation.Ideation.TimeEstimation;

namespace WriterTycoon.WorkCreation.Ideation.WorkTypes
{
    public class WorkTypePayload : Payload<WorkType>
    {
        public override WorkType Content { get; set; }

        public override void Visit<T>(T visitable)
        {
            // Verify types for mediation
            if (visitable is TimeEstimator timeEstimator) timeEstimator.SetWorkType(Content);
            if (visitable is IdeaReviewer ideaReviewer) ideaReviewer.SetWorkType(Content);
        }
    }
}
