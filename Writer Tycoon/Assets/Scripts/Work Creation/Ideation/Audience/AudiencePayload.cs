using GhostWriter.Patterns.Mediator;
using GhostWriter.WorkCreation.Ideation.Compatibility;
using GhostWriter.WorkCreation.Ideation.Review;

namespace GhostWriter.WorkCreation.Ideation.Audience
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
