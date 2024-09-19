using UnityEngine;
using UnityEngine.UI;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public class FocusSlider : MonoBehaviour
    {
        private Slider slider;
        private Text pointText;

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
        }

        /// <summary>
        /// Display the amount of points assigned to the slider
        /// </summary>
        private void DisplayPoints(float value)
        {
            pointText.text = $"{value}";
        }
    }
}