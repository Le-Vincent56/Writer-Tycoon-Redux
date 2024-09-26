using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WriterTycoon.WorkCreation.Development.Tracker;

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
        [SerializeField] private float dailyErrorQuota;
        [SerializeField] private float previousWholeNumber;
        [SerializeField] private float errorRate;

        [Header("Score")]
        [SerializeField] private float developedPoints;
        [SerializeField] private float currentPolishBurst;
        [SerializeField] private float polishBurstMax;
        [SerializeField] private float polishRate;

        public Polisher(Work workParent)
        {
            this.workParent = workParent;

            polishing = false;

            // Set error variables
            finishedRemovingErrors = false;
            totalErrors = 0;
            currentErrors = 0;
            dailyErrorGoal = 0;
            dailyErrorQuota = 0;
            previousWholeNumber = 0;
            errorRate = 0;

            // Set polish variables
            developedPoints = 0;
            currentPolishBurst = 0;
            polishBurstMax = 0;
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

                // Reset the current daily errors
                dailyErrorQuota = 0;

                // Get the error rate for hourly fixes
                errorRate = dailyErrorGoal / 24f;

                return;
            }
        }
    }
}