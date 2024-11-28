using GhostWriter.Patterns.EventBus;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter.Entities.Competitors.UI
{
    public class CompetitorButton : MonoBehaviour
    {
        private Button button;
        [SerializeField] private Text nameText;
        [SerializeField] private Text flavorText;
        [SerializeField] private Text salesText;
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

            // Update the Button's text
            UpdateInfo();
        }

        /// <summary>
        /// Handle Button clicking
        /// </summary>
        private void OnClick()
        {
            // Set the Button's Competitor's Work History to display
            EventBus<SetCompetitorWorkHistory>.Raise(new SetCompetitorWorkHistory()
            {
                Competitor = competitor
            });

            // Set the History state for the Competitor Window
            EventBus<SetCompetitorWindowState>.Raise(new SetCompetitorWindowState()
            {
                State = 1
            });
        }

        /// <summary>
        /// Update the info of the Competitor
        /// </summary>
        public void UpdateInfo()
        {
            nameText.text = competitor.Name;
            flavorText.text = competitor.FlavorText;
            salesText.text = FormatNumber(competitor.TotalSales);
        }

        /// <summary>
        /// Format the number to use K or M if within the thousands or millions, respectively
        /// </summary>
        private string FormatNumber(int value)
        {
            // Check if the value is below 1000
            if (value < 1000)
            {
                // Display the full number
                return value.ToString();
            }
            // Otherwise, check if the value is below a million
            else if (value < 1_000_000)
            {
                // Convert to the thousands
                float thousands = value / 1000f;

                // Add a "K" at the end and floor to one decimal place
                return $"{Mathf.Floor(thousands * 10) / 10}K";
            }
            // Otherwise, check if the value is below a billion
            else if(value < 1_000_000_000)
            {
                // Convert to the millions
                float millions = value / 1_000_000f;

                // Add a "M" at the end and floor to one decimal place
                return $"{Mathf.Floor(millions * 10) / 10}M";
            }
            else
            {
                // Otherwise, convert to the billions
                float billions = value / 1_000_000_000f;

                // Add a "B" at the end and floor to one decimal place
                return $"{Mathf.Floor(billions * 10) / 10}B";
            }
        }
    }
}
