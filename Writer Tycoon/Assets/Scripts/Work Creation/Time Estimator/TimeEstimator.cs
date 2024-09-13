using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WriterTycoon.WorkCreation.Mediation;
using WriterTycoon.WorkCreation.Topics;
using WriterTycoon.WorkCreation.WorkTypes;

namespace WriterTycoon.WorkCreation.TimeEstimation
{
    public class TimeEstimator : Dedicant
    {
        private int dayEstimate;
        private List<Topic> topics;
        [SerializeField] private WorkType workType;

        public UnityAction<int> Updated = delegate { };

        public override string Name { get => "Time Estimator"; }
        public override DedicantType Type { get => DedicantType.TimeEstimator; }

        private void Awake()
        {
            topics = new();
        }

        /// <summary>
        /// Update the Time Estimate
        /// </summary>
        private void UpdateEstimate()
        {
            // Reset the total time estimate
            dayEstimate = 0;

            // Add estimates
            dayEstimate += GetWorkTypeEstimates();
            dayEstimate += GetTopicEstimates();

            // Invoke the Time Estimate Updated event
            Updated.Invoke(dayEstimate);
        }

        /// <summary>
        /// Set the Time Estimator's current Work Type
        /// </summary>
        public void SetWorkType(WorkType workType)
        {
            this.workType = workType;

            // Update the estmiate
            UpdateEstimate();
        }

        /// <summary>
        /// Set the Time Estimator's current Topics
        /// </summary>
        public void SetTopics(List<Topic> topics)
        {
            this.topics = topics;

            // Update the estimate
            UpdateEstimate();
        }

        /// <summary>
        /// Get the time estimates for the current Work Type
        /// </summary>
        private int GetWorkTypeEstimates()
        {
            return workType switch
            {
                WorkType.None => 0,
                WorkType.Poetry => 10,
                WorkType.FlashFiction => 15,
                WorkType.ShortStory => 30,
                WorkType.Novella => 50,
                WorkType.Novel => 100,
                WorkType.Screenplay => 100,
                _ => 0
            };
        }

        /// <summary>
        /// Get the total estimation time for the selected Topics
        /// </summary>
        /// <returns></returns>
        private int GetTopicEstimates()
        {
            // Exit case - if the Topics list is null or empty
            if (topics == null || topics.Count == 0) return 0;

            // Establish a total time estimate
            int totalTime = 0;

            // Iterate through each selected Topic
            foreach(Topic topic in topics)
            {
                // Increment the total time by the Topic's Mastery Level
                totalTime += EstimateTopicByMastery(topic);
            }

            return totalTime;
        }

        /// <summary>
        /// Decide how many days to add to the time estimate based on a Topic's mastery level
        /// </summary>
        private int EstimateTopicByMastery(Topic topic)
        {
            return topic.MasteryLevel switch
            {
                0 => 10,
                1 => 5,
                2 => 2,
                3 => 0,
                _ => 10
            };
        }
    }
}
