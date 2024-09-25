using System.Collections.Generic;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.Editing;

namespace WriterTycoon.WorkCreation.Development.PointGeneration
{
    public class PointPayload : Payload<Dictionary<int, Work>>
    {
        public override Dictionary<int, Work> Content { get; set; }
        public override void Visit<T>(T visitable)
        {
            // Determine mediation routes
            if (visitable is PolishManager polishManager) polishManager.SetWorks(Content);
        }
    }
}