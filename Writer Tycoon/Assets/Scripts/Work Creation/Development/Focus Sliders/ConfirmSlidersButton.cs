using UnityEngine;
using UnityEngine.UI;
using GhostWriter.Patterns.EventBus;
using GhostWriter.WorkCreation.Development.Tracker;
using GhostWriter.WorkCreation.UI.Development;
using GhostWriter.World.GeneralUI;

namespace GhostWriter.WorkCreation.Development.FocusSliders
{
    public class ConfirmSlidersButton : CodeableButton
    {
        private FocusSliderWindow sliderWindow;

        [SerializeField] private DevelopmentPhase intendedPhase;

        protected override void Awake()
        {
            // Noop
        }

        /// <summary>
        /// Initialize the Focus Slider Window
        /// </summary>
        public void Initialize(FocusSliderWindow sliderWindow)
        {
            // Verify the button
            if (button == null)
                button = GetComponent<Button>();

            // Set references
            this.sliderWindow = sliderWindow;

            // Hook up button references
            button.onClick.AddListener(OnClick);
        }

        protected override void OnClick()
        {
            // Send the points
            EventBus<SendSliderPoints>.Raise(new SendSliderPoints()
            {
                Hash = sliderWindow.GetCurrentHash()
            });

            // Close the window
            EventBus<CloseSliderWindow>.Raise(new CloseSliderWindow());
        }
    }
}