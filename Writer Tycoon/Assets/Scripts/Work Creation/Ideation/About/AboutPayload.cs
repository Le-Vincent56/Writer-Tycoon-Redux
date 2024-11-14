using GhostWriter.Patterns.Mediator;
using GhostWriter.WorkCreation.Ideation.Review;

namespace GhostWriter.WorkCreation.Ideation.About
{
    public class AboutPayload : Payload<AboutInfo>
    {
        public override AboutInfo Content { get; set; }
        public override void Visit<T>(T visitable)
        {
            if (visitable is IdeaReviewer ideaReviewer) ideaReviewer.SetAboutInfo(Content);
        }
    }
}