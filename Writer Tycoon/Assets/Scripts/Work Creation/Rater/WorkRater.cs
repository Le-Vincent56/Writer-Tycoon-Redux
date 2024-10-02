using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.Ideation.Compatibility;
using WriterTycoon.WorkCreation.Ideation.Genres;
using WriterTycoon.WorkCreation.Ideation.Topics;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Rater
{
    public enum ReviewScore
    {
        Terrible = 1,
        Poor = 2,
        Neutral = 3,
        Good = 4,
        Excellent = 5
    }

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
        private ReviewTextDatabase reviewTextDatabase;

        private EventBinding<PublishWork> publishWorkEvent;
        private EventBinding<PassDay> passDayEvent;

        public override string Name { get => "Work Rater"; }
        public override DedicantType Type { get => DedicantType.Rater; }

        private void Awake()
        {
            // Initialize the Queue
            worksToRate = new();

            // Initialize the review text database
            reviewTextDatabase = new ReviewTextDatabase();
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
            // Get the rating and compatibility info from the Work
            RatingInfo ratingInfo = workToRate.GetRatingInfo();
            CompatibilityInfo compatibilityInfo = workToRate.GetCompatibilityInfo();

            // Calculate a percentage between 0-1
            float percentage = Mathf.Clamp01(ratingInfo.EndScore / ratingInfo.TargetScore);

            // Multiply by 100 to get a random from 0-100 and round to an integer
            float roundedPercentage = Mathf.RoundToInt(percentage * 100f);

            // Add the total compatibility score to the Work
            float finalPercentage = roundedPercentage + compatibilityInfo.TotalScore;

            // Round the score to be within an integer range of 0-5
            int intScore = Mathf.RoundToInt(finalPercentage / 20f);

            // Clamp the lower bound to 1
            intScore = Mathf.Clamp(intScore, 1, 5);

            // Cast the int score to a ReviewSCore
            ReviewScore reviewScore = (ReviewScore)intScore;

            // Generate four reviews
            string[] reviewTexts = reviewTextDatabase.GetRandomReviews(reviewScore, 4);

            // Iterate through the array
            for(int i = 0; i < reviewTexts.Length; i++)
            {
                // Send out the review text at the index
                EventBus<SetReviewText>.Raise(new SetReviewText()
                {
                    ID = i,
                    Text = reviewTexts[i],
                });
            }

            // Show the Review Window
            EventBus<ShowReviewWindow>.Raise(new ShowReviewWindow()
            {
                Score = Mathf.RoundToInt(finalPercentage),
                AboutInfo = workToRate.GetAboutInfo()
            });

            // Update masteries
            UpdateMasteries(workToRate);
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

        /// <summary>
        /// Update masteries after Work completion
        /// </summary>
        private void UpdateMasteries(Work workToUpdate)
        {
            // Get the relevant Topics and Genres
            List<Topic> topicsToUpdate = workToUpdate.GetTopics();
            List<Genre> genresToUpdate = workToUpdate.GetGenres();

            Send(new RaterPayload()
                { Content = (topicsToUpdate, genresToUpdate) },
                AreTypes(new DedicantType[2]
                {
                    DedicantType.Topic,
                    DedicantType.Genre
                })
            );
        }
    }
}