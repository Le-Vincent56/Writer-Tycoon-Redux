using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.WorkCreation.TimeEstimation;

namespace WriterTycoon.WorkCreation.UI
{
    public class TimeEstimationUI : MonoBehaviour
    {
        private TimeEstimator timeEstimator;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Text timeEstimationText;
        [SerializeField] private float fadeDuration;

        private Tween fadeTween;

        private void Awake()
        {
            // Verify the TopicManager
            if (timeEstimator == null)
                timeEstimator = GetComponent<TimeEstimator>();
        }

        private void OnEnable()
        {
            timeEstimator.Updated += UpdateTimeEstimationText;
        }

        private void OnDisable()
        {
            timeEstimator.Updated -= UpdateTimeEstimationText;
        }

        private void UpdateTimeEstimationText(int dayEstimation)
        {
            // Exit case - if the Time Estimation Text is null
            if (timeEstimationText == null) return;

            if (dayEstimation == 0)
                Fade(0f, fadeDuration);
            else if (dayEstimation > 0)
                Fade(1f, fadeDuration);

            timeEstimationText.text = $"Estimated Time: {dayEstimation} Days";
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