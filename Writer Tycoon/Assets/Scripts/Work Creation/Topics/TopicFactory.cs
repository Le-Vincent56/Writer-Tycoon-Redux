namespace WriterTycoon.WorkCreation.Topics
{
    public abstract class TopicFactory
    {
        public abstract Topic CreateTopic(TopicType topicType, bool unlocked);
    }

    public class StandardTopicFactory : TopicFactory
    {
        public override Topic CreateTopic(TopicType topicType, bool unlocked) => new Topic(topicType, unlocked);
    }
}