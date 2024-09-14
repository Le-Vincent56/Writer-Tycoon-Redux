using UnityEngine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.UI.States
{
    public class ReviewState : CreateWorkState
    {
        public ReviewState(CanvasGroup canvasGroup) : base(canvasGroup)
        {
        }

        public override void OnEnter()
        {
            // Fade in
            base.OnEnter();

            // Raise an event to hide the Estimation Time Text
            EventBus<ShowEstimationTextEvent>.Raise(new ShowEstimationTextEvent { ShowText = false });
        }

        public override void OnExit()
        {
            // Raise an event to show the Estimation Time Text
            EventBus<ShowEstimationTextEvent>.Raise(new ShowEstimationTextEvent { ShowText = true });

            // Fade out
            base.OnExit();
        }
    }
}