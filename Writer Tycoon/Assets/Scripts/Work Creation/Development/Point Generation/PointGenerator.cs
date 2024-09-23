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
        [SerializeField] private float targetScore;
        [SerializeField] private float componentScore;
        [SerializeField] private float[] targetSplitScores;
        [SerializeField] private float currentScore;
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
        }

        private void OnEnable()
        {
            passDayEvent = new EventBinding<PassDay>(GeneratePoints);
            EventBus<PassDay>.Register(passDayEvent);

            setDevelopmentPhaseEvent = new EventBinding<SetDevelopmentPhase>(SetDevelopmentPhase);
            EventBus<SetDevelopmentPhase>.Register(setDevelopmentPhaseEvent);

            sendPhaseTimeEvent = new EventBinding<SendPhaseTime>(SetPhaseTotal);
            EventBus<SendPhaseTime>.Register(sendPhaseTimeEvent);

            endDevelopmentEvent = new EventBinding<EndDevelopment>(ResetGenerator);
            EventBus<EndDevelopment>.Register(endDevelopmentEvent);
        }

        private void OnDisable()
        {
            EventBus<PassDay>.Deregister(passDayEvent);
            EventBus<SetDevelopmentPhase>.Deregister(setDevelopmentPhaseEvent);
            EventBus<SendPhaseTime>.Deregister(sendPhaseTimeEvent);
            EventBus<EndDevelopment>.Deregister(endDevelopmentEvent);
        }

        /// <summary>
        /// Callback function to generate points every day
        /// </summary>
        private void GeneratePoints()
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
        /// Calculate the percentage of how close the player was to the score
        /// </summary>
        private float[] CalculateCategoryScores(
            PointCategory splitOneCategory, PointCategory splitTwoCategory, PointCategory splitThreeCategory
        )
        {
            // Create a new array representing each split
            float[] targetScores = new float[3];

            // Set each split score
            targetScores[0] = CalculateCategoryScore(splitOneCategory);
            targetScores[1] = CalculateCategoryScore(splitTwoCategory);
            targetScores[2] = CalculateCategoryScore(splitThreeCategory);

            return targetScores;
        }

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

                float multiplier = 1f - percentageLoss;

                // Add the multiplier to the total
                totalMultiplier += multiplier;

                //Debug.Log($"Multiplier for {pointCategory}: {multiplier}" +
                //    $"\nPlayer Value: {playerValue}" +
                //    $"\nTarget Value: {targetValue}" +
                //    $"\nDifference: {difference}" +
                //    $"\nStep Value: {stepValue}" +
                //    $"\nPercentage Loss: {percentageLoss}");
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

                    // Set the current split target scores
                    targetSplitScores = CalculateCategoryScores(PointCategory.CharacterSheets, PointCategory.PlotOutline, PointCategory.WorldDocument);
                    break;

                case DevelopmentPhase.PhaseTwo:
                    // Set the split times
                    CalculateSplitTimes(PointCategory.Dialogue, PointCategory.Subplots, PointCategory.Descriptions);

                    // Set the current split target scores
                    targetSplitScores = CalculateCategoryScores(PointCategory.Dialogue, PointCategory.Subplots, PointCategory.Descriptions);
                    break;

                case DevelopmentPhase.PhaseThree:
                    // Set the split times
                    CalculateSplitTimes(PointCategory.Emotions, PointCategory.Twists, PointCategory.Symbolism);

                    // Set the current split target scores
                    targetSplitScores = CalculateCategoryScores(PointCategory.Emotions, PointCategory.Twists, PointCategory.Symbolism);
                    break;
            }
        }

        /// <summary>
        /// Reset the Point Generator
        /// </summary>
        private void ResetGenerator()
        {
            // Clear allocated points
            allocatedPoints = new();
            currentPhase = DevelopmentPhase.PhaseOne;
            
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
        public void SetTargetScore(float targetScore)
        {
            this.targetScore = targetScore;
            componentScore = targetScore / 9f;
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