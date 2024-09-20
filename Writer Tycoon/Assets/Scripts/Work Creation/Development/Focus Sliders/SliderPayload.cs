using System.Collections.Generic;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Development.PointGeneration;

namespace WriterTycoon.WorkCreation.Development.FocusSliders
{
    public class SliderPayload : Payload<Dictionary<PointCategory, int>>
    {
        public override Dictionary<PointCategory, int> Content { get; set; }
        public override void Visit<T>(T visitable)
        {
            if (visitable is PointGenerator pointGenerator) pointGenerator.SetAllocatedPoints(Content);
        }
    }
}