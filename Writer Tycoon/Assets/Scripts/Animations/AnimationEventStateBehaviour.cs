using UnityEngine;

namespace WriterTycoon.Animations
{
    public class AnimationEventStateBehaviour : StateMachineBehaviour
    {
        public string eventName;
        [Range(0f, 1f)] public float triggerTime;

        private bool hasTriggered;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            hasTriggered = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Restrict the time into one loop
            float currentTime = stateInfo.normalizedTime % 1f;

            // Check if the event has not triggered and the current time
            // has surpassed the trigger time
            if(!hasTriggered && currentTime >= triggerTime)
            {
                // Notify the receiver and set triggered
                NotifyReceiver(animator);
                hasTriggered = true;
            }
        }
        
        /// <summary>
        /// Notify event receivers of the animation event triggering
        /// </summary>
        private void NotifyReceiver(Animator animator)
        {
            AnimationEventReceiver receiver = animator.GetComponent<AnimationEventReceiver>();
            if(receiver != null)
            {
                receiver.OnAnimationEventTriggered(eventName);
            }
        }
    }
}
