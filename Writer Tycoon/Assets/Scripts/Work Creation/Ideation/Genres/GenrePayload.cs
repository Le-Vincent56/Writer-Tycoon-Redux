using System.Collections.Generic;
using GhostWriter.Patterns.Mediator;
using GhostWriter.WorkCreation.Ideation.Compatibility;
using GhostWriter.WorkCreation.Ideation.Review;

namespace GhostWriter.WorkCreation.Ideation.Genres
{
    public class GenrePayload : Payload<List<Genre>>
    {
        public override List<Genre> Content { get; set; }

        public override void Visit<T>(T visitable)
        {
            // Verify types for mediation
            if (visitable is CompatibilityManager compatibilityManager) compatibilityManager.SetGenres(Content);
            if (visitable is IdeaReviewer ideaReviewer) ideaReviewer.SetGenres(Content);
        }
    }
}