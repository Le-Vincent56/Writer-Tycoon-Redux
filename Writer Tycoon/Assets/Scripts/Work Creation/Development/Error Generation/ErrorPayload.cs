using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Editing;

namespace WriterTycoon.WorkCreation.Development.ErrorGeneration
{
    public class ErrorPayload : Payload<(int Hash, int TotalErrors)>
    {
        public override (int Hash, int TotalErrors) Content { get; set; }
        public override void Visit<T>(T visitable)
        {
            // Verify types of mediation
            if (visitable is PolishManager editingManager) editingManager.SetErrors(Content.Hash, Content.TotalErrors);
        }
    }
}