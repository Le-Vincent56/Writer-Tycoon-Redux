using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.WorkCreation.Mediation;
using WriterTycoon.WorkCreation.Ideation.Audience;
using WriterTycoon.WorkCreation.Ideation.Genres;
using WriterTycoon.WorkCreation.Ideation.Topics;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Patterns.ServiceLocator;

namespace WriterTycoon.WorkCreation.Ideation.Compatibility
{
    public enum CompatibilityType
    {
        None = -3,
        Terrible = -2,
        Poor = -1,
        Neutral = 0,
        Good = 1,
        Excellent = 2
    }

    public struct CompatibilityInfo
    {
        public List<CompatibilityType> TopicGenreTypes;
        public List<CompatibilityType> TopicAudienceTypes;
        public float TopicGenreScore;
        public float TopicAudienceScore;
        public float TotalScore;
    }

    public class CompatibilityManager : Dedicant
    {
        private GenreTopicCompatibility genreTopicCompatibility;
        private TopicAudienceCompatibility topicAudienceCompatibility;

        [SerializeField] private List<Topic> topics;
        [SerializeField] private List<Genre> genres;
        [SerializeField] private AudienceType audience;
        [SerializeField] private float compatibilityMultiplier;

        private EventBinding<CalculateCompatibility> calculateCompatibilityEvent;

        public override string Name { get => "Compatibility Manager"; }
        public override DedicantType Type { get => DedicantType.Compatibility; }

        private void Awake()
        {
            // Create a new data base for Genre-Topic compatibility
            genreTopicCompatibility = new GenreTopicCompatibility();
            topicAudienceCompatibility = new TopicAudienceCompatibility();

            // Register this as a service
            ServiceLocator.ForSceneOf(this).Register(this);
        }

        private void OnEnable()
        {
            calculateCompatibilityEvent = new EventBinding<CalculateCompatibility>(SendCompatibilityScore);
            EventBus<CalculateCompatibility>.Register(calculateCompatibilityEvent);
        }

        private void OnDisable()
        {
            EventBus<CalculateCompatibility>.Deregister(calculateCompatibilityEvent);
        }

        /// <summary>
        /// Retrieve the GenreTopicCompatibility scoring object
        /// </summary>
        public GenreTopicCompatibility GetGenreTopicCompatibilities() => genreTopicCompatibility;

        /// <summary>
        /// Retrieve the TopicAudienceCompatibility scoring object
        /// </summary>
        public TopicAudienceCompatibility GetTopicAudienceCompatibilities() => topicAudienceCompatibility;

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
            for (int i = 0; i < genres.Count; i++)
            {
                // Iterate through each topic
                for (int j = 0; j < topics.Count; j++)
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
            for (int i = 0; i < topics.Count; i++)
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
        public float CalculateCompatibilityScore(List<CompatibilityType> compatibilities)
        {
            // Set a container for the Genre-Topic compatibility score
            float compatibilityScore = 0;

            // Iterate through each Genre-Topic compatibility
            foreach (CompatibilityType compatibilityType in compatibilities)
            {
                // Multiply the enum values by the multiplier and add them to the compatibility score
                compatibilityScore += ((int)compatibilityType) * compatibilityMultiplier;
            }

            // Return the average
            return compatibilityScore / compatibilities.Count;
        }

        /// <summary>
        /// Send the Compatibility score
        /// </summary>
        public void SendCompatibilityScore()
        {
            // Get the compatibility types
            List<CompatibilityType> genreTopicCompatibilities = CheckGenreTopicCompatibilities();
            List<CompatibilityType> topicAudienceCompatibilities = CheckTopicAudienceCompatibility();

            // Exit case - if either of the compatibility lists are empty or null
            if (genreTopicCompatibilities == null || genreTopicCompatibilities.Count <= 0) return;
            if (topicAudienceCompatibilities == null || topicAudienceCompatibilities.Count <= 0) return;

            // Calculate the compatibility scores
            float genreTopicScore = CalculateCompatibilityScore(genreTopicCompatibilities);
            float topicAudienceScore = CalculateCompatibilityScore(topicAudienceCompatibilities);

            // Calculate the average of the scores and round it down
            float totalScore = Mathf.FloorToInt((genreTopicScore + topicAudienceScore) / 2f);

            CompatibilityInfo compatibilityInfo = new CompatibilityInfo()
            {
                TopicGenreTypes = genreTopicCompatibilities,
                TopicAudienceTypes = topicAudienceCompatibilities,
                TopicGenreScore = genreTopicScore,
                TopicAudienceScore = topicAudienceScore,
                TotalScore = totalScore,
            };

            // Send the Compatibility info
            Send(new CompatibilityPayload()
                { Content = compatibilityInfo },
                IsType(DedicantType.IdeaReviewer)
            );
        }
    }
}