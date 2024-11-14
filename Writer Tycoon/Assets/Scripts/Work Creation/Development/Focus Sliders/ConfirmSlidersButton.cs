using UnityEngine;
using UnityEngine.UI;
using GhostWriter.Patterns.EventBus;
using GhostWriter.WorkCreation.Development.Tracker;
using GhostWriter.WorkCreation.UI.Development;

namespace GhostWriter.WorkCreation.Development.FocusSliders
{
    public class ConfirmSlidersButton : MonoBehaviour
    {
        private FocusSliderWindow sliderWindow;

        [SerializeField] private DevelopmentPhase intendedPhase;
        private Button button;

        public void Initialize(FocusSliderWindow sliderWindow)
        {
            // Verify the button
            if (button == null)
                button = GetComponent<Button>();

            // Set references
            this.sliderWindow = sliderWindow;

            button.onClick.AddListener(SendSliderPoints);
        }

        private void SendSliderPoints()
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