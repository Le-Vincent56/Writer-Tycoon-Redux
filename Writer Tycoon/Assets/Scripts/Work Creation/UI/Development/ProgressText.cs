using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public class ProgressText : MonoBehaviour
    {
        [SerializeField] private float fadeDuration;
        private Text displayText;
        private EventBinding<ShowProgressText> showProgressTextEvent;
        private EventBinding<HideProgressText> hideProgressTextEvent;

        Tween fadeTween;

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

        private void UpdateProgressText(ShowProgressText eventData)
        {
            UpdateText(eventData.Text);
        }

        private void HideProgressText()
        {
            // Create a tween that fades out and changes the text when completed
            Tween fadeOutTween = displayText.DOFade(0f, fadeDuration);
            fadeOutTween.onComplete += () => displayText.text = string.Empty;
        }

        private void UpdateText(string newText)
        {
            Sequence sequence = DOTween.Sequence();

            // Create a tween that fades out and changes the text when completed
            Tween fadeOutTween = displayText.DOFade(0f, fadeDuration);
            fadeOutTween.onComplete += () => displayText.text = newText;

            Tween fadeInTween = displayText.DOFade(1f, fadeDuration);

            sequence.Append(fadeOutTween);
            sequence.Append(fadeInTween);
        }
    }
}