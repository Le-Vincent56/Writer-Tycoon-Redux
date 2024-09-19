namespace WriterTycoon.Patterns.EventBus
{
    public struct PassDay : IEvent { }

    public struct CalendarPauseStateChanged : IEvent
    {
        public bool Paused;
    }

    public struct ChangeCalendarPauseState : IEvent
    {
        public bool Paused;
        public bool AllowSpeedChanges;
    }

    public struct ChangeCalendarSpeed : IEvent
    {
        public int TimeScale;
    }
}
