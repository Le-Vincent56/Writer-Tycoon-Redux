using DG.Tweening;
using UnityEngine;

namespace WriterTycoon.WorkCreation.UI.Rating
{
    public class Review : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Tweening Variables")]
        [SerializeField] private float fadeDuration;
        [SerializeField] private Tween fadeTween;

        private void Awake()
        {
            // Verify the Canvas Group component
            if(canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// Handle fading for the text
        /// </summary>
        public Tween Fade(float endFadeValue, Ease easeType = Ease.OutQuint)
        {
            // Kill the current fade tween if it exists
            fadeTween?.Kill(false);

            // Set the fade animation
            fadeTween = canvasGroup.DOFade(endFadeValue, fadeDuration)
                .SetEase(easeType);

            return fadeTween;
        }
    }
}