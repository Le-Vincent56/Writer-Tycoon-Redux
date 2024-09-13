using DG.Tweening;
using UnityEngine;
using WriterTycoon.Patterns.StateMachine;

namespace WriterTycoon.WorkCreation.UI.States
{
    public class CreateWorkState : IState
    {
        protected float fadeDuration;
        protected CanvasGroup canvasGroup;
        protected Tween fadeTween;
        protected float durationTime;

        public CreateWorkState(CanvasGroup canvasGroup)
        {
            fadeDuration = 0f;
            this.canvasGroup = canvasGroup;
        }

        public virtual void OnEnter()
        {
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
            Hide();
        }

        protected void Show()
        {
            Fade(1f, fadeDuration, () =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
        }

        protected void Hide()
        {
            Fade(0f, fadeDuration, () =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
        }

        /// <summary>
        /// Handle fading for the Window
        /// </summary>
        /// <param name="endFadeValue"></param>
        /// <param name="duration"></param>
        /// <param name="onEnd"></param>
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