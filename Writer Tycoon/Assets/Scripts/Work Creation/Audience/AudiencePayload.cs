using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Compatibility;
using WriterTycoon.WorkCreation.Review;

namespace WriterTycoon.WorkCreation.Audience
{
    public class AudiencePayload : Payload<AudienceType>
    {
        public override AudienceType Content { get; set; }

        public override void Visit<T>(T visitable)
        {
            // Verify types for mediation
            if (visitable is CompatibilityManager compatibilityManager) compatibilityManager.SetAudience(Content);
            if (visitable is IdeaReviewer ideaReviewer) ideaReviewer.SetAudienceType(Content);
        }
    }
}