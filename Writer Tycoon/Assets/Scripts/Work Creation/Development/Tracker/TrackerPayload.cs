using System.Collections.Generic;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Development.PointGeneration;

namespace WriterTycoon.WorkCreation.Development.Tracker
{
    public class TrackerPayload : Payload<Dictionary<int, Work>>
    {
        public override Dictionary<int, Work> Content { get; set; }
        public override void Visit<T>(T visitable)
        {
            // Determine mediation routes
            if (visitable is PointGenerationManager pointGenerator) pointGenerator.SetWorksInProgress(Content);
        }
    }
}