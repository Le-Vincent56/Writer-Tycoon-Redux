using GhostWriter.Patterns.EventBus;
using GhostWriter.World.GeneralUI;

namespace GhostWriter.Entities.Competitors.UI
{
    public class CompetitorHistoryBackButton : CodeableButton
    {
        /// <summary>
        /// Handle button clicking
        /// </summary>
        protected override void OnClick()
        {
            // Set the List state
            EventBus<SetCompetitorWindowState>.Raise(new SetCompetitorWindowState()
            {
                State = 0
            });
        }
    }
}
