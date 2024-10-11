using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.UI.Publication
{
    public class PublicationExitButton : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            // Verify the Button component
            if(button == null) 
                button = GetComponent<Button>();

            button.onClick.AddListener(OnClick);
        }

        /// <summary>
        /// On Click function to close the Publication History
        /// </summary>
        private void OnClick()
        {
            // Close the Publication History window
            EventBus<ClosePublicationHistory>.Raise(new ClosePublicationHistory());

            // Unpause the Calendar
            EventBus<ChangeCalendarPauseState>.Raise(new ChangeCalendarPauseState()
            {
                Paused = false,
                AllowSpeedChanges = true
            });
        }
    }
}