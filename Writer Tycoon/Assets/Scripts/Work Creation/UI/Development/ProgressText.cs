using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public class ProgressText : MonoBehaviour
    {
        [SerializeField] private float fadeDuration;
        private Text displayText;

        private void Awake()
        {
            // Verify the text component
            if(displayText == null)
                displayText = GetComponent<Text>();

            // Hide the text
            Color invisible = displayText.color;
            invisible.a = 0f;
            displayText.color = invisible;
        }

        /// <summary>
        /// Fade out the text, update it, then fade it back in
        /// </summary>
        public void FadeInAndSetText(string newText)
        {
            // Create a sequence
            Sequence sequence = DOTween.Sequence();

            // Create a tween that fades out and changes the text when completed
            Tween fadeOutTween = displayText.DOFade(0f, fadeDuration);
            fadeOutTween.onComplete += () => UpdateText(newText);

            // Create a tween that fades in
            Tween fadeInTween = displayText.DOFade(1f, fadeDuration);

            // Add both tweens to the sequence
            sequence.Append(fadeOutTween);
            sequence.Append(fadeInTween);
        }

        /// <summary>
        /// Update the Text
        /// </summary>
        public void UpdateText(string newText) => displayText.text = newText;

        /// <summary>
        /// Fade out the Text
        /// </summary>
        public void FadeOutText()
        {
            // Create a tween that fades out and changes the text when completed
            Tween fadeOutTween = displayText.DOFade(0f, fadeDuration);
            fadeOutTween.onComplete += () => displayText.text = string.Empty;
        }
    }
}