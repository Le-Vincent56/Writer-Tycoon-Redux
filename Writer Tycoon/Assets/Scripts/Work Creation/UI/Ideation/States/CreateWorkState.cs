using DG.Tweening;
using UnityEngine;
using WriterTycoon.Patterns.StateMachine;

namespace WriterTycoon.WorkCreation.UI.Ideation.States
{
    public class CreateWorkState : IState
    {
        protected float fadeDuration;
        protected CanvasGroup canvasGroup;
        protected Tween fadeTween;

        public CreateWorkState(CanvasGroup canvasGroup)
        {
            fadeDuration = 0f;
            this.canvasGroup = canvasGroup;
        }

        public virtual void OnEnter()
        {
            // Show the State
            Show();
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnExit()
        {
            // Hide the State
            Hide();
        }

        /// <summary>
        /// Show the State
        /// </summary>
        protected void Show()
        {
            Fade(1f, fadeDuration, () =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
        }

        /// <summary>
        /// Hide the State
        /// </summary>
        protected void Hide()
        {
            Fade(0f, fadeDuration, () =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
        }

        /// <summary>
        /// Handle fading for the State
        /// </summary>
        private void Fade(float endFadeValue, float duration, TweenCallback onEnd)
        {
            // Kill the current fade tween if it exists
            fadeTween?.Kill(false);

            // Set the fade animation
            fadeTween = canvasGroup.DOFade(endFadeValue, duration)
                .SetEase(Ease.OutQuint);

            // Hook up callback events
            fadeTween.onComplete += onEnd;
        }
    }
}