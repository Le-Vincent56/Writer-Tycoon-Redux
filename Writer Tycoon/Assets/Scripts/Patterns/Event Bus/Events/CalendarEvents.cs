namespace WriterTycoon.Patterns.EventBus
{
    public struct PassHour : IEvent { }

    public struct PassDay : IEvent { }

    public struct PassWeek : IEvent { }

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
