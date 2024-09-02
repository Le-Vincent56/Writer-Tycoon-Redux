namespace WriterTycoon.WorkCreation.Topics
{
    public abstract class TopicFactory
    {
        public abstract Topic CreateTopic(string name, bool unlocked);
    }

    public class StandardTopicFactory : TopicFactory
    {
        public override Topic CreateTopic(string name, bool unlocked) => new Topic(name, unlocked);
    }
}