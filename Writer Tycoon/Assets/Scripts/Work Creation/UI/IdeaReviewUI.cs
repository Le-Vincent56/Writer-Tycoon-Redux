using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.About;
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
        [SerializeField] private float fadeDuration;
        [SerializeField] private float highlightDuration;
        [SerializeField] private Color missingColorHighlight;
        [SerializeField] private Color missingColorDefault;

        [SerializeField] private Text titleText;
        [SerializeField] private Text workTypeText;
        [SerializeField] private Text audienceTypeText;
        [SerializeField] private Text topicsText;
        [SerializeField] private Text genresText;
        [SerializeField] private Text timeEstimateText;
        [SerializeField] private Text missingText;

        EventBinding<NotifyFailedCreation> notifyFailedCreationEvent;

        private void Awake()
        {
            // Verify the Idea Reviewer
            if(ideaReviewer == null)
                ideaReviewer = GetComponent<IdeaReviewer>();

            // Set the missing text to be invisible
            Color invisibleColor = missingColorDefault;
            invisibleColor.a = 0f;
            missingText.color = invisibleColor;
        }

        private void OnEnable()
        {
            notifyFailedCreationEvent = new EventBinding<NotifyFailedCreation>(NotifyFailedCreation);
            EventBus<NotifyFailedCreation>.Register(notifyFailedCreationEvent);

            ideaReviewer.OnUpdateAboutData += UpdateTitleReview;
            ideaReviewer.OnUpdateWorkType += UpdateWorkTypeReview;
            ideaReviewer.OnUpdateAudienceType += UpdateAudienceReview;
            ideaReviewer.OnUpdateTopics += UpdateTopicsReview;
            ideaReviewer.OnUpdateGenres += UpdateGenresReview;
            ideaReviewer.OnUpdateTimeEstimate += UpdateTimeEstimateReview;
        }

        private void OnDisable()
        {
            EventBus<NotifyFailedCreation>.Deregister(notifyFailedCreationEvent);

            ideaReviewer.OnUpdateAboutData -= UpdateTitleReview;
            ideaReviewer.OnUpdateWorkType -= UpdateWorkTypeReview;
            ideaReviewer.OnUpdateAudienceType -= UpdateAudienceReview;
            ideaReviewer.OnUpdateTopics -= UpdateTopicsReview;
            ideaReviewer.OnUpdateGenres -= UpdateGenresReview;
            ideaReviewer.OnUpdateTimeEstimate -= UpdateTimeEstimateReview;
        }

        private void NotifyFailedCreation(NotifyFailedCreation eventData)
        {
            // Set data
            ReviewData reviewData = eventData.ReviewData;

            // Create a starting string
            string failedText = "Missing the Following:";

            if (reviewData.AboutInfo.Title == null || reviewData.AboutInfo.Title == string.Empty)
                failedText += "\nTitle";
            if (reviewData.AboutInfo.Author == null || reviewData.AboutInfo.Author == string.Empty)
                failedText += "\nAuthor";
            if (reviewData.AboutInfo.Description == null || reviewData.AboutInfo.Description == string.Empty)
                failedText += "\nDescription";
            if (reviewData.WorkType == WorkType.None)
                failedText += "\nWork Type";
            if (reviewData.AudienceType == AudienceType.None)
                failedText += "\nAudience";
            if (reviewData.Topics == null || reviewData.Topics.Count == 0)
                failedText += "\nTopic(s)";
            if (reviewData.Genres == null || reviewData.Genres.Count == 0)
                failedText += "\nGenre(s)";

            // Set the text
            missingText.text = failedText;

            // Fade in the text
            missingText.DOFade(1f, fadeDuration);

            // Create a simultaneous fade-in sequence for a highlight
            Sequence fadeSequence = DOTween.Sequence();
            fadeSequence.Append(missingText.DOColor(
                missingColorHighlight,
                highlightDuration
            ));
            fadeSequence.Append(missingText.DOColor(
                missingColorDefault,
                highlightDuration * 3f
            ));
        }

        /// <summary>
        /// Event handler for updating the Title for the Idea Reviewer
        /// </summary>
        private void UpdateTitleReview(AboutInfo aboutInfo)
        {
            string titleDisplayText = (aboutInfo.Title == null || aboutInfo.Title == string.Empty)
                ? "-----"
                : aboutInfo.Title;

            titleText.text = titleDisplayText;
        }

        /// <summary>
        /// Event handler for updating the Work Type for the Idea Reviewer
        /// </summary>
        private void UpdateWorkTypeReview(WorkType workType)
        {
            // Create a container for the name
            string workTypeName = "-----";

            // Decide the Work Type name
            switch (workType)
            {
                case WorkType.None:
                    workTypeName = "-----";
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
            string audienceTypeName = "-----";

            // Decide the Work Type name
            switch (audienceType)
            {
                case AudienceType.None:
                    audienceTypeName = "-----";
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
            string selectedTopicsText = (topics == null || topics.Count == 0)
                ? "-----"
                : "";

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
            string selectedGenresText = (genres == null || genres.Count == 0)
                ? "-----"
                : "";

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
        private void UpdateTimeEstimateReview(int dayEstimate)
        {
            string dayEstimateText = (dayEstimate > 0)
                ? $"{dayEstimate} Days"
                : "-----";

            timeEstimateText.text = dayEstimateText;
        }
    }
}