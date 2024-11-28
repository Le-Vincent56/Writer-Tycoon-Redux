using GhostWriter.Patterns.EventBus;
using GhostWriter.World.GeneralUI;

namespace GhostWriter.WorkCreation.UI.Publication
{
    public class PublicationExitButton : CodeableButton
    {
        /// <summary>
        /// On Click function to close the Publication History
        /// </summary>
        protected override void OnClick()
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