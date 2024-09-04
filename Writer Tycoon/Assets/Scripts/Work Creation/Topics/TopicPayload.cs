using System.Collections.Generic;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Compatibility;

namespace WriterTycoon.WorkCreation.Topics
{
    public class TopicPayload : Payload<List<Topic>>
    {
        public override List<Topic> Content { get; set; }

        public override void Visit<T>(T visitable)
        {
            // Verify types for mediation
            if (visitable is CompatibilityManager compatibilityManager) compatibilityManager.SetTopics(Content);
        }
    }
}