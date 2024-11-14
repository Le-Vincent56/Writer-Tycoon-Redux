using System.Collections.Generic;
using GhostWriter.Patterns.Mediator;
using GhostWriter.WorkCreation.Development.PointGeneration;

namespace GhostWriter.WorkCreation.Development.FocusSliders
{
    public class SliderPayload : Payload<(int Hash, Dictionary<PointCategory, int> Points)>
    {
        public override (int Hash, Dictionary<PointCategory, int> Points) Content { get; set; }
        public override void Visit<T>(T visitable)
        {
            if (visitable is PointGenerationManager pointGenerator) pointGenerator.SetAllocatedPoints(Content);
        }
    }
}