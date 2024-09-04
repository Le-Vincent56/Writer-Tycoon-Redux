using WriterTycoon.WorkCreation.Mediation;

namespace WrtierTycoon.WorkCreation.Rater
{
    public class WorkRater : Dedicant
    {
        public override string Name { get => "Work Rater"; }
        public override DedicantType Type { get => DedicantType.Rater; }
    }
}