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

    public struct SetPhaseSlider : IEvent
    {
        public DevelopmentPhase Phase;
    }

    public struct  HandleSliderWindow : IEvent
    {
        public bool IsOpening;
    }

    public struct CloseSliderWindow : IEvent { }

    public struct EndDevelopment : IEvent { }
    #endregion
}
