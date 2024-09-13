using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.WorkCreation.Audience;
using WriterTycoon.WorkCreation.Genres;
using WriterTycoon.WorkCreation.Review;
using WriterTycoon.WorkCreation.Topics;
using WriterTycoon.WorkCreation.WorkTypes;

namespace WriterTycoon.WorkCreation.UI
{
    public class IdeaReviewUI : MonoBehaviour
    {
        private IdeaReviewer ideaReviewer;
        [SerializeField] private Text titleText;
        [SerializeField] private Text workTypeText;
        [SerializeField] private Text audienceTypeText;
        [SerializeField] private Text topicsText;
        [SerializeField] private Text genresText;
        [SerializeField] private Text timeEstimateText;

        private void Awake()
        {
            // Verify the Idea Reviewer
            if(ideaReviewer == null)
                ideaReviewer = GetComponent<IdeaReviewer>();
        }

        private void OnEnable()
        {
            ideaReviewer.OnUpdateTitle += UpdateTitleReview;
            ideaReviewer.OnUpdateWorkType += UpdateWorkTypeReview;
            ideaReviewer.OnUpdateAudienceType += UpdateAudienceReview;
            ideaReviewer.OnUpdateTopics += UpdateTopicsReview;
            ideaReviewer.OnUpdateGenres += UpdateGenresReview;
            ideaReviewer.OnUpdateTimeEstimate += UpdateTimeEstimateReview;
        }

        private void OnDisable()
        {
            ideaReviewer.OnUpdateTitle -= UpdateTitleReview;
            ideaReviewer.OnUpdateWorkType -= UpdateWorkTypeReview;
            ideaReviewer.OnUpdateAudienceType -= UpdateAudienceReview;
            ideaReviewer.OnUpdateTopics -= UpdateTopicsReview;
            ideaReviewer.OnUpdateGenres -= UpdateGenresReview;
            ideaReviewer.OnUpdateTimeEstimate -= UpdateTimeEstimateReview;
        }

        /// <summary>
        /// Event handler for updating the Title for the Idea Reviewer
        /// </summary>
        /// <param name="title"></param>
        private void UpdateTitleReview(string title) => titleText.text = title;

        /// <summary>
        /// Event handler for updating the Work Type for the Idea Reviewer
        /// </summary>
        private void UpdateWorkTypeReview(WorkType workType)
        {
            // Create a container for the name
            string workTypeName = "";

            // Decide the Work Type name
            switch (workType)
            {
                case WorkType.None:
                    workTypeName = "None";
                    break;
                case WorkType.Poetry:
                    workTypeName = "Poetry";
                    break;
                case WorkType.FlashFiction:
                    workTypeName = "Flash Fiction";
                    break;
                case WorkType.ShortStory:
                    workTypeName = "Short Story";
                    break;
                case WorkType.Novella:
                    workTypeName = "Novella";
                    break;
                case WorkType.Novel:
                    workTypeName = "Novel";
                    break;
                case WorkType.Screenplay:
                    workTypeName = "Screenplay";
                    break;
            }

            // Set the text
            workTypeText.text = workTypeName;
        }

        /// <summary>
        /// Event handler for updating the Audience Type for the Idea Reviewer
        /// </summary>
        private void UpdateAudienceReview(AudienceType audienceType)
        {
            // Create a container for the name
            string audienceTypeName = "";

            // Decide the Work Type name
            switch (audienceType)
            {
                case AudienceType.None:
                    audienceTypeName = "None";
                    break;
                case AudienceType.Children:
                    audienceTypeName = "Children";
                    break;
                case AudienceType.Teens:
                    audienceTypeName = "Teens";
                    break;
                case AudienceType.YoungAdults:
                    audienceTypeName = "Young Adults";
                    break;
                case AudienceType.Adults:
                    audienceTypeName = "Adults";
                    break;
            }

            // Set the text
            audienceTypeText.text = audienceTypeName;
        }

        /// <summary>
        /// Event handler for updating the Topics for the Idea Reviewer
        /// </summary>
        private void UpdateTopicsReview(List<Topic> topics)
        {
            // Create a starting string
            string selectedTopicsText = "";

            // Iterate through each selected Topic
            for (int i = 0; i < topics.Count; i++)
            {
                // Add the selected Topic's name
                selectedTopicsText += $"{topics[i].Name}";

                // Add commas if not the last element
                if (i != topics.Count - 1)
                    selectedTopicsText += ", ";
            }

            // Set the text
            topicsText.text = selectedTopicsText;
        }

        /// <summary>
        /// Event handler for updating the Genres for the Idea Reviewer
        /// </summary>
        private void UpdateGenresReview(List<Genre> genres)
        {
            // Create a starting string
            string selectedGenresText = "";

            // Iterate through each selected Genre
            for (int i = 0; i < genres.Count; i++)
            {
                // Add the selected Genre's name
                selectedGenresText += $"{genres[i].Name}";

                // Add commas if not the last element
                if (i != genres.Count - 1)
                    selectedGenresText += ", ";
            }

            // Set the text
            genresText.text = selectedGenresText;
        }

        /// <summary>
        /// Event handler for updating the Time Estimate for the Idea Reviewer
        /// </summary>
        private void UpdateTimeEstimateReview(int dayEstimate) => timeEstimateText.text = $"{dayEstimate} Days";
    }
}