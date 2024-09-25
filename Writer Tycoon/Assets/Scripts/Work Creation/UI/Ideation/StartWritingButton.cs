using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Entities.Player;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Utilities.Hash;
using WriterTycoon.WorkCreation.Ideation.About;
using WriterTycoon.WorkCreation.Ideation.Audience;
using WriterTycoon.WorkCreation.Ideation.Review;
using WriterTycoon.WorkCreation.Ideation.TimeEstimation;
using WriterTycoon.WorkCreation.Ideation.WorkTypes;

namespace WriterTycoon.WorkCreation.UI.Ideation
{
    public class StartWritingButton : MonoBehaviour
    {
        private Button button;
        [SerializeField] private ReviewData reviewData;
        private EventBinding<SendReviewData> reviewDataEvent;

        private void Awake()
        {
            // Verify the button component
            if(button == null)
                button = GetComponent<Button>();

            // Create a default Review Data
            reviewData = new ReviewData()
            {
                AboutInfo = new AboutInfo()
                {
                    Title = string.Empty,
                    Author = string.Empty,
                    Description = string.Empty,
                },
                WorkType = WorkType.None,
                AudienceType = AudienceType.None,
                Topics = null,
                Genres = null,
                TimeEstimates = new TimeEstimates()
                {
                    Total = 0,
                    PhaseOne = 0,
                    PhaseTwo = 0,
                    PhaseThree = 0
                }
            };

            // Add event listener
            button.onClick.AddListener(TryBeginWriting);
        }

        private void OnEnable()
        {
            reviewDataEvent = new EventBinding<SendReviewData>(UpdateReviewData);
            EventBus<SendReviewData>.Register(reviewDataEvent);
        }

        private void OnDisable()
        {
            EventBus<SendReviewData>.Deregister(reviewDataEvent);
        }

        /// <summary>
        /// Update the Review Data
        /// </summary>
        private void UpdateReviewData(SendReviewData eventData) => reviewData = eventData.ReviewData;

        /// <summary>
        /// Callback function to try to begin writing
        /// </summary>
        private void TryBeginWriting()
        {
            bool willSucceed = true;

            // Determine failure conditions
            if (reviewData.AboutInfo.Title == null || reviewData.AboutInfo.Title == string.Empty)
                willSucceed = false;
            if (reviewData.AboutInfo.Author == null || reviewData.AboutInfo.Author == string.Empty)
                willSucceed = false;
            if (reviewData.AboutInfo.Description == null || reviewData.AboutInfo.Description == string.Empty)
                willSucceed = false;
            if (reviewData.WorkType == WorkType.None)
                willSucceed = false;
            if (reviewData.AudienceType == AudienceType.None)
                willSucceed = false;
            if (reviewData.Topics == null || reviewData.Topics.Count == 0)
                willSucceed = false;
            if (reviewData.Genres == null || reviewData.Genres.Count == 0)
                willSucceed = false;
            if (reviewData.Workers == null || reviewData.Workers.Count == 0)
                willSucceed = false;
            if (reviewData.TimeEstimates.Total == 0)
                willSucceed = false;

            // Check if writing will succeed
            if (willSucceed)
                // If so, update with successful UI
                UpdateSuccessUI();
            else
                // If not, update with failed UI
                UpdateFailedUI();
        }

        /// <summary>
        /// Update the UI to reflect failed creation
        /// </summary>
        private void UpdateFailedUI() => EventBus<NotifyFailedCreation>.Raise(new NotifyFailedCreation() { ReviewData = reviewData });

        /// <summary>
        /// Update the UI to reflect successful creation
        /// </summary>
        private void UpdateSuccessUI()
        {
            // Generate a hash
            int hash = HashUtils.GenerateHash(reviewData.AboutInfo.Title);

            // Set the hash
            reviewData.Hash = hash;

            // Unpause the Calendar
            EventBus<ChangeCalendarPauseState>.Raise(new ChangeCalendarPauseState()
            {
                Paused = false,
                AllowSpeedChanges = true
            });

            // Create a progress card
            EventBus<CreateProgressCard>.Raise(new CreateProgressCard()
            {
                Hash = hash,
                Title = reviewData.AboutInfo.Title
            });

            // Notify that the Work was successfully created
            EventBus<NotifySuccessfulCreation>.Raise(new NotifySuccessfulCreation() 
            { 
                ReviewData = reviewData
            });

            // Move the player (on top of the chair)
            EventBus<CommandPlayerPosition>.Raise(new CommandPlayerPosition()
            {
                TargetPosition = new Vector2Int(9, 5),
                Type = CommandType.Computer
            });

            // Clear ideation to prepare for a new creation
            EventBus<ClearIdeation>.Raise(new ClearIdeation());
        }
    }
}