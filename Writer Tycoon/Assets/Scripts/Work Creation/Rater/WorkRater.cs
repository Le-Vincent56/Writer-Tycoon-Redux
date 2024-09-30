using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.Ideation.Compatibility;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Rater
{
    public struct RatingInfo
    {
        public string Title;
        public string Author;
        public float TargetScore;
        public float EndScore;
    }

    public class WorkRater : Dedicant
    {
        [SerializeField] private int daysUntilRate;
        [SerializeField] private Queue<Work> worksToRate;

        private EventBinding<PublishWork> publishWorkEvent;
        private EventBinding<PassDay> passDayEvent;

        public override string Name { get => "Work Rater"; }
        public override DedicantType Type { get => DedicantType.Rater; }

        private void Awake()
        {
            // Initialize the Queue
            worksToRate = new();
        }

        private void OnEnable()
        {
            publishWorkEvent = new EventBinding<PublishWork>(QueueWorkToRate);
            EventBus<PublishWork>.Register(publishWorkEvent);

            passDayEvent = new EventBinding<PassDay>(PassDaysUntilRating);
            EventBus<PassDay>.Register(passDayEvent);
        }

        private void OnDisable()
        {
            EventBus<PublishWork>.Deregister(publishWorkEvent);
            EventBus<PassDay>.Deregister(passDayEvent);
        }

        /// <summary>
        /// Rate a Work
        /// </summary>
        private void Rate(Work workToRate)
        {
            RatingInfo ratingInfo = workToRate.GetRatingInfo();
            CompatibilityInfo compatibilityInfo = workToRate.GetCompatibilityInfo();

            float percentage = Mathf.Clamp01(ratingInfo.EndScore / ratingInfo.TargetScore);
            float roundedPercentage = Mathf.FloorToInt(percentage);

            Debug.Log($"Slider Percentage: {roundedPercentage}");
            Debug.Log($"Compatibility Score: {compatibilityInfo.TotalScore}");

            // Add the total compatibility score to the Work
            roundedPercentage += compatibilityInfo.TotalScore;
        }

        /// <summary>
        /// Callback function to pass the days before Rating a work
        /// </summary>
        private void PassDaysUntilRating()
        {
            // Exit case - if the Queue is empty
            if (worksToRate.Count <= 0) return;

            // Get the queued Work and the amount of days to wait
            Work work = worksToRate.Peek();

            // Check if there are days to wait until rating
            if(work.DaysToWaitBeforeRating > 0)
            {
                // If so, decrement the days and exit
                work.DaysToWaitBeforeRating--;
                return;
            }

            // Dequeue the Work and rate it
            Rate(worksToRate.Dequeue());
        }

        /// <summary>
        /// Callback function to queue a published Work to rate
        /// </summary>
        private void QueueWorkToRate(PublishWork eventData)
        {
            // Enqueue the Work to be rated, and set the days to wait to 7
            worksToRate.Enqueue(eventData.WorkToPublish);
        }
    }
}