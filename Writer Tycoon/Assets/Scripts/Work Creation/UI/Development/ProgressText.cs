using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public class ProgressText : MonoBehaviour
    {
        [SerializeField] private ProgressType progressType;
        [SerializeField] private float fadeDuration;
        private Text displayText;
        private EventBinding<ShowProgressText> showProgressTextEvent;
        private EventBinding<HideProgressText> hideProgressTextEvent;

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

        private void OnEnable()
        {
            showProgressTextEvent = new EventBinding<ShowProgressText>(UpdateProgressText);
            EventBus<ShowProgressText>.Register(showProgressTextEvent);

            hideProgressTextEvent = new EventBinding<HideProgressText>(HideProgressText);
            EventBus<HideProgressText>.Register(hideProgressTextEvent);
        }

        private void OnDisable()
        {
            EventBus<ShowProgressText>.Deregister(showProgressTextEvent);
            EventBus<HideProgressText>.Deregister(hideProgressTextEvent);
        }

        /// <summary>
        /// Callback function to update the progress text
        /// </summary>
        private void UpdateProgressText(ShowProgressText eventData)
        {
            // Update the progress text
            UpdateText(eventData.Text);
        }

        /// <summary>
        /// Callback function to hide the progress text
        /// </summary>
        private void HideProgressText(HideProgressText eventData)
        {
            // Create a tween that fades out and changes the text when completed
            Tween fadeOutTween = displayText.DOFade(0f, fadeDuration);
            fadeOutTween.onComplete += () => displayText.text = string.Empty;
        }

        /// <summary>
        /// Fade out the text, update it, then fade it back in
        /// </summary>
        private void UpdateText(string newText)
        {
            // Create a sequence
            Sequence sequence = DOTween.Sequence();

            // Create a tween that fades out and changes the text when completed
            Tween fadeOutTween = displayText.DOFade(0f, fadeDuration);
            fadeOutTween.onComplete += () => displayText.text = newText;

            // Create a tween that fades in
            Tween fadeInTween = displayText.DOFade(1f, fadeDuration);

            // Add both tweens to the sequence
            sequence.Append(fadeOutTween);
            sequence.Append(fadeInTween);
        }
    }
}