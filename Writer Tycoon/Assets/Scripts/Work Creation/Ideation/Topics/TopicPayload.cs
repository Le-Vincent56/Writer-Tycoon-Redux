using System.Collections.Generic;
using GhostWriter.Patterns.Mediator;
using GhostWriter.WorkCreation.Ideation.Compatibility;
using GhostWriter.WorkCreation.Ideation.Review;
using GhostWriter.WorkCreation.Ideation.TimeEstimation;

namespace GhostWriter.WorkCreation.Ideation.Topics
{
    public class TopicPayload : Payload<List<Topic>>
    {
        public override List<Topic> Content { get; set; }

        public override void Visit<T>(T visitable)
        {
            // Verify types for mediation
            if (visitable is CompatibilityManager compatibilityManager) compatibilityManager.SetTopics(Content);
            if (visitable is TimeEstimator timeEstimator) timeEstimator.SetTopics(Content);
            if (visitable is IdeaReviewer ideaReviewer) ideaReviewer.SetTopics(Content);
        }
    }
}