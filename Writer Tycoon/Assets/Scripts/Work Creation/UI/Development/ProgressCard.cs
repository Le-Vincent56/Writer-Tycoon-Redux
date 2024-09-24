using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public class ProgressCard : MonoBehaviour
    {
        [SerializeField] private float translateValue;
        [SerializeField] private float animateDuration;
        private RectTransform rectTransform;
        private LayoutElement layoutElement;
        private CanvasGroup card;
        private Vector3 originalPosition;
        private Tween fadeTween;
        private Tween translateTween;

        private EventBinding<ConfirmPlayerWorkState> confirmPlayerWorkStateEvent;
        private EventBinding<EndDevelopment> endDevelopmentEvent;

        private void Awake()
        {
            // Verify the rect transform
            if(rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            // Verify the layout element
            if (layoutElement == null)
                layoutElement = GetComponent<LayoutElement>();

            // Verify the Canvas Group
            if (card == null)
                card = GetComponent<CanvasGroup>();

            // Set variables
            originalPosition = rectTransform.localPosition;
        }

        private void OnEnable()
        {
            confirmPlayerWorkStateEvent = new EventBinding<ConfirmPlayerWorkState>(HandleProgressBar);
            EventBus<ConfirmPlayerWorkState>.Register(confirmPlayerWorkStateEvent);

            endDevelopmentEvent = new EventBinding<EndDevelopment>(Hide);
            EventBus<EndDevelopment>.Register(endDevelopmentEvent);
        }

        private void OnDisable()
        {
            EventBus<ConfirmPlayerWorkState>.Deregister(confirmPlayerWorkStateEvent);
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
        private void Show()
        {
            layoutElement.ignoreLayout = true;

            // Set the window's initial position to be below
            Vector3 startPos = new Vector3(
                originalPosition.x,
                originalPosition.y - translateValue,
                originalPosition.z
            );
            rectTransform.localPosition = startPos;

            // Fade in
            Fade(1f, animateDuration);

            // Translate up
            Translate(translateValue, animateDuration, () => layoutElement.ignoreLayout = false);
        }

        /// <summary>
        /// Hide the Progress Bar
        /// </summary>
        private void Hide()
        {
            layoutElement.ignoreLayout = true;

            // Translate up slightly
            Translate(
                translateValue * (3f/4f),
                animateDuration / 3f,
                () =>
                {
                    // When done, fade out
                    Fade(0f, animateDuration, () =>
                    {
                        card.interactable = false;
                        card.blocksRaycasts = false;
                    }, Ease.OutCirc);

                    // Translate downwards
                    Translate(-translateValue * 2f, animateDuration, () => layoutElement.ignoreLayout = false, Ease.OutCirc);
                },
                Ease.OutBack
            );
        }

        /// <summary>
        /// Handle fading for the Progress Bar
        /// </summary>
        private void Fade(float endFadeValue, float duration, TweenCallback onEnd = null, Ease easeType = Ease.InQuint)
        {
            // Kill the current fade tween if it exists
            fadeTween?.Kill(false);

            // Set the fade animation
            fadeTween = card.DOFade(endFadeValue, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onEnd == null) return;

            // Hook up callback events
            fadeTween.onComplete += onEnd;
        }

        /// <summary>
        /// Handle translating for the Window
        /// </summary>
        private void Translate(float endTranslateValue, float duration, TweenCallback onEnd = null, Ease easeType = Ease.OutQuint)
        {
            // Kill the current translate tween if it exists
            translateTween?.Kill(false);

            // Calculate the target position
            float targetPos = rectTransform.localPosition.y + endTranslateValue;

            // Set the tween animation
            translateTween = rectTransform.DOLocalMoveY(targetPos, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onEnd == null) return;

            // Hook up callback events
            translateTween.onComplete += onEnd;
        }
    }
}