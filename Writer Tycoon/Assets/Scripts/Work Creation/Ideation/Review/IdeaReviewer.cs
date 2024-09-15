using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Ideation.About;
using WriterTycoon.WorkCreation.Ideation.Audience;
using WriterTycoon.WorkCreation.Ideation.Genres;
using WriterTycoon.WorkCreation.Ideation.Mediation;
using WriterTycoon.WorkCreation.Ideation.Topics;
using WriterTycoon.WorkCreation.Ideation.WorkTypes;

namespace WriterTycoon.WorkCreation.Ideation.Review
{
    [Serializable]
    public struct ReviewData
    {
        public AboutInfo AboutInfo;
        public WorkType WorkType;
        public AudienceType AudienceType;
        public List<Topic> Topics;
        public List<Genre> Genres;
        public int DayEstimate;
    }

    public class IdeaReviewer : Dedicant
    {
        [SerializeField] private AboutInfo aboutInfo;
        [SerializeField] private WorkType workType;
        [SerializeField] private AudienceType audienceType;
        [SerializeField] private List<Topic> topics;
        [SerializeField] private List<Genre> genres;
        [SerializeField] private int dayEstimate;

        public UnityAction<AboutInfo> OnUpdateAboutData = delegate { };
        public UnityAction<WorkType> OnUpdateWorkType = delegate { };
        public UnityAction<AudienceType> OnUpdateAudienceType = delegate { };
        public UnityAction<List<Topic>> OnUpdateTopics = delegate { };
        public UnityAction<List<Genre>> OnUpdateGenres = delegate { };
        public UnityAction<int> OnUpdateTimeEstimate = delegate { };

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
        public void SetWorkType(WorkType workType)
        {
            // Set data
            this.workType = workType;

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
        public void SetTimeEstimate(int dayEstimate)
        {
            // Set data
            this.dayEstimate = dayEstimate;

            // Invoke the update event
            OnUpdateTimeEstimate.Invoke(this.dayEstimate);

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
                    AboutInfo = aboutInfo,
                    WorkType = workType,
                    AudienceType = audienceType,
                    Topics = topics,
                    Genres = genres,
                    DayEstimate = dayEstimate
                }
            });
        }
    }
}