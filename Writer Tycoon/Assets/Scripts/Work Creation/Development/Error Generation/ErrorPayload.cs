using GhostWriter.Patterns.Mediator;
using GhostWriter.WorkCreation.Editing;

namespace GhostWriter.WorkCreation.Development.ErrorGeneration
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