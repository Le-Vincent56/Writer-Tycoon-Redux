using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.WorkCreation.Audience;
using WriterTycoon.WorkCreation.Genres;
using WriterTycoon.WorkCreation.Mediation;
using WriterTycoon.WorkCreation.Topics;
using WriterTycoon.WorkCreation.WorkType;

namespace WriterTycoon.WorkCreation.Compatibility
{
    public enum CompatibilityType
    {
        None,
        Excellent,
        Good,
        Neutral,
        Poor,
        Terrible
    }

    public class CompatibilityManager : Dedicant
    {
        private GenreTopicCompatibility genreTopicCompatibility;
        private TopicAudienceCompatibility topicAudienceCompatibility;

        [SerializeField] private List<Topic> topics;
        [SerializeField] private List<Genre> genres;
        [SerializeField] private AudienceType audience;
        [SerializeField] private WorkType.WorkType workType;

        public override string Name { get => "Compatibility Manager"; }
        public override DedicantType Type { get => DedicantType.Compatibility; }

        private void Awake()
        {
            // Create a new data base for Genre-Topic compatibility
            genreTopicCompatibility = new GenreTopicCompatibility();
            topicAudienceCompatibility = new TopicAudienceCompatibility();
        }

        /// <summary>
        /// Set the selected Work Type
        /// </summary>
        public void SetWorkType(WorkType.WorkType workType) => this.workType = workType;

        /// <summary>
        /// Set the selected Audience
        /// </summary>
        public void SetAudience(AudienceType audience) => this.audience = audience;

        /// <summary>
        /// Set the selected Topics
        /// </summary>
        public void SetTopics(List<Topic> topics) => this.topics = topics;

        /// <summary>
        /// Set the selected Genres
        /// </summary>
        public void SetGenres(List<Genre> genres) => this.genres = genres;

        /// <summary>
        /// Check the Genre-Topic compatibilities
        /// </summary>
        public List<CompatibilityType> CheckGenreTopicCompatibilities()
        {
            // Exit case - there are no Topics selected
            if (topics.Count <= 0) return null;

            // Exit case - there are no Genres selected
            if (genres.Count <= 0) return null;

            // Exit case - the Genre-Topic compatibility object does not exist
            if (genreTopicCompatibility == null) return null;

            // Create a list to store the compatibilities
            List<CompatibilityType> compatibilities = new();

            // Iterate through each genre
            for(int i = 0; i < genres.Count; i++)
            {
                // Iterate through each topic
                for(int j = 0; j < topics.Count; j++)
                {
                    // Ignore if the topic has compatibility ignored
                    if (topics[j].IgnoreGenreCompatibility) continue;

                    // Add the compatibility for the Genre-Topic to the list
                    compatibilities.Add(
                        genreTopicCompatibility.GetCompatibility(genres[i].Type, topics[j].Type)
                    );
                }
            }

            return compatibilities;
        }

        /// <summary>
        /// Check the Audience-Topic compatibilities
        /// </summary>
        public List<CompatibilityType> CheckTopicAudienceCompatibility()
        {
            // Exit case - there are no Topics selected
            if (topics.Count < 0) return null;

            // Exit case - there is no Audience selected
            if (audience == AudienceType.None) return null;

            // Exit case - the Topic-Audience compatibility object does not exist
            if (topicAudienceCompatibility == null) return null;

            // Create a list to store the compatibilities
            List<CompatibilityType> compatibilities = new();

            // Iterate through each genre
            for(int i = 0; i < topics.Count; i++)
            {
                // Add the compatibility for the Topic-Audience to the list
                compatibilities.Add(
                    topicAudienceCompatibility.GetCompatibility(topics[i].Type, audience)
                );
            }

            return compatibilities;
        }

        /// <summary>
        /// Calculate the total compatibility score
        /// </summary>
        public void CalculateCompatibilityScore()
        {
            // Get the compatibilities
            List<CompatibilityType> genreTopicCompatibilities = CheckGenreTopicCompatibilities();
            List<CompatibilityType> topicAudienceCompatibilities = CheckTopicAudienceCompatibility();

            string genreTopicString = $"Genre Compatibilities (";

            for(int i = 0; i < genres.Count; i++)
            {
                genreTopicString += $"{genres[i].Name}";

                if (i < genres.Count - 1) genreTopicString += "/";
            }

            genreTopicString += ") ";
            genreTopicString += " [";

            for(int i = 0; i < topics.Count; i++)
            {
                genreTopicString += $"{topics[i].Name}";

                if (i < topics.Count - 1) genreTopicString += ", ";
            }

            genreTopicString += "]: ";

            for (int i = 0; i < genreTopicCompatibilities.Count; i++)
            {
                genreTopicString += $"{genreTopicCompatibilities[i]}";

                if (i < genreTopicCompatibilities.Count - 1) genreTopicString += ", ";
            }

            string topicAudienceString = $"Audience Compatibilities ({audience}) [";

            for (int i = 0; i < topics.Count; i++)
            {
                topicAudienceString += $"{topics[i].Name}";

                if (i < topics.Count - 1) topicAudienceString += ", ";
            }

            topicAudienceString += "]: ";

            for (int i = 0; i < topicAudienceCompatibilities.Count; i++)
            {
                topicAudienceString += $"{topicAudienceCompatibilities[i]}";

                if (i < topicAudienceCompatibilities.Count - 1) topicAudienceString += ", ";
            }

            Debug.Log(genreTopicString);
            Debug.Log(topicAudienceString);
        }
    }
}