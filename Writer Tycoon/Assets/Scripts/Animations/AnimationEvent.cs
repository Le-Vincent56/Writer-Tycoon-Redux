using System;
using UnityEngine.Events;

namespace WriterTycoon.Animations
{
    [Serializable]
    public class AnimationEvent
    {
        public string eventName;
        public UnityEvent OnAnimationEvent;
    }
}