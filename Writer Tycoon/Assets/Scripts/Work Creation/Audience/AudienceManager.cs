using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Audience
{
    public enum AudienceType
    {
        Children,
        Teens,
        YoungAdults,
        Adults
    }

    public class AudienceManager : Dedicant
    {
        public override string Name { get => "Audience Manager"; }
        public override DedicantType Type { get => DedicantType.Audience; }
    }
}