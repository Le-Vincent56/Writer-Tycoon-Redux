using System.Collections.Generic;
using UnityEngine;
using GhostWriter.WorkCreation.Development.PointGeneration;
using GhostWriter.WorkCreation.Ideation.Audience;
using GhostWriter.WorkCreation.Ideation.Compatibility;
using GhostWriter.WorkCreation.Ideation.Genres;
using GhostWriter.WorkCreation.Ideation.Topics;
using GhostWriter.WorkCreation.Ideation.WorkTypes;

namespace GhostWriter.Entities.Competitors.Learning
{
    public struct AIConceptData
    {
        public Topic Topic;
        public Genre Genre;
        public AudienceType Audience;
    }

    public struct AISliderData
    {
        public string Genre;
        public (PointCategory category, int value) SliderOne;
        public (PointCategory category, int value) SliderTwo;
        public (PointCategory category, int value) SliderThree;
    }

    public enum Problem
    {
        Concept,
        FocusOne,
        FocusTwo,
        FocusThree
    }

    public abstract class CompetitorBrain : MonoBehaviour
    {
        [Header("Characteristics")]
        [SerializeField] protected WorkType workType;
        [SerializeField] protected float targetScore;
        [SerializeField] protected HashSet<Topic> knownTopics;
        [SerializeField] protected HashSet<Genre> knownGenres;

        [Header("Scoring Objects")]
        [SerializeField] protected GenreTopicCompatibility genreTopicCompatibility;
        [SerializeField] protected TopicAudienceCompatibility topicAudienceCompatibility;
        [SerializeField] protected GenreFocusTargets genreFocusTargets;

        /// <summary>
        /// Initialize the Competitor Brain
        /// </summary>
        public void InitializeBrain(
            WorkType workType, int targetScore,
            List<Topic> availableTopics, HashSet<TopicType> knownTopics,
            List<Genre> availableGenres, HashSet<GenreType> knownGenres,
            GenreTopicCompatibility genreTopicCompatibility,
            TopicAudienceCompatibility topicAudienceCompatibility,
            GenreFocusTargets genreFocusTargets)
        {
            // Set the work type and associated target score
            this.workType = workType;
            this.targetScore = targetScore * 0.8f; // Can only earn up to 80% with sliders

            // Set the known Topics and Genres
            SetKnownTopics(availableTopics, knownTopics);
            SetKnownGenres(availableGenres, knownGenres);

            // Set scoring objects
            this.genreTopicCompatibility = genreTopicCompatibility;
            this.topicAudienceCompatibility = topicAudienceCompatibility;
            this.genreFocusTargets = genreFocusTargets;

            // Initialize learning
            InitializeLearning();
        }

        /// <summary>
        /// Set the Competitor's known Topics
        /// </summary>
        public void SetKnownTopics(List<Topic> availableTopics, HashSet<TopicType> topicData)
        {
            // Initialize the known Topics HashSet if not already set
            knownTopics ??= new();

            // Iterate through each Topic
            foreach (Topic topicObj in availableTopics)
            {
                // Iterate through each TopicType
                foreach (TopicType topicType in topicData)
                {
                    // Skip if the types are not equal
                    if (topicObj.Type != topicType) continue;

                    // Copy over the Topic
                    Topic topicToAdd = new(topicObj);

                    // Add the copied Topic to the known Topics
                    knownTopics.Add(topicToAdd);
                }
            }

            // Iterate through each Topic in the known Topics
            foreach (Topic topic in knownTopics)
            {
                // Unlock the Topic for the Competitor
                topic.Unlock();
            }
        }

        /// <summary>
        /// Set the Competitor's known Genres
        /// </summary>
        public void SetKnownGenres(List<Genre> availableGenres, HashSet<GenreType> genreData)
        {
            // Initialize the known Genres HashSet if not already set
            knownGenres ??= new();

            // Iterate through each Genre
            foreach (Genre genreObj in availableGenres)
            {
                // Iterate through each GenreType
                foreach (GenreType genreType in genreData)
                {
                    // Skip if the types are not equal
                    if (genreObj.Type != genreType) continue;

                    // Copy over the Genre
                    Genre genreToAdd = new(genreObj);

                    // Add the copied Genre to the known Genres
                    knownGenres.Add(genreToAdd);
                }
            }

            // Iterate through each Genre in the known Genres
            foreach (Genre genre in knownGenres)
            {
                // Unlock the Genre for the Competitor
                genre.Unlock();
            }
        }

        /// <summary>
        /// Calculate the score for a Point Category and a given value
        /// </summary>
        protected float CalculatePointScore(GenreType genreType, PointCategory pointCategory, int value, float componentScore)
        {
            // Start counting a total multiplier
            float totalMultiplier = 0;

            // Get the target value for the Genre
            int targetValue = genreFocusTargets.GetTargetScore(genreType, pointCategory);

            // Get the absolute difference of the given values
            int difference = Mathf.Abs(value - targetValue);

            // Get the step value for calculating percentages (symmetrically)
            float stepValue = (targetValue >= 5f)
                ? 1f / targetValue
                : 1f / (10f - targetValue);

            // Calculate the percentage loss
            float percentageLoss = difference * stepValue;

            // Get the multiplier
            float multiplier = 1f - percentageLoss;

            // Clamp so that the most someone can get if 5%
            multiplier = Mathf.Clamp(multiplier, 0.05f, 1f);

            // Add the multiplier to the total
            totalMultiplier += multiplier;

            // Return the average multiplier times the base component score
            return componentScore * totalMultiplier;
        }

        public abstract void InitializeLearning();

        public abstract void Learn(Problem problem);

        public abstract void Rate();
    }
}