using GhostWriter.Patterns.EventBus;
using GhostWriter.World.GeneralUI;

namespace GhostWriter.WorkCreation.UI.Publication
{
    public class PublicationBackButton : CodeableButton
    {   
        /// <summary>
        // Set the Publication History Window state to 0 (Shelf)
        /// </summary>
        protected override void OnClick()
        {
            // Set the Shelf state
            EventBus<SetPublicationHistoryState>.Raise(new SetPublicationHistoryState()
            {
                State = 0
            });
        }
    }
}