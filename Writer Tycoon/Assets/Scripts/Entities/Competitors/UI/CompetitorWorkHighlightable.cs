using GhostWriter.WorkCreation.Publication;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GhostWriter.Entities.Competitors.UI
{
    public class CompetitorWorkHighlightable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Image image;
        [SerializeField] private Text titleText;
        [SerializeField] private Text detailsText;
        [SerializeField] private Text salesText;
        private PublishedWork publishedWork;
        private CompetitorWorkTooltip tooltip;

        private string topicText;
        private string genreText;

        /// <summary>
        /// Initialize the Work Highlightable
        /// </summary>
        public void Initialize(PublishedWork publishedWork, CompetitorWorkTooltip tooltip)
        {
            // Get components
            image = GetComponent<Image>();

            // Set variables
            this.publishedWork = publishedWork;
            this.tooltip = tooltip;

            // Start naming the Topics
            topicText = "";

            // Iterate through each Topic
            for (int i = 0; i < publishedWork.Topics.Count; i++)
            {
                // Add the Topic name
                topicText += publishedWork.Topics[i].Name;

                // Add a comma for formatting
                if (i < publishedWork.Topics.Count - 1)
                    topicText += ", ";
            }

            // Start naming the Genres
            genreText = "";

            // Iterate through each Topic
            for (int i = 0; i < publishedWork.Genres.Count; i++)
            {
                // Add the Topic name
                genreText += publishedWork.Genres[i].Name;

                // Add a comma for formatting
                if (i < publishedWork.Genres.Count - 1)
                    genreText += ", ";
            }

            // Set the text
            titleText.text = publishedWork.Title;
            detailsText.text = $"{topicText} | {genreText} | {publishedWork.Audience}";
            salesText.text = $"Total Sales: {FormatNumber(publishedWork.CumulativeSales)}";
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Show the Tooltip
            tooltip.ShowTooltip(
                publishedWork.Title, publishedWork.Description,
                topicText, genreText,
                publishedWork.Audience.ToString(), publishedWork.Type.ToString(),
                FormatNumber(publishedWork.CumulativeSales)
            );
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Hide the Tooltip
            tooltip.HideTooltip();
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
            else if (value < 1_000_000_000)
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
