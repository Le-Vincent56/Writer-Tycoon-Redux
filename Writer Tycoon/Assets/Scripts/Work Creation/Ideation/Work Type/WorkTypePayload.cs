using GhostWriter.Patterns.Mediator;
using GhostWriter.WorkCreation.Ideation.Review;
using GhostWriter.WorkCreation.Ideation.TimeEstimation;

namespace GhostWriter.WorkCreation.Ideation.WorkTypes
{
    public class WorkTypePayload : Payload<(WorkType Type, float TargetScore)>
    {
        public override (WorkType Type, float TargetScore) Content { get; set; }

        public override void Visit<T>(T visitable)
        {
            // Verify types for mediation
            if (visitable is TimeEstimator timeEstimator) timeEstimator.SetWorkType(Content.Type);
            if (visitable is IdeaReviewer ideaReviewer) ideaReviewer.SetWorkType(Content.Type, Content.TargetScore);
        }
    }
}
