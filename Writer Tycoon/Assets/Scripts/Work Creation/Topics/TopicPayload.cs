using System.Collections.Generic;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Compatibility;
using WriterTycoon.WorkCreation.TimeEstimation;

namespace WriterTycoon.WorkCreation.Topics
{
    public class TopicPayload : Payload<List<Topic>>
    {
        public override List<Topic> Content { get; set; }

        public override void Visit<T>(T visitable)
        {
            // Verify types for mediation
            if (visitable is CompatibilityManager compatibilityManager) compatibilityManager.SetTopics(Content);
            if (visitable is TimeEstimator timeEstimator) timeEstimator.SetTopics(Content);
        }
    }
}