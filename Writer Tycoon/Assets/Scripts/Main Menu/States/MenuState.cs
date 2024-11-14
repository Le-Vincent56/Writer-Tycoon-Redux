using DG.Tweening;
using UnityEngine;
using GhostWriter.Patterns.StateMachine;

namespace GhostWriter.MainMenu.States
{
    public class MenuState : IState
    {
        protected static int ForwardHash = Animator.StringToHash("Forward");
        protected static int BackwardHash = Animator.StringToHash("Backward");

        protected readonly MainMenuController controller;
        protected readonly Animator animator;
        private readonly CanvasGroup stateGroup;
        private Tween fadeTween;
        private readonly float fadeDuration;

        public MenuState(MainMenuController controller, Animator animator, CanvasGroup stateGroup)
        {
            this.controller = controller;
            this.animator = animator;
            this.stateGroup = stateGroup;
            fadeDuration = 0.45f;
        }

        public virtual void OnEnter()
        {
            // Fade in
            Fade(1f, fadeDuration, () =>
            {
                // Allow the CanvasGroup to be interacted with
                stateGroup.interactable = true;
                stateGroup.blocksRaycasts = true;
            }, Ease.InExpo);
        }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void OnExit()
        {
            // Fade out
            Fade(0f, fadeDuration, () =>
            {
                // Don't allow the CanvasGroup to be interacted with
                stateGroup.interactable = false;
                stateGroup.blocksRaycasts = false;
            }, Ease.OutExpo);
        }

        /// <summary>
        /// Handle the fading of the CanvasGroup
        /// </summary>
        private void Fade(float endValue, float duration, TweenCallback onComplete = null, Ease easeType = Ease.Unset)
        {
            // Kill the fade tween if it exists already
            fadeTween?.Kill();

            // Fade the CanvasGroup
            fadeTween = stateGroup.DOFade(endValue, duration);

            // Check if there is a set Easing type
            if (easeType != Ease.Unset)
                // Set the Easing type
                fadeTween.SetEase(easeType);

            // Exit case - no completion action was provided
            if (onComplete == null) return;

            // Hook up completion actions
            fadeTween.onComplete += onComplete;
        }
    }
}
