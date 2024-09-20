using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Development.PointGeneration;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public class FocusSlider : MonoBehaviour
    {
        [SerializeField] private PointCategory category;
        private Slider slider;
        private Text pointText;

        private EventBinding<EndDevelopment> endDevelopmentEvent;

        private void Awake()
        {
            // Verify the slider
            if(slider == null)
                slider = GetComponentInChildren<Slider>();

            // Verify the point text
            if(pointText == null)
                pointText = GetComponentInChildren<Text>();

            // Add event listeners
            slider.onValueChanged.AddListener(DisplayPoints);

            // Set the text to the slider's value
            pointText.text = $"{slider.value}";
        }

        private void OnEnable()
        {
            endDevelopmentEvent = new EventBinding<EndDevelopment>(ResetSlider);
            EventBus<EndDevelopment>.Register(endDevelopmentEvent);
        }

        private void OnDisable()
        {
            EventBus<EndDevelopment>.Deregister(endDevelopmentEvent);
        }

        /// <summary>
        /// Display the amount of points assigned to the slider
        /// </summary>
        private void DisplayPoints(float value)
        {
            // Send the points
            EventBus<SetSliderPoints>.Raise(new SetSliderPoints()
            {
                Category = category,
                Value = (int)value
            });

            // Update the text
            pointText.text = $"{value}";
        }

        /// <summary>
        /// Reset the slider to its default value
        /// </summary>
        private void ResetSlider()
        {
            slider.value = 5;
            pointText.text = $"{slider.value}";
        }
    }
}