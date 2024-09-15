using System.Collections.Generic;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Ideation.Compatibility;
using WriterTycoon.WorkCreation.Ideation.Review;
using WriterTycoon.WorkCreation.Ideation.TimeEstimation;

namespace WriterTycoon.WorkCreation.Ideation.Topics
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