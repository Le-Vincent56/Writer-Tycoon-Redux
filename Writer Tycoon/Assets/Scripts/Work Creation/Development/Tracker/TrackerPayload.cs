using System.Collections.Generic;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Development.ErrorGeneration;
using WriterTycoon.WorkCreation.Development.PointGeneration;
using WriterTycoon.WorkCreation.Editing;

namespace WriterTycoon.WorkCreation.Development.Tracker
{
    public class TrackerPayload : Payload<Dictionary<int, Work>>
    {
        public override Dictionary<int, Work> Content { get; set; }
        public override void Visit<T>(T visitable)
        {
            // Determine mediation routes
            if (visitable is PointGenerationManager pointGenerationManager) pointGenerationManager.SetWorksInProgress(Content);
            if (visitable is ErrorGenerationManager errorGenerationManager) errorGenerationManager.SetWorksInProgress(Content);
            if (visitable is PolishManager polishManager) polishManager.SetWorksInProgress(Content);
        }
    }
}