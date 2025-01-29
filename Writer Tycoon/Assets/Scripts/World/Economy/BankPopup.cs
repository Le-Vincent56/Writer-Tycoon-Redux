using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter.World.Economy
{
    public class BankPopup : MonoBehaviour
    {
        private RectTransform rectTransform;
        private Text popupText;

        [SerializeField] private Vector3 initialPosition;
        [SerializeField] private Color initialColor;
        [SerializeField] private int initialScaleSize;
        [SerializeField] private int popupScaleSize;
        [SerializeField] private float totalDuration;
        private Tween scaleTween;
        private Tween translateTween;
        private Tween fadeTween;

        public void Initialize()
        {
            // Verify the RectTransform component
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            // Verify the Text component
            if (popupText == null)
                popupText = GetComponent<Text>();

            // Set the initial scale size
            initialScaleSize = popupText.fontSize;
            initialPosition = rectTransform.localPosition;
            initialColor = popupText.color;
        }


        private void OnDestroy()
        {
            // Kill all existing Tweens
            scaleTween.Kill();
            translateTween.Kill();
            fadeTween.Kill();
        }

        /// <summary>
        /// Popup the Text
        /// </summary>
        public void Popup(BankPopupPool pool, Color popupColor, float revenue)
        {
            // If there's no revenue made, return
            if(revenue == 0)
            {
                // Release this object back to the pool
                pool.Pool.Release(this);

                return;
            }

            // Create the popup string
            string popupString = revenue > 0
                ? $"+${revenue}"
                : $"-${revenue}";

            // Set the text
            popupText.text = popupString;
            popupText.color = popupColor;

            // Fade in
            Fade(1f, totalDuration * 0.15f);

            // Scale the size out then back in for a bounce effect
            Scale(popupScaleSize, totalDuration * 0.3f, Ease.OutBack, () =>
            {
                Scale(initialScaleSize, totalDuration * 0.7f, Ease.InOutSine);
            });

            // Translate upwards
            Translate(100f, totalDuration * 0.5f, Ease.OutQuad, () =>
            {
                Fade(0f, totalDuration * 0.5f, () =>
                {
                    // Release this object back to the pool
                    pool.Pool.Release(this);
                });
            });
        }

        /// <summary>
        /// Reset the Popup
        /// </summary>
        public void ResetPopup()
        {
            // Set initial variables
            rectTransform.localPosition = initialPosition;
            popupText.color = initialColor;
            popupText.fontSize = initialScaleSize;
        }

        /// <summary>
        /// Translate the Bank Popup text
        /// </summary>
        private void Translate(float endValue, float duration, Ease easeType, TweenCallback onComplete = null)
        {
            // Kill the current translate tween if it exists
            translateTween?.Kill(false);

            // Calculate the target position based on anchoredPosition
            float targetPos = rectTransform.anchoredPosition.x + endValue;

            // Set the tween animation
            translateTween = rectTransform.DOLocalMoveY(targetPos, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onComplete == null) return;

            // Hook up callback events
            translateTween.onComplete += onComplete;
        }

        /// <summary>
        /// Fade the opacity of the Bank Popup text
        /// </summary>
        private void Fade(float endValue, float duration, TweenCallback onComplete = null)
        {
            // Kill the existing color tween
            fadeTween.Kill();

            // Create a new color tween
            fadeTween = popupText.DOFade(endValue, duration);

            // Exit case - no completion action
            if (onComplete == null) return;

            // Hook up completion actions
            fadeTween.onComplete = onComplete;
        }

        /// <summary>
        /// Scale the Bank Popup text
        /// </summary>
        private void Scale(int endValue, float duration, Ease easeType, TweenCallback onComplete = null)
        {
            // Kill the Select Tween if it exists
            scaleTween?.Kill();

            // Set the Select Tween
            scaleTween = DOTween.To(() => popupText.fontSize, x => popupText.fontSize = x, (int)endValue, duration);

            // Set the easing type
            scaleTween.SetEase(easeType);

            // Ignore time scale
            scaleTween.SetUpdate(true);

            // Exit case - there is no completion action
            if (onComplete == null) return;

            // Hook up completion actions
            scaleTween.onComplete += onComplete;
        }

    }
}
