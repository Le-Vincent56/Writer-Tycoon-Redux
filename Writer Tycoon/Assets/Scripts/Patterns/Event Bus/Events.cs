using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.WorkCreation.Review;

namespace WriterTycoon.Patterns.EventBus
{
    public interface IEvent { }

    public struct SendReviewData : IEvent 
    {
        public ReviewData ReviewData;
    }

    public struct NotifyFailedCreation : IEvent
    {
        public ReviewData ReviewData;
    }

    public struct NotifySuccessfulCreation : IEvent
    {
        public ReviewData ReviewData;
    }

    public struct OpenCreateWorkMenu : IEvent 
    {
        public bool IsOpening;
    }

    public struct ShowEstimationText : IEvent
    {
        public bool ShowText;
    }

    public struct PauseCalendar : IEvent
    {
        public bool Paused;
    }
}
