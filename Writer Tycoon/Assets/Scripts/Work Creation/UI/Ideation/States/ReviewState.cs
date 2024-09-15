using UnityEngine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.UI.Ideation.States
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
            EventBus<ShowEstimationText>.Raise(new ShowEstimationText { ShowText = false });
        }

        public override void OnExit()
        {
            // Raise an event to show the Estimation Time Text
            EventBus<ShowEstimationText>.Raise(new ShowEstimationText { ShowText = true });

            // Fade out
            base.OnExit();
        }
    }
}