using System.Collections.Generic;
using WriterTycoon.Patterns.Mediator;
using WrtierTycoon.WorkCreation.Rater;

namespace WriterTycoon.WorkCreation.Genres
{
    public class GenrePayload : Payload<List<Genre>>
    {
        public override List<Genre> Content { get; set; }

        public override void Visit<T>(T visitable)
        {
            // Verify types for mediation
            if (visitable is WorkRater rater) rater.SetGenres(Content);
        }
    }
}