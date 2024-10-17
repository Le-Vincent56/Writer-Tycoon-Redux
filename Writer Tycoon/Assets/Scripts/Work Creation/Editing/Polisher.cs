using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WriterTycoon.Entities;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.UI.Development;

namespace WriterTycoon.WorkCreation.Editing
{
    [System.Serializable]
    public class Polisher
    {
        private Work workParent;
        [SerializeField] private bool polishing;

        [Header("Errors")]
        [SerializeField] private bool finishedRemovingErrors;
        [SerializeField] private float totalErrors;
        [SerializeField] private float currentErrors;
        [SerializeField] private float dailyErrorGoal;
        [SerializeField] private float errorProgress;
        [SerializeField] private float errorRate;

        [Header("Score")]
        [SerializeField] private float developedPoints;
        [SerializeField] private float currentScore;
        [SerializeField] private float currentPolishBurst;
        [SerializeField] private float maxPolishPointsPossible;
        [SerializeField] private float polishBurstMax;
        [SerializeField] private float basePolishRate;
        [SerializeField] private float polishRate;

        public float EndScore { get => currentScore; set => currentScore = value; }

        public Polisher(Work workParent, float targetScore)
        {
            this.workParent = workParent;

            polishing = false;

            // Set error variables
            finishedRemovingErrors = false;
            totalErrors = 0;
            currentErrors = 0;
            dailyErrorGoal = 0;
            errorRate = 0;

            // Set polish variables
            developedPoints = 0;
            maxPolishPointsPossible = targetScore * 0.1f; // Only allow the player to get +10% maximum from polish points
            currentPolishBurst = 0;
            polishBurstMax = 0;

            // Set the polish rate
            basePolishRate = 0.4f;

            // Iterate through each Worker after the first
            for(int i = 1; i < workParent.Workers.Count; i++)
            {
                // For each worker, increment the base polish rate
                basePolishRate += 0.1f;
            }
        }

        /// <summary>
        /// Set the daily error goals for the Work
        /// </summary>
        public void SetDailyErrorGoals()
        {
            // Exit case - if the Work is not being worked on
            if (!workParent.IsWorkedOn()) return;

            // Exit case - if not polishing
            if (!polishing) return;

            // Check if there are errors to remove
            if (currentErrors > 0)
            {
                // Generate anywhere between 1 - 5% of errors
                dailyErrorGoal = Random.Range(totalErrors * 0.01f, totalErrors * 0.05f);

                // Get the error rate for hourly fixes
                errorRate = dailyErrorGoal / 24f;

                return;
            }
        }

        /// <summary>
        /// Polish a Work
        /// </summary>
        public void Polish()
        {
            // Exit case - if the Work is not being worked on
            if (!workParent.IsWorkedOn()) return;

            // Exit case - if not polishing
            if (!polishing) return;

            // Check if there are errors to remove
            if (currentErrors > 0 && dailyErrorGoal > 0 && !finishedRemovingErrors)
            {
                // Subtract from the total errors
                currentErrors -= errorRate;

                // Add to the daily error goal
                errorProgress += errorRate;

                // Check if the error progress has reached 1
                if (errorProgress >= 1)
                {
                    // If so, reset back to 0
                    errorProgress = 0;

                    // Update to show how many errors are left
                    EventBus<UpdateProgressText>.Raise(new UpdateProgressText()
                    {
                        Hash = workParent.Hash,
                        Stage = ProgressStage.Error,
                        Text = $"Errors: {Mathf.CeilToInt(currentErrors)}"
                    });
                }

                // Update the error progress bar
                EventBus<UpdateProgressData>.Raise(new UpdateProgressData()
                {
                    Hash = workParent.Hash,
                    Stage = ProgressStage.Error,
                    Current = errorProgress,
                    Maximum = 1
                });

                return;
            }
            // Check if all of the current errors have been removed and 
            // if we have not yet finished removing errors
            else if (currentErrors <= 0 && !finishedRemovingErrors)
            {
                // Set finished removing errors to true
                finishedRemovingErrors = true;

                // Set the current score for polishing
                currentScore = workParent.PointGenerator.CurrentScore;

                // Update the progress card to the new state
                EventBus<SetProgressStage>.Raise(new SetProgressStage()
                {
                    Hash = workParent.Hash,
                    Stage = ProgressStage.Polish
                });

                // Update the polish progress text
                EventBus<ShowProgressText>.Raise(new ShowProgressText()
                {
                    Hash = workParent.Hash,
                    Stage = ProgressStage.Polish,
                    Text = $"Polishing"
                });

                // Set a maximum for the polish burst
                polishBurstMax = Random.Range(1, 3);
            }

            // Exit case - if not finished removing errors
            if (!finishedRemovingErrors) return;

            // Exit case - if developed the max amount of points
            if (developedPoints >= maxPolishPointsPossible) return;

            // Increase the current polish burst
            currentPolishBurst += polishRate;

            // Update the polish progres bar
            EventBus<UpdateProgressData>.Raise(new UpdateProgressData()
            {
                Hash = workParent.Hash,
                Stage = ProgressStage.Polish,
                Current = currentPolishBurst,
                Maximum = polishBurstMax
            });

            // Check if the current burst has exceeded the maximum
            if (currentPolishBurst >= polishBurstMax)
            {
                // Reset the current polish burst
                currentPolishBurst = 0;

                // Calculate the earned points
                float earnedPoints = developedPoints + polishBurstMax;

                // Check if the developed points would go over the max polish points possible
                if(earnedPoints > maxPolishPointsPossible)
                {
                    // Subtract the difference from the polish burst max
                    float difference = earnedPoints - maxPolishPointsPossible;
                    polishBurstMax -= difference;
                }

                // Add the maximum to the developed points and to the current score
                developedPoints += polishBurstMax;
                currentScore += polishBurstMax;

                // Regenerate the new maximum
                polishBurstMax = Random.Range(1, 3);
            }
        }

        /// <summary>
        /// Begin polishing for a Work
        /// </summary>
        public void BeginPolish()
        {
            // Set variables
            finishedRemovingErrors = false;
            polishing = true;

            // Update the progress card to the new state
            EventBus<SetProgressStage>.Raise(new SetProgressStage()
            {
                Hash = workParent.Hash,
                Stage = ProgressStage.Error
            });

            // Show the errors
            EventBus<ShowProgressText>.Raise(new ShowProgressText()
            {
                Hash = workParent.Hash,
                Stage = ProgressStage.Error,
                Text = $"Errors: {Mathf.CeilToInt(currentErrors)}"
            });
        }

        /// <summary>
        /// Update the polish rate
        /// </summary>
        public void UpdatePolishRate()
        {
            float workerStatusTotal = 0f;

            // Iterate through each Worker
            foreach (IWorker worker in workParent.Workers)
            {
                // Add their status to the total
                workerStatusTotal += worker.GetStatus();
            }

            // Divide the total by the amount of Workers for the average
            workerStatusTotal /= workParent.Workers.Count;

            // Set the polish rate to the base polish rate multiplied by the worker status
            // then divide by 24 as it is used each hour
            polishRate = (basePolishRate * workerStatusTotal) / 24f;
        }

        /// <summary>
        /// Set the total amount of errors for the Polisher to polish
        /// </summary>
        public void SetTotalErrors(int totalErrors)
        {
            this.totalErrors = totalErrors;
            currentErrors = totalErrors;
        }

        /// <summary>
        /// Set the current score for the Polisher
        /// </summary>
        public void SetCurrentScore(float currentScore) => this.currentScore = currentScore;
    }
}