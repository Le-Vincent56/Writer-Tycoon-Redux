using WriterTycoon.WorkCreation.Development.PointGeneration;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.Ideation.Review;

namespace WriterTycoon.Patterns.EventBus
{
    #region WORK CREATION EVENTS
    public struct SendReviewData : IEvent
    {
        public ReviewData ReviewData;
    }
    #endregion

    #region WORK DEVELOPMENT EVENTS
    public struct UpdateProgressData : IEvent
    {
        public int Current;
        public int Maximum;
    }

    public struct SetSliderPhaseState : IEvent
    {
        public DevelopmentPhase Phase;
    }

    public struct SetSliderPoints : IEvent
    {
        public PointCategory Category;
        public int Value;
    }

    public struct SendSliderPoints : IEvent { }

    public struct ClearIdeation : IEvent { }

    public struct EndDevelopment : IEvent { }
    #endregion
}
