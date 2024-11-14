using System;
using UnityEngine.Events;

namespace GhostWriter.Animations
{
    [Serializable]
    public class AnimationEvent
    {
        public string eventName;
        public UnityEvent OnAnimationEvent;
    }
}