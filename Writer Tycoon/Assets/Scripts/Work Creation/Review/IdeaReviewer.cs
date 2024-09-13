using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WriterTycoon.WorkCreation.Audience;
using WriterTycoon.WorkCreation.Genres;
using WriterTycoon.WorkCreation.Mediation;
using WriterTycoon.WorkCreation.Topics;
using WriterTycoon.WorkCreation.WorkTypes;

namespace WriterTycoon.WorkCreation.Review
{
    public class IdeaReviewer : Dedicant
    {
        [SerializeField] private string title;
        [SerializeField] private WorkType workType;
        [SerializeField] private AudienceType audienceType;
        [SerializeField] private List<Topic> topics;
        [SerializeField] private List<Genre> genres;
        [SerializeField] private int dayEstimate;

        public UnityAction<string> OnUpdateTitle = delegate { };
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
        public void SetTitle(string title)
        {
            // Set data
            this.title = title;

            // Invoke the update event
            OnUpdateTitle.Invoke(this.title);
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
        }
    }
}