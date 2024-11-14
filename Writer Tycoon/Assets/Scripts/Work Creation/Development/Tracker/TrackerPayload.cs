using System.Collections.Generic;
using GhostWriter.Patterns.Mediator;
using GhostWriter.WorkCreation.Development.ErrorGeneration;
using GhostWriter.WorkCreation.Development.PointGeneration;
using GhostWriter.WorkCreation.Editing;

namespace GhostWriter.WorkCreation.Development.Tracker
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