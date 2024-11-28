using DG.Tweening;
using GhostWriter.Patterns.StateMachine;
using UnityEngine;

namespace GhostWriter.Entities.Competitors.UI.States
{
    public class CompetitorWindowState : IState
    {
        private readonly CompetitorWindow controller;
        private readonly CanvasGroup canvasGroup;

        private Tween fadeTween;
        private readonly float fadeDuration;

        public CompetitorWindowState(CompetitorWindow controller, CanvasGroup canvasGroup)
        {
            this.controller = controller;
            this.canvasGroup = canvasGroup;

            fadeDuration = 0.2f;
        }

        public virtual void OnEnter()
        {
            // Fade in
            Fade(1f, fadeDuration, () =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
        }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void OnExit()
        {
            // Fade out
            Fade(0f, fadeDuration, () =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
        }

        /// <summary>
        /// Handle state fading
        /// </summary>
        protected void Fade(float endValue, float duration, TweenCallback onComplete = null)
        {
            // Kill the Fade Tween if it already exists
            fadeTween?.Kill();

            // Set the Fade Tween
            fadeTween = canvasGroup.DOFade(endValue, duration);

            // Exit case - there's no completion action
            if (onComplete == null) return;

            // Hook up completion actions
            fadeTween.onComplete += onComplete;
        }
    }
}
