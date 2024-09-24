using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Development.Tracker;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public class SliderPointsText : MonoBehaviour
    {
        [SerializeField] private DevelopmentPhase intendedPhase;
        private Text displayText;

        private EventBinding<SetSliderPointText> setSliderPointTextEvent;

        private void Awake()
        {
            // Verify the Text component
            if(displayText == null)
                displayText = GetComponent<Text>();
        }

        private void OnEnable()
        {
            setSliderPointTextEvent = new EventBinding<SetSliderPointText>(UpdateText);
            EventBus<SetSliderPointText>.Register(setSliderPointTextEvent);
        }

        private void OnDisable()
        {
            EventBus<SetSliderPointText>.Deregister(setSliderPointTextEvent);
        }

        /// <summary>
        /// Callback function to update the text with how many slider points the player has
        /// remaining
        /// </summary>
        private void UpdateText(SetSliderPointText eventData)
        {
            // Exit case - if not the intended phase
            if (eventData.IntendedPhase != intendedPhase) return;

            displayText.text = $"POINTS REMAINING: {eventData.PointsRemaining}";
        }
    }
}