using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter.Entities.Competitors.UI
{
    public class CompetitorButton : MonoBehaviour
    {
        private Button button;
        private NPCCompetitor competitor;

        /// <summary>
        /// Initialize the Competitor Button
        /// </summary>
        public void Initialize(NPCCompetitor competitor)
        {
            // Get components
            button = GetComponent<Button>();

            // Set the Competitor
            this.competitor = competitor;

            // Hook up click actions
            button.onClick.AddListener(OnClick);
        }

        /// <summary>
        /// Handle Button clicking
        /// </summary>
        private void OnClick()
        {

        }
    }
}
