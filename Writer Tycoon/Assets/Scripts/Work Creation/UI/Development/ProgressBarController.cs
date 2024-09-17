using DG.Tweening;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public class ProgressBarController : MonoBehaviour
    {
        [SerializeField] private float fadeDuration;
        private CanvasGroup canvasGroup;
        private Tween fadeTween;

        private EventBinding<ConfirmPlayerWorkState> confirmPlayerWorkStateEvent;
        private EventBinding<NotifySuccessfulCreation> notifySuccessfulCreationEvent;
        private EventBinding<EndDevelopment> endDevelopmentEvent;

        private void Awake()
        {
            // Verify the Canvas Group
            if(canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            confirmPlayerWorkStateEvent = new EventBinding<ConfirmPlayerWorkState>(HandleProgressBar);
            EventBus<ConfirmPlayerWorkState>.Register(confirmPlayerWorkStateEvent);

            notifySuccessfulCreationEvent = new EventBinding<NotifySuccessfulCreation>(Show);
            EventBus<NotifySuccessfulCreation>.Register(notifySuccessfulCreationEvent);

            endDevelopmentEvent = new EventBinding<EndDevelopment>(Hide);
            EventBus<EndDevelopment>.Register(endDevelopmentEvent);
        }

        private void OnDisable()
        {
            EventBus<ConfirmPlayerWorkState>.Deregister(confirmPlayerWorkStateEvent);
            EventBus<NotifySuccessfulCreation>.Deregister(notifySuccessfulCreationEvent);
            EventBus<EndDevelopment>.Deregister(endDevelopmentEvent);
        }

        /// <summary>
        /// Callback function to handle work state confirmation
        /// </summary>
        private void HandleProgressBar(ConfirmPlayerWorkState eventData)
        {
            // Check if the player is working
            if (eventData.Working)
                // If so, show the progress bar
                Show();
            else
                // Otherwise, hide it
                Hide();
        }

        /// <summary>
        /// Show the Progress Bar
        /// </summary>
        private void Show() => Fade(1f, fadeDuration);

        /// <summary>
        /// Hide the Progress Bar
        /// </summary>
        private void Hide() => Fade(0f, fadeDuration);

        /// <summary>
        /// Handle fading for the Progress Bar
        /// </summary>
        private void Fade(float endFadeValue, float duration, TweenCallback onEnd = null, Ease easeType = Ease.InQuint)
        {
            // Kill the current fade tween if it exists
            fadeTween?.Kill(false);

            // Set the fade animation
            fadeTween = canvasGroup.DOFade(endFadeValue, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onEnd == null) return;

            // Hook up callback events
            fadeTween.onComplete += onEnd;
        }
    }
}