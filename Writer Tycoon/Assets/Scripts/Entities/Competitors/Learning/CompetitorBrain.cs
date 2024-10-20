using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WriterTycoon.WorkCreation.Development.PointGeneration;
using WriterTycoon.WorkCreation.Ideation.Audience;
using WriterTycoon.WorkCreation.Ideation.Compatibility;
using WriterTycoon.WorkCreation.Ideation.Genres;
using WriterTycoon.WorkCreation.Ideation.Topics;

namespace WriterTycoon.Entities.Competitors.Learning
{
    public class CompetitorBrain
    {
        [Header("Learning")]
        [SerializeField] private bool learned;
        [SerializeField] private float learningQ;

        [Header("Characteristics")]
        [SerializeField] private HashSet<Topic> knownTopics;
        [SerializeField] private HashSet<Genre> knownGenres;

        private QLearner qLearner;
        private ReinforcementProblem conceptProblem;
        private ReinforcementProblem focusOneProblem;
        private ReinforcementProblem focusTwoProblem;
        private ReinforcementProblem focusThreeProblem;

        [Header("Scoring Objects")]
        [SerializeField] private GenreTopicCompatibility genreTopicCompatibility;
        [SerializeField] private TopicAudienceCompatibility topicAudienceCompatibility;
        [SerializeField] private GenreFocusTargets genreFocusTargets;

        public CompetitorBrain(
            bool learned, float learningQ, 
            List<Topic> availableTopics, HashSet<TopicType> knownTopics,
            List<Genre> availableGenres, HashSet<GenreType> knownGenres,
            GenreTopicCompatibility genreTopicCompatibility,
            TopicAudienceCompatibility topicAudienceCompatibility,
            GenreFocusTargets genreFocusTargets
        )
        {
            // Set variables
            this.learned = learned;
            this.learningQ = learningQ;

            // Set the known Topics and Genres
            SetKnownTopics(availableTopics, knownTopics);
            SetKnownGenres(availableGenres, knownGenres);

            // Set scoring objects
            this.genreTopicCompatibility = genreTopicCompatibility;
            this.topicAudienceCompatibility = topicAudienceCompatibility;
            this.genreFocusTargets = genreFocusTargets;

            // Initialize the learning components
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
        /// Initialize the Learning variables
        /// </summary>
        public void InitializeLearning()
        {
            // Initialize the Q-Learner
            qLearner = new QLearner();

            // Define the number of actions being made and a dictionary to store
            // corresponding functions
            int conceptActionCount = 0;
            Dictionary<int, Func<float>> availableActionsConcept = new();

            // Store the types of audiences into an array
            Array enumArray = Enum.GetValues(typeof(AudienceType));

            // Create a list of actions by combining each Topic, Genre, and AudienceType
            for (int i = 0; i < knownTopics.Count; i++)
            {
                for (int j = 0; j < knownGenres.Count; j++)
                {
                    for (int k = 1; k < enumArray.Length; k++)
                    {
                        // Get the Topic, Genre, and AudienceType
                        Topic topic = knownTopics.ElementAt(i);
                        Genre genre = knownGenres.ElementAt(j);
                        AudienceType audience = (AudienceType)enumArray.GetValue(k);

                        // Add the action to the Dictionary with its corresponding function
                        availableActionsConcept.Add(
                            conceptActionCount,
                            () => TryConcept(
                                topic,
                                genre,
                                audience
                            )
                        );

                        // Increment the action count
                        conceptActionCount++;
                    }
                }
            }

            // Create the new Concept Reinforcement Problem
            conceptProblem = new ReinforcementProblem(availableActionsConcept);

            int sliderOneActionCount = 0;
            int sliderTwoActionCount = 0;
            int sliderThreeActionCount = 0;
            Dictionary<int, Func<float>> availableActionsFocusSliderOne = new();
            Dictionary<int, Func<float>> availableActionsFocusSliderTwo = new();
            Dictionary<int, Func<float>> availableActionsFocusSliderThree = new();

            // Get all possible, unique Focus Slider combinations
            HashSet<(int s1, int s2, int s3)> focusSliderCombinations = GetSliderCombinations();

            // Create slider combinations for phase one
            foreach ((int s1, int s2, int s3) in focusSliderCombinations)
            {
                foreach(Genre genre in knownGenres)
                {
                    // Add the action to the Dictionary with its corresponding function
                    availableActionsFocusSliderOne.Add(
                        sliderOneActionCount, () =>
                        AttemptSliderCombination(
                            genre,
                            (PointCategory.CharacterSheets, s1),
                            (PointCategory.WorldDocument, s2),
                            (PointCategory.PlotOutline, s3)
                        )
                    );

                    // Increment the action count
                    sliderOneActionCount++;
                }
            }

            // Create slider combinations for phase two
            foreach ((int s1, int s2, int s3) in focusSliderCombinations)
            {
                foreach(Genre genre in knownGenres)
                {
                    // Add the action to the Dictionary with its corresponding function
                    availableActionsFocusSliderTwo.Add(
                        sliderTwoActionCount, () =>
                        AttemptSliderCombination(
                            genre,
                            (PointCategory.Dialogue, s1),
                            (PointCategory.Subplots, s2),
                            (PointCategory.Descriptions, s3)
                        )
                    );

                    // Increment the action count
                    sliderTwoActionCount++;
                }
            }

            // Create slider combinations for phase three
            foreach ((int s1, int s2, int s3) in focusSliderCombinations)
            {
                foreach(Genre genre in knownGenres)
                {
                    // Add the action to the Dictionary with its corresponding function
                    availableActionsFocusSliderThree.Add(
                        sliderThreeActionCount, () =>
                        AttemptSliderCombination(
                            genre,
                            (PointCategory.Emotions, s1),
                            (PointCategory.Twists, s2),
                            (PointCategory.Symbolism, s3)
                        )
                    );

                    // Increment the action count
                    sliderThreeActionCount++;
                }
            }

            // Create the Focus Slider Reinforcement Problem
            focusOneProblem = new ReinforcementProblem(availableActionsFocusSliderOne, true);
            focusTwoProblem = new ReinforcementProblem(availableActionsFocusSliderTwo);
            focusThreeProblem = new ReinforcementProblem(availableActionsFocusSliderThree);
        }

        /// <summary>
        /// Simulate the creation of the Work and provide a reward
        /// </summary>
        private float TryConcept(Topic topic, Genre genre, AudienceType audienceType)
        {
            // Exit case - the Genre-Topic compatibility object does not exist
            if (genreTopicCompatibility == null) return 0f;

            // Exit case - the Topic-Audience compatibility object does not exist
            if (topicAudienceCompatibility == null) return 0f;

            // Get the compatibility types
            CompatibilityType genreTopicCompatibilities = 
                genreTopicCompatibility.GetCompatibility(genre.Type, topic.Type);
            CompatibilityType topicAudienceCompatibilities =
                topicAudienceCompatibility.GetCompatibility(topic.Type, audienceType);

            // Calculate the compatibility scores
            int genreTopicScore = (int)genreTopicCompatibilities;
            int topicAudienceScore = (int)topicAudienceCompatibilities;

            // Calculate the average of the scores and round it down
            float totalScore = Mathf.FloorToInt((genreTopicScore + topicAudienceScore) / 2f);

            return totalScore;
        }

        /// <summary>
        /// Get all valid Focus Slider Combinations
        /// </summary>
        private HashSet<(int s1, int s2, int s3)> GetSliderCombinations()
        {
            // Create a new HashSet to store
            HashSet<(int s1, int s2, int s3)> combinations = new();

            // Iterate over all possible values for the three sliders
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    for (int k = 1; k <= 10; k++)
                    {
                        // Check if the sum of the three sliders equals 15
                        if (i + j + k == 15)
                        {
                            // Store the valid combination
                            combinations.Add((i, j, k));
                        }
                    }
                }
            }

            return combinations;
        }

        /// <summary>
        /// Simulate the setting of Focus Sliders
        /// </summary>
        private float AttemptSliderCombination(
            Genre genre,
            (PointCategory category, int value) sliderOne, 
            (PointCategory category, int value) sliderTwo, 
            (PointCategory category, int value) sliderThree
        )
        {
            // Create an array to store the point stores
            float[] pointScores = new float[3];
            
            // 80% of the SHORT STORY target score divided into each slider component
            float componentScore = (1000 * 0.8f) / 9f;
            float idealSliderScore = componentScore * 3f;

            // Calculate the point scores
            pointScores[0] = CalculatePointScore(
                genre.Type, sliderOne.category, sliderOne.value, componentScore
            );
            pointScores[1] = CalculatePointScore(
                genre.Type, sliderTwo.category, sliderTwo.value, componentScore
            );
            pointScores[2] = CalculatePointScore(
                genre.Type, sliderThree.category, sliderThree.value, componentScore
            );

            // Store the final score
            float finalScore = 0f;

            // Iterate through each point score
            for(int i = 0; i < pointScores.Length; i++)
            {
                // Add on to the final score
                finalScore += pointScores[i];
            }

            //Debug.Log($"Phase Slider Results: {finalScore / idealSliderScore}" +
            //    $"\nSlider One: {pointScores[0]}" +
            //    $"\nSlider Two: {pointScores[1]}" +
            //    $"\nSlider Three: {pointScores[2]}" +
            //    $"\nFinal Score: {finalScore}" +
            //    $"Ideal Slider Score: {idealSliderScore}");

            // Return the percentage of the final score compared to the ideal
            return finalScore / idealSliderScore;
        }

        /// <summary>
        /// Calculate the score for a Point Category and a given value
        /// </summary>
        private float CalculatePointScore(GenreType genreType, PointCategory pointCategory, int value, float componentScore)
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
    }
}