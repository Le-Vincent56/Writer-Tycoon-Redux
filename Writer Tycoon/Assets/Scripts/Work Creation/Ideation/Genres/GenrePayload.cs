using System.Collections.Generic;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Ideation.Compatibility;
using WriterTycoon.WorkCreation.Ideation.Review;

namespace WriterTycoon.WorkCreation.Ideation.Genres
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