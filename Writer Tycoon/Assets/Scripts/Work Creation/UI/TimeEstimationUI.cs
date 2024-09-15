using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Ideation.TimeEstimation;

namespace WriterTycoon.WorkCreation.UI
{
    public class TimeEstimationUI : MonoBehaviour
    {
        private TimeEstimator timeEstimator;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Text timeEstimationText;
        [SerializeField] private float fadeDuration;
        [SerializeField] private int dayEstimation;

        EventBinding<ShowEstimationText> showEstimationTextEvent;

        private Tween fadeTween;

        private void Awake()
        {
            // Verify the TopicManager
            if (timeEstimator == null)
                timeEstimator = GetComponent<TimeEstimator>();
        }

        private void OnEnable()
        {
            showEstimationTextEvent = new EventBinding<ShowEstimationText>(HandleEstimationText);
            EventBus<ShowEstimationText>.Register(showEstimationTextEvent);

            timeEstimator.Updated += UpdateTimeEstimationText;
        }

        private void OnDisable()
        {
            EventBus<ShowEstimationText>.Deregister(showEstimationTextEvent);

            timeEstimator.Updated -= UpdateTimeEstimationText;
        }

        /// <summary>
        /// Show the Estimation Text
        /// </summary>
        private void ShowText() => Fade(1f, fadeDuration);

        /// <summary>
        /// Hide the Estimation Text
        /// </summary>
        private void HideText() => Fade(0f, fadeDuration);

        /// <summary>
        /// Callback for the Show Estimation Text Event
        /// </summary>
        private void HandleEstimationText(ShowEstimationText eventData)
        {
            // Check whether or not to show the text
            if (eventData.ShowText)
                // If so, update the time estimation text
                UpdateTimeEstimationText(dayEstimation);
            else
                // Otherwise, hide the text
                HideText();
        }

        /// <summary>
        /// Update the Time Estimation Text
        /// </summary>
        /// <param name="dayEstimation"></param>
        private void UpdateTimeEstimationText(int dayEstimation)
        {
            // Exit case - if the Time Estimation Text is null
            if (timeEstimationText == null) return;

            // Set the estimated days
            this.dayEstimation = dayEstimation;

            // Set the text
            timeEstimationText.text = $"Estimated Time: {dayEstimation} Days";

            // Check if there is a set estimation in days
            if (dayEstimation == 0)
                // If not, hide the text
                HideText();
            else if (dayEstimation > 0)
                // If so, show the text
                ShowText();
        }

        /// <summary>
        /// Handle fading for the estimation Text
        /// </summary>
        private void Fade(float endFadeValue, float duration, TweenCallback onEnd = null)
        {
            // Exit case - if the canvas group is null
            if (canvasGroup == null) return;

            // Kill the current fade tween if it exists
            fadeTween?.Kill(false);

            // Set the fade animation
            fadeTween = canvasGroup.DOFade(endFadeValue, duration)
                .SetEase(Ease.OutQuint);

            // Exit case - there is no specified callback
            if (onEnd == null) return;

            // Hook up callback events
            fadeTween.onComplete += onEnd;
        }
    }
}