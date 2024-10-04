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

        private EventBinding<RateWork> publishWorkEvent;
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
            publishWorkEvent = new EventBinding<RateWork>(QueueWorkToRate);
            EventBus<RateWork>.Register(publishWorkEvent);

            passDayEvent = new EventBinding<PassDay>(PassDaysUntilRating);
            EventBus<PassDay>.Register(passDayEvent);
        }

        private void OnDisable()
        {
            EventBus<RateWork>.Deregister(publishWorkEvent);
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

            // Add the mastery percentage to the Work
            finalPercentage += GetMasteryPercentage(workToRate);

            // Clamp the final percentage to a maximum of 100 and a minimum of 0
            finalPercentage = Mathf.Clamp(finalPercentage, 0, 100);

            // Display the rating
            DisplayUI(workToRate, finalPercentage);

            // Update masteries
            UpdateMasteries(workToRate);

            // Publish the work
            EventBus<PublishWork>.Raise(new PublishWork()
            {
                FinalScore = finalPercentage,
                WorkToPublish = workToRate
            });
        }

        /// <summary>
        /// Get the Mastery percentage of a Work
        /// </summary>
        private float GetMasteryPercentage(Work workToRate)
        {
            // Get the Topics and Genres list
            List<Topic> topics = workToRate.GetTopics();
            List<Genre> genres = workToRate.GetGenres();

            // Get the highest mastery points from the Topics list
            float averageTopicMastery = 0f;

            // Iterate through each Topic
            foreach (Topic topic in topics)
            {
                // Add the mastery percentage to the total
                averageTopicMastery += topic.GetMasteryPercentage();
            }

            // Divide by the count to get the average
            averageTopicMastery /= topics.Count;

            // Get the highest mastery points from the Genres list
            float averageGenreMastery = 0f;

            // Iterate through each Genre
            foreach (Genre genre in genres)
            {
                // Add the mastery percentage to the total
                averageGenreMastery += genre.GetMasteryPercentage();
            }

            // Divide by the count to get the average
            averageGenreMastery /= genres.Count;

            // Round the mastery score down after adding
            float totalMasteryScore = Mathf.FloorToInt(averageTopicMastery + averageGenreMastery);

            return totalMasteryScore;
        }

        /// <summary>
        /// Display the rating within a UI window
        /// </summary>
        private void DisplayUI(Work workToRate, float finalPercentage)
        {
            // Round the score to be within an integer range of 0-5
            int intScore = Mathf.RoundToInt(finalPercentage / 20f);

            // Clamp the lower bound to 1
            intScore = Mathf.Clamp(intScore, 1, 5);

            // Cast the int score to a ReviewSCore
            ReviewScore reviewScore = (ReviewScore)intScore;

            // Generate four reviews
            string[] reviewTexts = reviewTextDatabase.GetRandomReviews(reviewScore, 4);

            // Iterate through the array
            for (int i = 0; i < reviewTexts.Length; i++)
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
        private void QueueWorkToRate(RateWork eventData)
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