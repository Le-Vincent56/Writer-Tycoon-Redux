using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.UI.Development;

namespace WriterTycoon.WorkCreation.Development.FocusSliders
{
    public class SlidersController : MonoBehaviour
    {
        [SerializeField] private DevelopmentPhase intendedPhase;
        [SerializeField] private Slider[] sliders;
        [SerializeField] private int[] previousValues;

        private void Awake()
        {
            // Attempt to get children Focus Sliders
            FocusSlider[] focusSliders = GetComponentsInChildren<FocusSlider>();

            // Exit case - if no Focus Sliders are found
            if (focusSliders == null) return;

            List<Slider> sliderList = new();
            List<int> initialValues = new();

            // Iterate through each Slider
            foreach (FocusSlider slider in focusSliders)
            {
                // Get the child slider
                Slider childSlider = slider.GetComponentInChildren<Slider>();

                // Add the child slider to the list
                sliderList.Add(childSlider);

                // Add the initial slider value to the list
                initialValues.Add((int)childSlider.value);
            }

            // Cast the lists to arrays for more clear traversal
            sliders = sliderList.ToArray();
            previousValues = initialValues.ToArray();
        }

        private void Start()
        {
            sliders[0].onValueChanged.AddListener((value) => BindSliders(value, sliders[0], ref previousValues[0]));
            sliders[1].onValueChanged.AddListener((value) => BindSliders(value, sliders[1], ref previousValues[1]));
            sliders[2].onValueChanged.AddListener((value) => BindSliders(value, sliders[2], ref previousValues[2]));

            // Initialize the remaining points text
            EventBus<SetSliderPointText>.Raise(new SetSliderPointText()
            {
                IntendedPhase = intendedPhase,
                PointsRemaining = 0
            });
        }

        /// <summary>
        /// Callback function to bind the sliders to a maximum
        /// </summary>
        /// <param name="value"></param>
        private void BindSliders(float value, Slider slider, ref int previousValue)
        {
            // Calculate the total value
            int totalValue = CalculateTotalValue();

            // Check if the total value is above the maximum of 15
            if (totalValue > 15)
            {
                // Subtract the excess from the slider's previous value
                slider.value = previousValue;
            }

            // Update the slider's previous value
            previousValue = (int)slider.value;

            // Calculate the points remaining - don't go below 0
            int pointsRemaining = Mathf.Max(15 - totalValue, 0);

            // Update the remaining points text
            EventBus<SetSliderPointText>.Raise(new SetSliderPointText()
            {
                IntendedPhase = intendedPhase,
                PointsRemaining = pointsRemaining
            });
        }

        /// <summary>
        /// Calculate the total value between all of the Sliders
        /// </summary>
        private int CalculateTotalValue()
        {
            // Create a container for the total value for an initial check
            int totalValue = 0;

            // Iterate through each Slider
            foreach (Slider slider in sliders)
            {
                // Add each slider value
                totalValue += (int)slider.value;
            }

            return totalValue;
        }
    }
}