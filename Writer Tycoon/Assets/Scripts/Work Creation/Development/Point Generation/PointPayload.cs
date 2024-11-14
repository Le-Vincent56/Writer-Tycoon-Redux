using System.Collections.Generic;
using GhostWriter.Patterns.Mediator;
using GhostWriter.WorkCreation.Development.Tracker;
using GhostWriter.WorkCreation.Editing;

namespace GhostWriter.WorkCreation.Development.PointGeneration
{
    public class PointPayload : Payload<Dictionary<int, Work>>
    {
        public override Dictionary<int, Work> Content { get; set; }
        public override void Visit<T>(T visitable)
        {
            // Determine mediation routes
            if (visitable is PolishManager polishManager) polishManager.SetWorksInProgress(Content);
        }
    }
}