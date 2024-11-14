using System.Collections.Generic;
using UnityEngine;

namespace GhostWriter.Animations
{
    public class AnimationEventReceiver : MonoBehaviour
    {
        [SerializeField] List<AnimationEvent> animationEvents = new();

        /// <summary>
        /// Handle animation event triggering
        /// </summary>
        public void OnAnimationEventTriggered(string eventName)
        {
            // Try to find a matching event within the listed events
            AnimationEvent matchingEvent = animationEvents.Find(se => se.eventName == eventName);

            // Invoke the animation event if both it and the matching event exists
            matchingEvent?.OnAnimationEvent?.Invoke();
        }
    }
}
