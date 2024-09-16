using WriterTycoon.WorkCreation.Ideation.Review;

namespace WriterTycoon.Patterns.EventBus
{
    #region WORK CREATION UI EVENTS
    public struct OpenCreateWorkMenu : IEvent
    {
        public bool IsOpening;
    }

    public struct ShowEstimationText : IEvent
    {
        public bool ShowText;
    }

    public struct NotifyFailedCreation : IEvent
    {
        public ReviewData ReviewData;
    }

    public struct NotifySuccessfulCreation : IEvent
    {
        public ReviewData ReviewData;
    }

    public struct SetInteracting : IEvent
    {
        public bool Interacting;
    }
    #endregion
}
