using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GhostWriter.Patterns.EventBus;
using GhostWriter.WorkCreation.Ideation.Audience;
using GhostWriter.WorkCreation.Ideation.Genres;
using GhostWriter.WorkCreation.Ideation.Topics;
using GhostWriter.WorkCreation.Ideation.WorkTypes;
using GhostWriter.WorkCreation.Publication;

namespace GhostWriter.WorkCreation.UI.Publication
{
    public class PublicationHistoryDisplay : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Text titleText;
        [SerializeField] private Text authorText;
        [SerializeField] private Text typeText;
        [SerializeField] private Text audienceText;
        [SerializeField] private Text topicText;
        [SerializeField] private Text genreText;
        [SerializeField] private Text salesText;
        [SerializeField] private Text scoreText;

        private PublishedWork currentData;
        private EventBinding<SetPublicationDisplayData> setPublicationDisplayDataEvent;

        private void Awake()
        {
            // Set an initial null data
            currentData = null;
        }

        private void OnEnable()
        {
            setPublicationDisplayDataEvent = new EventBinding<SetPublicationDisplayData>(SetData);
            EventBus<SetPublicationDisplayData>.Register(setPublicationDisplayDataEvent);
        }

        private void OnDisable()
        {
            EventBus<SetPublicationDisplayData>.Deregister(setPublicationDisplayDataEvent);
        }

        /// <summary>
        /// Callback function to set the display data
        /// </summary>
        private void SetData(SetPublicationDisplayData eventData)
        {
            // Set the data
            currentData = eventData.PublishedWork;

            // Display the data
            DisplayData();
        }

        /// <summary>
        /// Display the data
        /// </summary>
        private void DisplayData()
        {
            titleText.text = currentData?.Title;
            authorText.text = currentData?.Author;
            typeText.text = GetTypeText(currentData?.Type);
            audienceText.text = GetAudienceText(currentData?.Audience);
            topicText.text = GetTopicsText(currentData?.Topics);
            genreText.text = GetGenresText(currentData?.Genres);
            salesText.text = currentData?.CumulativeSales.ToString("N0");
            scoreText.text = $"{currentData?.Score}%";
        }

        /// <summary>
        /// Get the Type tdisplay text
        /// </summary>
        private string GetTypeText(WorkType? type)
        {
            return type switch
            {
                WorkType.None => "None",
                WorkType.Poetry => "Poem",
                WorkType.FlashFiction => "Flash Fiction",
                WorkType.ShortStory => "Short Story",
                WorkType.Novella => "Novella",
                WorkType.Novel => "Novel",
                WorkType.Screenplay => "Screenplay",
                null => string.Empty,
                _ => "None"
            };
        }

        /// <summary>
        /// Get the Audience display text
        /// </summary>
        private string GetAudienceText(AudienceType? type)
        {
            return type switch
            {
                AudienceType.None => "None",
                AudienceType.Children => "Children",
                AudienceType.Teens => "Teens",
                AudienceType.YoungAdults => "Young Adults",
                AudienceType.Adults => "Adults",
                null => string.Empty,
                _ => "None"
            };
        }

        /// <summary>
        /// Get the Topics display text
        /// </summary>
        private string GetTopicsText(List<Topic> topics)
        {
            // Exit case - there are no Topics
            if (topics == null) return string.Empty;

            // Create a container string
            string topicsText = "";

            // Iterate through each Topic
            for(int i = 0; i < topics.Count; i++)
            {
                // Add the Topic name to the string
                topicsText += topics[i].Name;

                // Skip if the second to last or onward
                if (i >= topics.Count - 1) continue;

                // Add a comma
                topicsText += ", ";
            }

            return topicsText;
        }

        /// <summary>
        /// Get the Genres display text
        /// </summary>
        private string GetGenresText(List<Genre> genres)
        {
            // Exit case - there are no Genres
            if (genres == null) return string.Empty;

            // Create a container string
            string genresText = "";

            // Iterate through each Genre
            for (int i = 0; i < genres.Count; i++)
            {
                // Add the Genre name to the string
                genresText += genres[i].Name;

                // Skip if the second to last or onward
                if (i >= genres.Count - 1) continue;

                // Add a comma
                genresText += ", ";
            }

            return genresText;
        }
    }
}