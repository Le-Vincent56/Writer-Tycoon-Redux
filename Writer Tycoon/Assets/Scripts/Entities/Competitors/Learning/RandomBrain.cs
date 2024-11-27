using GhostWriter.WorkCreation.Development.PointGeneration;
using GhostWriter.WorkCreation.Ideation.Audience;
using GhostWriter.WorkCreation.Ideation.Genres;
using GhostWriter.WorkCreation.Ideation.Topics;
using System;
using System.Linq;
using UnityEngine;

namespace GhostWriter.Entities.Competitors.Learning
{
    public class RandomBrain : CompetitorBrain
    {
        [SerializeField] private AIConceptData currentConcept;
        [SerializeField] private AISliderData currentFocusOne;
        [SerializeField] private AISliderData currentFocusTwo;
        [SerializeField] private AISliderData currentFocusThree;

        private float currentFocusOneScore;
        private float currentFocusTwoScore;
        private float currentFocusThreeScore;


        /// <summary>
        /// Initialize the focus scores
        /// </summary>
        public override void ExtendInitialization()
        {
            currentFocusOneScore = 0;
            currentFocusTwoScore = 0;
            currentFocusThreeScore = 0;
        }

        /// <summary>
        /// Handle a specific problem using randomness
        /// </summary>
        public override void HandleProblem(Problem problemToLearn)
        {
            switch (problemToLearn)
            {
                case Problem.Concept:
                    // Create a new ConceptData
                    currentConcept = new AIConceptData();

                    // Get a random Topic and Genre
                    currentConcept.Topic = knownTopics.ElementAt(UnityEngine.Random.Range(0, knownTopics.Count));
                    currentConcept.Genre = knownGenres.ElementAt(UnityEngine.Random.Range(0, knownGenres.Count));

                    // Store the types of audiences into an array
                    Array enumArray = Enum.GetValues(typeof(AudienceType));

                    // Get a random Audience type
                    currentConcept.Audience = (AudienceType)enumArray.GetValue(UnityEngine.Random.Range(0, enumArray.Length));
                    break;

                case Problem.FocusOne:
                    // Create a new SliderData
                    currentFocusOne = new AISliderData();
                    
                    // Check if there is a Genre within the current concept
                    if (currentConcept.Genre != null)
                        // Set its name
                        currentFocusOne.Genre = currentConcept.Genre.Name;

                    // Set slider data using random values
                    currentFocusOne.SliderOne = (PointCategory.CharacterSheets, UnityEngine.Random.Range(1, 11));
                    currentFocusOne.SliderTwo = (PointCategory.WorldDocument, UnityEngine.Random.Range(1, 11));
                    currentFocusOne.SliderThree = (PointCategory.PlotOutline, UnityEngine.Random.Range(1, 11));

                    // Get the current score for the slider combination
                    currentFocusOneScore = AttemptSliderCombination(
                        currentConcept.Genre,
                        currentFocusOne.SliderOne,
                        currentFocusOne.SliderTwo,
                        currentFocusOne.SliderThree
                    );
                    break;

                case Problem.FocusTwo:
                    // Create a new SliderData
                    currentFocusTwo = new AISliderData();

                    // Check if there is a Genre within the current concept
                    if (currentConcept.Genre != null)
                        // Set its name
                        currentFocusTwo.Genre = currentConcept.Genre.Name;

                    // Set slider data using random values
                    currentFocusTwo.SliderOne = (PointCategory.Dialogue, UnityEngine.Random.Range(1, 11));
                    currentFocusTwo.SliderTwo = (PointCategory.Subplots, UnityEngine.Random.Range(1, 11));
                    currentFocusTwo.SliderThree = (PointCategory.Descriptions, UnityEngine.Random.Range(1, 11));

                    // Get the current score for the slider combination
                    currentFocusTwoScore = AttemptSliderCombination(
                        currentConcept.Genre,
                        currentFocusTwo.SliderOne,
                        currentFocusTwo.SliderTwo,
                        currentFocusTwo.SliderThree
                    );
                    break;

                case Problem.FocusThree:
                    // Create a new SliderData
                    currentFocusThree = new AISliderData();

                    // Check if there is a Genre within the current concept
                    if (currentConcept.Genre != null)
                        // Set its name
                        currentFocusThree.Genre = currentConcept.Genre.Name;

                    // Set slider data using random values
                    currentFocusThree.SliderOne = (PointCategory.Emotions, UnityEngine.Random.Range(1, 11));
                    currentFocusThree.SliderTwo = (PointCategory.Twists, UnityEngine.Random.Range(1, 11));
                    currentFocusThree.SliderThree = (PointCategory.Symbolism, UnityEngine.Random.Range(1, 11));

                    // Get the current score for the slider combination
                    currentFocusThreeScore = AttemptSliderCombination(
                        currentConcept.Genre,
                        currentFocusThree.SliderOne,
                        currentFocusThree.SliderTwo,
                        currentFocusThree.SliderThree
                    );
                    break;
            }
        }

        /// <summary>
        /// Rate the work
        /// </summary>
        public override RateData Rate()
        {
            // Set concept data
            Topic chosenTopic = currentConcept.Topic;
            Genre chosenGenre = currentConcept.Genre;
            AudienceType chosenAudience = currentConcept.Audience;

            float phaseScores = targetScore / 3f;
            float focusOneScore = phaseScores * currentFocusOneScore;
            float focusTwoScore = phaseScores * currentFocusTwoScore;
            float focusThreeScore = phaseScores * currentFocusThreeScore;

            // Calculate the total points
            float totalPoints = focusOneScore + focusTwoScore + focusThreeScore;

            // Multiply by 100 and roundto an integer
            float roundedPercentage = Mathf.RoundToInt((totalPoints / targetScore) * 100f);

            // Clamp the final percentage to a maximum of 100 and a minimum of 0
            float finalPercentage = Mathf.Clamp(roundedPercentage, 0, 100);

            // Create the final data
            RateData finalData = new RateData()
            {
                Topic = chosenTopic,
                Genre = chosenGenre,
                Audience = chosenAudience,
                FinalScore = finalPercentage
            };

            return finalData;
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
            return finalScore / idealFocusScore;
        }
    }
}
