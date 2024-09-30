using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WriterTycoon.Entities;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Ideation.About;
using WriterTycoon.WorkCreation.Ideation.Audience;
using WriterTycoon.WorkCreation.Ideation.Compatibility;
using WriterTycoon.WorkCreation.Ideation.Genres;
using WriterTycoon.WorkCreation.Ideation.TimeEstimation;
using WriterTycoon.WorkCreation.Ideation.Topics;
using WriterTycoon.WorkCreation.Ideation.WorkTypes;
using WriterTycoon.WorkCreation.Mediation;

namespace WriterTycoon.WorkCreation.Ideation.Review
{
    [Serializable]
    public struct ReviewData
    {
        public int Hash;
        public List<IWorker> Workers;
        public AboutInfo AboutInfo;
        public WorkType WorkType;
        public float TargetScore;
        public AudienceType AudienceType;
        public List<Topic> Topics;
        public List<Genre> Genres;
        public TimeEstimates TimeEstimates;
        public CompatibilityInfo CompatibilityInfo;
    }

    public class IdeaReviewer : Dedicant
    {
        [SerializeField] private List<IWorker> workers;
        [SerializeField] private AboutInfo aboutInfo;
        [SerializeField] private WorkType workType;
        [SerializeField] private float targetScore;
        [SerializeField] private AudienceType audienceType;
        [SerializeField] private List<Topic> topics;
        [SerializeField] private List<Genre> genres;
        [SerializeField] private TimeEstimates timeEstimates;
        [SerializeField] private CompatibilityInfo compatibilityInfo;

        public UnityAction<List<IWorker>> OnUpdateWorkers = delegate { };
        public UnityAction<AboutInfo> OnUpdateAboutData = delegate { };
        public UnityAction<WorkType> OnUpdateWorkType = delegate { };
        public UnityAction<AudienceType> OnUpdateAudienceType = delegate { };
        public UnityAction<List<Topic>> OnUpdateTopics = delegate { };
        public UnityAction<List<Genre>> OnUpdateGenres = delegate { };
        public UnityAction<TimeEstimates> OnUpdateTimeEstimate = delegate { };

        public override string Name { get => "Idea Reviewer"; }
        public override DedicantType Type { get => DedicantType.IdeaReviewer; }

        private void Awake()
        {
            topics = new();
            genres = new();
        }

        /// <summary>
        /// Set the Title for the Idea Reviewer
        /// </summary>
        public void SetAboutInfo(AboutInfo aboutInfo)
        {
            // Set data
            this.aboutInfo = aboutInfo;

            // Invoke the update event
            OnUpdateAboutData.Invoke(this.aboutInfo);

            // Update review data
            UpdateReviewData();
        }

        /// <summary>
        /// Set the Work Type for the Idea Reviewer
        /// </summary>
        public void SetWorkType(WorkType workType, float targetScore)
        {
            // Set data
            this.workType = workType;
            this.targetScore = targetScore;

            // Invoke the update event
            OnUpdateWorkType.Invoke(this.workType);

            // Update review data
            UpdateReviewData();
        }

        /// <summary>
        /// Set the Audience Type for the Idea Reviewer
        /// </summary>
        public void SetAudienceType(AudienceType audienceType)
        {
            // Set data
            this.audienceType = audienceType;

            // Invoke the update event
            OnUpdateAudienceType.Invoke(this.audienceType);

            // Update review data
            UpdateReviewData();
        }

        /// <summary>
        /// Set the Topics for the Idea Reviewer
        /// </summary>
        public void SetTopics(List<Topic> topics)
        {
            // Set data
            this.topics = topics;

            // Invoke the update event
            OnUpdateTopics.Invoke(this.topics);

            // Update review data
            UpdateReviewData();
        }

        /// <summary>
        /// Set the Genres for the Idea Reviewer
        /// </summary>
        public void SetGenres(List<Genre> genres)
        {
            // Set data
            this.genres = genres;

            // Invoke the update event
            OnUpdateGenres.Invoke(this.genres);

            // Update review data
            UpdateReviewData();
        }

        /// <summary>
        /// Set the Time Estimate for the Idea Reviewer
        /// </summary>
        public void SetTimeEstimate(TimeEstimates estimates)
        {
            // Set data
            timeEstimates = estimates;

            // Invoke the update event
            OnUpdateTimeEstimate.Invoke(timeEstimates);

            // Update review data
            UpdateReviewData();
        }

        /// <summary>
        /// Set the list of Workers for the Idea Reviewer
        /// </summary>
        public void SetWorkers(List<IWorker> workers)
        {
            // Set data
            this.workers = workers;

            // Invoke the update event
            OnUpdateWorkers.Invoke(this.workers);

            // Update review data
            UpdateReviewData();
        }

        public void SetCompatibilityInfo(CompatibilityInfo compatibilityInfo)
        {
            // Set data
            this.compatibilityInfo = compatibilityInfo;

            // Update review data
            UpdateReviewData();
        }

        /// <summary>
        /// Update and send the Review Data
        /// </summary>
        private void UpdateReviewData()
        {
            EventBus<SendReviewData>.Raise(new SendReviewData
            {
                ReviewData = new ReviewData()
                {
                    Workers = workers,
                    AboutInfo = aboutInfo,
                    WorkType = workType,
                    TargetScore = targetScore,
                    AudienceType = audienceType,
                    Topics = topics,
                    Genres = genres,
                    TimeEstimates = timeEstimates,
                    CompatibilityInfo = compatibilityInfo
                }
            });
        }
    }
}