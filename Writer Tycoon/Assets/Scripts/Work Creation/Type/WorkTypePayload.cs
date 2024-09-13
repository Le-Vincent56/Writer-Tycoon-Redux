using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Compatibility;

namespace WriterTycoon.WorkCreation.WorkType
{
    public class WorkTypePayload : Payload<WorkType>
    {
        public override WorkType Content { get; set; }

        public override void Visit<T>(T visitable)
        {
            // Verify types for mediation
            if (visitable is CompatibilityManager compatibilityManager) compatibilityManager.SetWorkType(Content);
        }
    }
}
