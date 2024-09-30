using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Ideation.Review;

namespace WriterTycoon.WorkCreation.Ideation.Compatibility
{
    public class CompatibilityPayload : Payload<CompatibilityInfo>
    {
        public override CompatibilityInfo Content { get; set; }

        public override void Visit<T>(T visitable)
        {
            if (visitable is IdeaReviewer ideaReviewer) ideaReviewer.SetCompatibilityInfo(Content);
        }
    }
}