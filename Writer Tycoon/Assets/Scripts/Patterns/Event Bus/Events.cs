using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WriterTycoon.Patterns.EventBus
{
    public interface IEvent { }

    public struct OpenWorkMenuEvent : IEvent 
    {
        public bool IsOpening;
    }

    public struct PauseCalendarEvent : IEvent
    {
        public bool Paused;
    }
}
