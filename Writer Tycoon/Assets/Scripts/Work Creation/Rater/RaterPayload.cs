using System.Collections.Generic;
using GhostWriter.Patterns.Mediator;
using GhostWriter.WorkCreation.Ideation.Genres;
using GhostWriter.WorkCreation.Ideation.Topics;

namespace GhostWriter.WorkCreation.Rater
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
