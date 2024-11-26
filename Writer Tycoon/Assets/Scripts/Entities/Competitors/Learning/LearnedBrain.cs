using GhostWriter.WorkCreation.Development.PointGeneration;
using GhostWriter.WorkCreation.Ideation.Audience;
using GhostWriter.WorkCreation.Ideation.Compatibility;
using GhostWriter.WorkCreation.Ideation.Genres;
using GhostWriter.WorkCreation.Ideation.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GhostWriter.Entities.Competitors.Learning
{
    public class LearnedBrain : CompetitorBrain
    {
        [Header("Learning")]
        [SerializeField] float learnFactor;
        [SerializeField] float discountFactor;
        [SerializeField] float explorationFactor;

        [SerializeField] private QLearner conceptLearner;
        [SerializeField] private QLearner focusOneLearner;
        [SerializeField] private QLearner focusTwoLearner;
        [SerializeField] private QLearner focusThreeLearner;
        [SerializeField] private ReinforcementProblem workProblem;

        private (float value, ActionData data) currentConcept;
        private (float value, ActionData data) currentFocusOne;
        private (float value, ActionData data) currentFocusTwo;
        private (float value, ActionData data) currentFocusThree;

        /// <summary>
        /// Initialize the Learning variables
        /// </summary>
        public override void InitializeLearning()
        {
            // Initialize the Q-Learners
            conceptLearner = new QLearner();
            focusOneLearner = new QLearner();
            focusTwoLearner = new QLearner();
            focusThreeLearner = new QLearner();

            // Define the number of actions being made and a dictionary to store
            // corresponding functions
            int conceptActionCount = 0;
            Dictionary<int, Func<(float value, object data)>> availableActionsConcept = new();

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

            // Create a Dictionary to map a Genre to its tested slider combinations
            Dictionary<Genre, List<Dictionary<int, Func<(float value, object data)>>>> genreFocusActions = new();

            // Get all possible, unique Focus Slider combinations
            HashSet<(int s1, int s2, int s3)> focusSliderCombinations = GetSliderCombinations();

            foreach (Genre genre in knownGenres)
            {
                int sliderOneActionCount = 0;
                int sliderTwoActionCount = 0;
                int sliderThreeActionCount = 0;

                Dictionary<int, Func<(float value, object data)>> genreFocusActionsSliderOne = new();
                Dictionary<int, Func<(float value, object data)>> genreFocusActionsSliderTwo = new();
                Dictionary<int, Func<(float value, object data)>> genreFocusActionsSliderThree = new();

                // Create the slider combinations for phase one
                foreach ((int s1, int s2, int s3) in focusSliderCombinations)
                {
                    genreFocusActionsSliderOne.Add(
                        sliderOneActionCount, () =>
                        AttemptSliderCombination(
                            genre,
                            (PointCategory.CharacterSheets, s1),
                            (PointCategory.WorldDocument, s2),
                            (PointCategory.PlotOutline, s3)
                        )
                    );

                    sliderOneActionCount++;
                }

                // Create slider combinations for phase two
                foreach ((int s1, int s2, int s3) in focusSliderCombinations)
                {
                    // Add the action to the Dictionary with its corresponding function
                    genreFocusActionsSliderTwo.Add(
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

                // Create slider combinations for phase three
                foreach ((int s1, int s2, int s3) in focusSliderCombinations)
                {
                    // Add the action to the Dictionary with its corresponding function
                    genreFocusActionsSliderThree.Add(
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

                // Create the list of Actions
                List<Dictionary<int, Func<(float value, object data)>>> genreFocusActionsList = new()
                {
                    genreFocusActionsSliderOne,
                    genreFocusActionsSliderTwo,
                    genreFocusActionsSliderThree,
                };

                // Add it to the Dictionary
                genreFocusActions.Add(genre, genreFocusActionsList);
            }

            // Create the new Concept Reinforcement Problem
            workProblem = new ReinforcementProblem(
                availableActionsConcept,
                genreFocusActions
            );
        }

        /// <summary>
        /// Set the learning variables
        /// </summary>
        public void SetVariables(float learnFactor, float discountFactor, float explorationFactor)
        {
            this.learnFactor = learnFactor;
            this.discountFactor = discountFactor;
            this.explorationFactor = explorationFactor;
        }

        /// <summary>
        /// Simulate the creation of the Work and provide a reward
        /// </summary>
        private (float value, object data) TryConcept(Topic topic, Genre genre, AudienceType audienceType)
        {
            // Exit case - the Genre-Topic compatibility object does not exist
            if (genreTopicCompatibility == null) return (0f, null);

            // Exit case - the Topic-Audience compatibility object does not exist
            if (topicAudienceCompatibility == null) return (0f, null);

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

            return (totalScore, new AIConceptData() { Topic = topic, Genre = genre, Audience = audienceType });
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
        private (float value, object data) AttemptSliderCombination(
            Genre genre,
            (PointCategory category, int value) sliderOne,
            (PointCategory category, int value) sliderTwo,
            (PointCategory category, int value) sliderThree
        )
        {
            // Create an array to store the point stores
            float[] pointScores = new float[3];

            // 80% of the SHORT STORY target score divided into each slider component
            float componentScore = targetScore / 9f;
            float idealFocusScore = componentScore * 3f;

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
            for (int i = 0; i < pointScores.Length; i++)
            {
                // Add on to the final score
                finalScore += pointScores[i];
            }

            // Return the percentage of the final score compared to the ideal
            return
                (
                    finalScore / idealFocusScore,
                    new AISliderData() { Genre = genre.Name, SliderOne = sliderOne, SliderTwo = sliderTwo, SliderThree = sliderThree }
                );
        }

        /// <summary>
        /// Learn about a specific problem
        /// </summary>
        public override void Learn(Problem problemToLearn)
        {
            switch (problemToLearn)
            {
                case Problem.Concept:
                    Debug.Log(" ----- LEARNING CONCEPT ------");
                    conceptLearner.RunQLearningStep(workProblem, 0, 3, learnFactor, discountFactor, explorationFactor);
                    currentConcept = conceptLearner.GetBestAction(true);
                    break;

                case Problem.FocusOne:
                    Debug.Log(" ----- LEARNING FOCUS ONE ------");

                    // Exit case - the current concept data is the wrong type
                    if (currentConcept.data.Data is not AIConceptData focusOneConceptData) return;

                    // Set a default state
                    int focusOneState = 0;

                    // Iterate through the known genres
                    for (int i = 0; i < knownGenres.Count; i++)
                    {
                        // Skip if the Genres are not equal
                        if (focusOneConceptData.Genre != knownGenres.ElementAt(i)) continue;

                        // Set the state based on the concepted Genre
                        focusOneState = i * 3 + 1;
                    }

                    // Run the learning
                    focusOneLearner.RunQLearningStep(workProblem, focusOneState, 3, learnFactor, discountFactor, explorationFactor);
                    currentFocusOne = focusOneLearner.GetBestAction(true);
                    break;

                case Problem.FocusTwo:
                    Debug.Log(" ----- LEARNING FOCUS TWO ------");

                    // Exit case - the current concept data is the wrong type
                    if (currentConcept.data.Data is not AIConceptData focusTwoConceptData) return;

                    // Set a default state
                    int focusTwoState = 0;

                    // Iterate through the known genres
                    for (int i = 0; i < knownGenres.Count; i++)
                    {
                        // Skip if the Genres are not equal
                        if (focusTwoConceptData.Genre != knownGenres.ElementAt(i)) continue;

                        // Set the state based on the concepted Genre
                        focusTwoState = i * 3 + 2;
                    }

                    focusTwoLearner.RunQLearningStep(workProblem, focusTwoState, 3, learnFactor, discountFactor, explorationFactor);
                    currentFocusTwo = focusTwoLearner.GetBestAction(true);
                    break;

                case Problem.FocusThree:
                    Debug.Log(" ----- LEARNING FOCUS THREE ------");

                    // Exit case - the current concept data is the wrong type
                    if (currentConcept.data.Data is not AIConceptData focusThreeConceptData) return;

                    // Set a default state
                    int focusThreeState = 0;

                    // Iterate through the known genres
                    for (int i = 0; i < knownGenres.Count; i++)
                    {
                        // Skip if the Genres are not equal
                        if (focusThreeConceptData.Genre != knownGenres.ElementAt(i)) continue;

                        // Set the state based on the concepted Genre
                        focusThreeState = i * 3 + 3;
                    }

                    focusThreeLearner.RunQLearningStep(workProblem, focusThreeState, 3, learnFactor, discountFactor, explorationFactor);
                    currentFocusThree = focusThreeLearner.GetBestAction(true);
                    break;
            }
        }

        /// <summary>
        /// Rate the work
        /// </summary>
        public override void Rate()
        {
            // Exit case - the Concept data is the wrong type
            if (currentConcept.data.Data is not AIConceptData conceptData) return;

            // Exit case - the Focus One data is null or the wrong type
            if (currentFocusOne.data.Data is not AISliderData focusOneData) return;

            // Exit case - the Focus Two data is null or the wrong type
            if (currentFocusTwo.data.Data is not AISliderData focusTwoData) return;

            // Exit case - the Focus Three data is null or the wrong type
            if (currentFocusThree.data.Data is not AISliderData focusThreeData) return;

            // Set concept data
            Topic chosenTopic = conceptData.Topic;
            Genre chosenGenre = conceptData.Genre;
            AudienceType chosenAudience = conceptData.Audience;

            float phaseScores = targetScore / 3f;
            float focusOneScore = phaseScores * currentFocusOne.data.Value;
            float focusTwoScore = phaseScores * currentFocusTwo.data.Value;
            float focusThreeScore = phaseScores * currentFocusThree.data.Value;

            // Calculate the total points
            float totalPoints = focusOneScore + focusTwoScore + focusThreeScore;

            // Creat the rating string
            string ratingString = "Rating: ";
            ratingString += $"\nTopic: {chosenTopic.Name}" +
                $"\nGenre: {chosenGenre.Name}" +
                $"\nAudience: {chosenAudience}" +
                $"\nFocus One Total: {focusOneScore}" +
                $"\nFocus Two Total: {focusTwoScore}" +
                $"\nFocus Three Total: {focusThreeScore}" +
                $"\nTotal Points: {totalPoints / targetScore}";

            Debug.Log(ratingString);
        }
    }
}
