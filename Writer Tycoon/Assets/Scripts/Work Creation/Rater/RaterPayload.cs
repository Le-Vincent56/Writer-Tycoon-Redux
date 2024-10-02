using System.Collections.Generic;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Ideation.Genres;
using WriterTycoon.WorkCreation.Ideation.Topics;

namespace WriterTycoon.WorkCreation.Rater
{
    public class RaterPayload : Payload<(List<Topic> Topics, List<Genre> Genres)>
    {
        public override (List<Topic> Topics, List<Genre> Genres) Content { get; set; }

        public override void Visit<T>(T visitable)
        {
            if (visitable is TopicManager topicManager) topicManager.UpdateMasteries(Content.Topics);
            if (visitable is GenreManager genreManager) genreManager.UpdateMasteries(Content.Genres);
        }
    }
}
