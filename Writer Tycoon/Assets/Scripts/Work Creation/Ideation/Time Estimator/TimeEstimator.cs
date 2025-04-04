using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GhostWriter.Entities;
using GhostWriter.Patterns.EventBus;
using GhostWriter.WorkCreation.Ideation.Topics;
using GhostWriter.WorkCreation.Ideation.WorkTypes;
using GhostWriter.WorkCreation.Mediation;

namespace GhostWriter.WorkCreation.Ideation.TimeEstimation
{
    public struct TimeEstimates
    {
        public int Total;
        public int PhaseOne;
        public int PhaseTwo;
        public int PhaseThree;
    }

    public class TimeEstimator : Dedicant
    {
        private int totalDayEstimate;
        private int phaseOneDayEstimate;
        private int phaseTwoDayEstimate;
        private int phaseThreeDayEstimate;
        private List<Topic> topics;
        private List<IWorker> workers;
        [SerializeField] private WorkType workType;

        public UnityAction<int> Updated = delegate { };

        private EventBinding<ClearIdeation> clearIdeationEvent;

        public override string Name { get => "Time Estimator"; }
        public override DedicantType Type { get => DedicantType.TimeEstimator; }

        private void Awake()
        {
            topics = new();
        }

        private void OnEnable()
        {
            clearIdeationEvent = new EventBinding<ClearIdeation>(ClearEstimates);
            EventBus<ClearIdeation>.Register(clearIdeationEvent);
        }

        private void OnDisable()
        {
            EventBus<ClearIdeation>.Deregister(clearIdeationEvent);
        }

        /// <summary>
        /// Update the Time Estimate
        /// </summary>
        private void UpdateEstimate()
        {
            // Reset the total time estimate
            totalDayEstimate = 0;

            // Add estimates
            totalDayEstimate += GetWorkTypeEstimates();
            totalDayEstimate += GetTopicEstimates();

            // Get the total day estimate as a float to incorporate the scalar
            float totalDayEstimateFloat = totalDayEstimate * GetWorkerEstimates();

            // Round the scaled estimate
            totalDayEstimate = Mathf.RoundToInt(totalDayEstimateFloat);

            // Split the total estimates into three phases
            phaseOneDayEstimate = (int)(totalDayEstimate * (1f / 5f));
            phaseTwoDayEstimate = (int)(totalDayEstimate * (3 / 5f));
            phaseThreeDayEstimate = (int)(totalDayEstimate * (1f / 5f));

            // Ensure that all of the total days are used
            int combinedEstimates = phaseOneDayEstimate + phaseTwoDayEstimate + phaseThreeDayEstimate;
            int leftoverEstimate = totalDayEstimate - combinedEstimates;
            
            // Check if a positive amount of leftover days remain
            if(leftoverEstimate > 0)
                // If so, add them to the third phase estimate
                phaseThreeDayEstimate += leftoverEstimate;

            // Send the Time Estimate to the mediator
            SendTimeEstimate();

            // Invoke the Time Estimate Updated event
            Updated.Invoke(totalDayEstimate);
        }

        /// <summary>
        /// Clear the estimated times
        /// </summary>
        private void ClearEstimates()
        {
            // Reset all estimates
            totalDayEstimate = 0;
            phaseOneDayEstimate = 0;
            phaseTwoDayEstimate = 0;
            phaseThreeDayEstimate = 0;

            // Send the Time Estimate to the mediator
            SendTimeEstimate();

            // Invoke the Time Estimate Updated event
            Updated.Invoke(totalDayEstimate);
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

        public void SetWorkers(List<IWorker> workers)
        {
            this.workers = workers;

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
                WorkType.FlashFiction => 25,
                WorkType.ShortStory => 70,
                WorkType.Novella => 100,
                WorkType.Novel => 365,
                WorkType.Screenplay => 365,
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

        private float GetWorkerEstimates()
        {
            // Exit case - if the Workers list is null or empty
            if (workers == null || workers.Count == 0) return 1f;

            // Exit case - if there's only one worker, return the default scalar
            if (workers.Count == 0) return 1f;

            // Establish a total time estimate
            float timeScalar = 1f;

            // Multiply the time scalar by 0.75 for each worker besides the first
            timeScalar *= Mathf.Pow(0.75f, workers.Count - 1);

            return timeScalar;
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

        /// <summary>
        /// Send the Time Estimate
        /// </summary>
        private void SendTimeEstimate()
        {
            TimeEstimates estimates = new TimeEstimates()
            {
                Total = totalDayEstimate,
                PhaseOne = phaseOneDayEstimate,
                PhaseTwo = phaseTwoDayEstimate,
                PhaseThree = phaseThreeDayEstimate,
            };

            // Send the Topic payload
            Send(new TimeEstimationPayload()
                { Content = estimates },
                IsType(DedicantType.IdeaReviewer)
            );
        }
    }
}
