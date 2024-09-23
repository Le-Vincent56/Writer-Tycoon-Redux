using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Editing;

namespace WriterTycoon.WorkCreation.Development.PointGeneration
{
    public class PointPayload : Payload<float>
    {
        public override float Content { get; set; }
        public override void Visit<T>(T visitable)
        {
            // Determine mediation routes
            if (visitable is PolishManager polishManager) polishManager.SetPoints(Content);
        }
    }
}