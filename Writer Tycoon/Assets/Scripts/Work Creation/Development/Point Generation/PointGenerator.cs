using System;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.Ideation.Genres;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Development.PointGeneration
{
    public class PointGenerator : Dedicant
    {
        [SerializeField] private List<Genre> chosenGenres;
        [SerializeField] private DevelopmentPhase currentPhase;

        [SerializeField] private bool generatePoints;
        [SerializeField] private float componentScore;
        [SerializeField] private float[] targetSplitScores;
        [SerializeField] private float targetScore;
        [SerializeField] private float currentScore;
        [SerializeField] private float[] generationRates;
        private GenreFocusTargets genreFocusTargets;
        private Dictionary<PointCategory, int> allocatedPoints;

        [SerializeField] private int currentDay;
        [SerializeField] private int totalPhaseTime;
        [SerializeField] private int split;
        [SerializeField] private int splitOneTime;
        [SerializeField] private int splitTwoTime;
        [SerializeField] private int splitThreeTime;

        public override string Name => "Point Generator";
        public override DedicantType Type => DedicantType.PointGenerator;

        private EventBinding<PassHour> passHourEvent;
        private EventBinding<PassDay> passDayEvent;
        private EventBinding<SetDevelopmentPhase> setDevelopmentPhaseEvent;
        private EventBinding<SendPhaseTime> sendPhaseTimeEvent;
        private EventBinding<EndDevelopment> endDevelopmentEvent;

        private void Awake()
        {
            // Initialize variables
            generatePoints = false;
            genreFocusTargets = new();
            allocatedPoints = new();
            currentDay = 0;
            split = 1;
            targetSplitScores = null;
            generationRates = null;
        }

        private void OnEnable()
        {
            passHourEvent = new EventBinding<PassHour>(GeneratePoints);
            EventBus<PassHour>.Register(passHourEvent);

            passDayEvent = new EventBinding<PassDay>(ManageSplits);
            EventBus<PassDay>.Register(passDayEvent);

            setDevelopmentPhaseEvent = new EventBinding<SetDevelopmentPhase>(SetDevelopmentPhase);
            EventBus<SetDevelopmentPhase>.Register(setDevelopmentPhaseEvent);

            sendPhaseTimeEvent = new EventBinding<SendPhaseTime>(SetPhaseTotal);
            EventBus<SendPhaseTime>.Register(sendPhaseTimeEvent);

            endDevelopmentEvent = new EventBinding<EndDevelopment>(SendAndResetGenerator);
            EventBus<EndDevelopment>.Register(endDevelopmentEvent);
        }

        private void OnDisable()
        {
            EventBus<PassHour>.Deregister(passHourEvent);
            EventBus<PassDay>.Deregister(passDayEvent);
            EventBus<SetDevelopmentPhase>.Deregister(setDevelopmentPhaseEvent);
            EventBus<SendPhaseTime>.Deregister(sendPhaseTimeEvent);
            EventBus<EndDevelopment>.Deregister(endDevelopmentEvent);
        }

        /// <summary>
        /// Generate points per hour
        /// </summary>
        private void GeneratePoints()
        {
            // Exit case - if the arrays are not initialized
            if (targetSplitScores == null || generationRates == null) return;

            // Exit case - if the current score has generated the amount of points for this split
            if (currentScore >= targetScore) return;

            // Increment the current score by this split's generation rate
            currentScore += generationRates[split - 1];
        }

        /// <summary>
        /// Callback function to generate points every day
        /// </summary>
        private void ManageSplits()
        {
            // Exit case - if not supposed to generate points
            if (!generatePoints) return;

            // Increment the current day
            currentDay++;

            // Update UI
            switch (split)
            {
                case 1:
                    // Handle the split time
                    HandleSplit(splitOneTime);
                    break;

                case 2:
                    // Handle the split time
                    HandleSplit(splitTwoTime);
                    break;

                case 3:
                    // Handle the split time
                    HandleSplit(splitThreeTime, false);
                    break;
            }
        }

        /// <summary>
        /// Handle the passing of days within a split
        /// </summary>
        private void HandleSplit(int splitTime, bool incrementSplit = true)
        {
            // Exit case - if finished the split time
            if(currentDay == splitTime)
            {
                // Reset the current day
                currentDay = 0;

                // Check whether or not to increment the split
                if (incrementSplit)
                {
                    // Increment the split (if on split 1 or 2)
                    split++;

                    // Update the progress text
                    ChangeProgressText();

                    // Update the target score
                    targetScore += targetSplitScores[split - 1];
                }
                else
                {
                    // Stop generating points (if on split 3)
                    generatePoints = false;
                    
                    // Hide the progress text
                    EventBus<HideProgressText>.Raise(new HideProgressText());
                }

                return;
            }
        }
        
        /// <summary>
        /// Change the progress text to display flavor text according to the current split
        /// </summary>
        private void ChangeProgressText()
        {
            // Check which phase of development
            switch (currentPhase)
            {
                case DevelopmentPhase.PhaseOne:
                    // Change the text based on the split
                    switch(split)
                    {
                        case 1:
                            EventBus<ShowProgressText>.Raise(new ShowProgressText()
                            {
                                Text = "Writing Characters"
                            });
                            break;

                        case 2:
                            EventBus<ShowProgressText>.Raise(new ShowProgressText()
                            {
                                Text = "Outlining Plot"
                            });
                            break;

                        case 3:
                            EventBus<ShowProgressText>.Raise(new ShowProgressText()
                            {
                                Text = "Building World"
                            });
                            break;
                    }
                    break;

                case DevelopmentPhase.PhaseTwo:
                    // Change the text based on the split
                    switch (split)
                    {
                        case 1:
                            EventBus<ShowProgressText>.Raise(new ShowProgressText()
                            {
                                Text = "Creating Dialogue"
                            });
                            break;

                        case 2:
                            EventBus<ShowProgressText>.Raise(new ShowProgressText()
                            {
                                Text = "Stringing Subplots"
                            });
                            break;

                        case 3:
                            EventBus<ShowProgressText>.Raise(new ShowProgressText()
                            {
                                Text = "Enhancing Descriptions"
                            });
                            break;
                    }
                    break;

                case DevelopmentPhase.PhaseThree:
                    // Change the text based on the split
                    switch (split)
                    {
                        case 1:
                            EventBus<ShowProgressText>.Raise(new ShowProgressText()
                            {
                                Text = "Invoking Emotions"
                            });
                            break;

                        case 2:
                            EventBus<ShowProgressText>.Raise(new ShowProgressText()
                            {
                                Text = "Unraveling Twists"
                            });
                            break;

                        case 3:
                            EventBus<ShowProgressText>.Raise(new ShowProgressText()
                            {
                                Text = "Developing Symbolism"
                            });
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Calculate the split times for each Point Category within a phase
        /// </summary>
        private void CalculateSplitTimes(
            PointCategory splitOneCategory, PointCategory splitTwoCategory, PointCategory splitThreeCategory
        )
        {
            // Calculate the percentage for each category
            float splitOnePercentage = (float)allocatedPoints[splitOneCategory] / 15;
            float splitTwoPercentage = (float)allocatedPoints[splitTwoCategory] / 15;
            float splitThreePercentage = (float)allocatedPoints[splitThreeCategory] / 15;

            // Calculate the raw, floating-point times
            float rawSplitOneTime = splitOnePercentage * totalPhaseTime;
            float rawSplitTwoTime = splitTwoPercentage * totalPhaseTime;
            float rawSplitThreeTime = splitThreePercentage * totalPhaseTime;

            // Round each split time
            int splitOneTime = Mathf.RoundToInt(rawSplitOneTime);
            int splitTwoTime = Mathf.RoundToInt(rawSplitTwoTime);
            int splitThreeTime = Mathf.RoundToInt(rawSplitThreeTime);

            // Calculate the difference between the total rounded split times and the total phase time
            int roundedTotal = splitOneTime + splitTwoTime + splitThreeTime;
            int difference = totalPhaseTime - roundedTotal;

            // Check if there's a difference to distribute
            if(difference != 0)
            {
                // Check if the difference is positive or negative
                if (difference > 0)
                {
                    // If positive, add extra time to one of the splits
                    if (splitOneTime < rawSplitOneTime) splitOneTime += 1;
                    else if (splitTwoTime < rawSplitTwoTime) splitTwoTime += 1;
                    else splitThreeTime += 1;
                }
                else
                {
                    // If negative, remove extra time from one of the splits
                    if (splitOneTime > rawSplitOneTime) splitOneTime -= 1;
                    else if (splitTwoTime > rawSplitTwoTime) splitTwoTime -= 1;
                    else splitThreeTime -= 1;
                }
            }

            // Set split times
            this.splitOneTime = splitOneTime;
            this.splitTwoTime = splitTwoTime;
            this.splitThreeTime = splitThreeTime;

            // Update the progress text
            ChangeProgressText();
        }

        /// <summary>
        /// Calculate the target scores and generation rates for each Point Category within a phase
        /// </summary>
        private void CalculateCategoryScoresAndRates(
            PointCategory splitOneCategory, PointCategory splitTwoCategory, PointCategory splitThreeCategory
        )
        {
            // Create a new array representing each split
            targetSplitScores = new float[3];

            // Set each split score
            targetSplitScores[0] = CalculateCategoryScore(splitOneCategory);
            targetSplitScores[1] = CalculateCategoryScore(splitTwoCategory);
            targetSplitScores[2] = CalculateCategoryScore(splitThreeCategory);

            // Calculate generation rates
            generationRates = new float[3];

            generationRates[0] = CalculateGenerationRate(splitOneTime, targetSplitScores[0]);
            generationRates[1] = CalculateGenerationRate(splitTwoTime, targetSplitScores[1]);
            generationRates[2] = CalculateGenerationRate(splitThreeTime, targetSplitScores[2]);
        }

        /// <summary>
        /// Calculate the hourly generation rate for the split and the target score
        /// </summary>
        private float CalculateGenerationRate(float splitTime, float targetScore) => targetScore / (splitTime * 24);

        /// <summary>
        /// Calculate the point threshold for a given Point Category
        /// </summary>
        private float CalculateCategoryScore(PointCategory pointCategory)
        {
            // Get the player and target values
            int playerValue = allocatedPoints[pointCategory];

            // Start counting a total multiplier
            float totalMultiplier = 0;

            // Iterate through each Genre
            foreach(Genre genre in chosenGenres)
            {
                // Get the target value for the Genre
                int targetValue = genreFocusTargets.GetTargetScore(genre.Type, pointCategory);

                // Get the absolute difference of the given values
                int difference = Math.Abs(playerValue - targetValue);

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
            }

            // Get the average of the total multiplier
            float averageMult = totalMultiplier / chosenGenres.Count;

            // Return the average multiplier times the base component score
            return componentScore * averageMult;
        }

        /// <summary>
        /// Callback function to set the current development phase
        /// </summary>
        private void SetDevelopmentPhase(SetDevelopmentPhase eventData)
        {
            currentPhase = eventData.Phase;
        }

        /// <summary>
        /// Callback function to set the current phase's total time
        /// </summary>
        private void SetPhaseTotal(SendPhaseTime eventData)
        {
            totalPhaseTime = eventData.TimeEstimate;
        }

        /// <summary>
        /// Set the split times for each phase
        /// </summary>
        private void SetPhaseSplitTime()
        {
            // Set the split to 1
            split = 1;

            // Set the current day to 0
            currentDay = 0;

            switch (currentPhase)
            {
                case DevelopmentPhase.PhaseOne:
                    // Set the split times
                    CalculateSplitTimes(PointCategory.CharacterSheets, PointCategory.PlotOutline, PointCategory.WorldDocument);

                    // Set the current split target scores and generation rates
                    CalculateCategoryScoresAndRates(PointCategory.CharacterSheets, PointCategory.PlotOutline, PointCategory.WorldDocument);
                    break;

                case DevelopmentPhase.PhaseTwo:
                    // Set the split times
                    CalculateSplitTimes(PointCategory.Dialogue, PointCategory.Subplots, PointCategory.Descriptions);

                    // Set the current split target scores and generation rates
                    CalculateCategoryScoresAndRates(PointCategory.Dialogue, PointCategory.Subplots, PointCategory.Descriptions);
                    break;

                case DevelopmentPhase.PhaseThree:
                    // Set the split times
                    CalculateSplitTimes(PointCategory.Emotions, PointCategory.Twists, PointCategory.Symbolism);

                    // Set the current split target scores and generation rates
                    CalculateCategoryScoresAndRates(PointCategory.Emotions, PointCategory.Twists, PointCategory.Symbolism);
                    break;
            }

            // Set the current target score for the first split
            targetScore += targetSplitScores[split - 1];
        }

        /// <summary>
        /// Reset the Point Generator
        /// </summary>
        private void SendAndResetGenerator()
        {
            // Send the points out for editing
            Send(new PointPayload()
                { Content = currentScore},
                IsType(DedicantType.Editor)
            );

            // Clear allocated points
            allocatedPoints = new();
            currentPhase = DevelopmentPhase.PhaseOne;

            // Nullify arrays
            targetSplitScores = null;
            generationRates = null;

            // Hide the progress text
            EventBus<HideProgressText>.Raise(new HideProgressText());
        }

        /// <summary>
        /// Set the allocated points Dictionary
        /// </summary>
        public void SetAllocatedPoints(Dictionary<PointCategory, int> allocatedPoints)
        {
            generatePoints = true;
            this.allocatedPoints = allocatedPoints;

            // Set the split times for the phases
            SetPhaseSplitTime();
        }

        /// <summary>
        /// Set the target score for the Point Generator
        /// </summary>
        public void SetTargetScore(float totalTargetScore)
        {
            componentScore = totalTargetScore / 9f;
            targetScore = 0;
            currentScore = 0;
        }

        /// <summary>
        /// Set the chosen genre
        /// </summary>
        public void SetGenres(List<Genre> genres)
        {
            chosenGenres = genres;
        }
    }
}