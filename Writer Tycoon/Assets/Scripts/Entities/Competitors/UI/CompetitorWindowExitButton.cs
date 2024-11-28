using GhostWriter.Patterns.EventBus;
using GhostWriter.World.GeneralUI;

namespace GhostWriter.Entities.Competitors.UI
{
    public class CompetitorWindowExitButton : CodeableButton
    {
        /// <summary>
        /// Handle button clicking
        /// </summary>
        protected override void OnClick()
        {
            // Close the Competitor Window
            EventBus<CloseCompetitorWindow>.Raise(new CloseCompetitorWindow());
        }
    }
}
