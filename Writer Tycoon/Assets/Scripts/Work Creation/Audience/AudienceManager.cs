using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Audience
{
    public class AudienceManager : Dedicant
    {
        public override string Name { get => "Audience Manager"; }
        public override DedicantType Type { get => DedicantType.Audience; }
    }
}