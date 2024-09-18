using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Development.Tracker;

namespace WriterTycoon.WorkCreation.Development.FocusSliders
{
    public class ConfirmSlidersButton : MonoBehaviour
    {
        [SerializeField] private DevelopmentPhase intendedPhase;
        private Button button;

        private void Awake()
        {
            // Verify the button
            if(button == null)
                button = GetComponent<Button>();

            button.onClick.AddListener(SendSliderPoints);
        }

        private void SendSliderPoints()
        {
            EventBus<CloseSliderWindow>.Raise(new CloseSliderWindow());
        }
    }
}