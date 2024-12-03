using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GhostWriter.Patterns.EventBus;
using GhostWriter.WorkCreation.Ideation.About;
using GhostWriter.World.GeneralUI;
using GhostWriter.Patterns.ServiceLocator;

namespace GhostWriter.WorkCreation.UI.Rating
{
    public class ReviewWindow : CodeableButton, IWindow
    {
        [Header("Review Window Queue")]
        [SerializeField] private bool canOpen;
        [SerializeField] private Queue<(bool CanOpen, AboutInfo Info, int Score)> openWindowQueue;

        [Header("UI References")]
        [SerializeField] private CanvasGroup window;
        [SerializeField] private Text titleText;
        [SerializeField] private Text authorText;
        [SerializeField] private Text scoreText;
        private RectTransform rectTransform;

        [SerializeField] private Color terribleColor;
        [SerializeField] private Color poorColor;
        [SerializeField] private Color neutralColor;
        [SerializeField] private Color goodColor;
        [SerializeField] private Color excellentColor;

        [Header("Review Objects")]
        [SerializeField] private Review[] reviews;

        [Header("Tweening Variables")]
        [SerializeField] private float translateValue;
        [SerializeField] private float duration;
        private Vector3 originalPosition;
        private Tween fadeTween;
        private Tween translateTween;
        private Sequence reviewTextSequence;

        private EventBinding<ShowReviewWindow> showReviewWindowEvent;

        public bool Open { get; set; }

        protected override void Awake()
        {
            // Verify the Canvas Group component
            if (window == null)
                window = GetComponent<CanvasGroup>();

            // Verify the Button component
            if(button == null)
                button = GetComponentInChildren<Button>();

            // Verify the RectTransform component
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            // Initialize the queue
            openWindowQueue = new();

            // Set variables
            originalPosition = rectTransform.anchoredPosition;
            canOpen = true;

            // Set button event
            button.onClick.AddListener(OnClick);
        }

        private void OnEnable()
        {
            showReviewWindowEvent = new EventBinding<ShowReviewWindow>(EnqueueWindow);
            EventBus<ShowReviewWindow>.Register(showReviewWindowEvent);
        }

        private void OnDisable()
        {
            EventBus<ShowReviewWindow>.Deregister(showReviewWindowEvent);
        }

        private void Start()
        {
            // Get the WindowTracker
            WindowTracker windowTracker = ServiceLocator.ForSceneOf(this).Get<WindowTracker>();

            // Register this as a Window
            windowTracker.RegisterWindow(this);
        }

        private void Update()
        {
            // Exit case - if the window cannot be opened
            if (!canOpen) return;

            // Exit case - if the Queue is empty
            if (openWindowQueue.Count <= 0) return;

            // Dequeue the item
            (bool CanOpen, AboutInfo Info, int Score) = openWindowQueue.Dequeue();

            // Show the window
            ShowWindow(Info, Score);

            // Don't allow the window to open
            canOpen = CanOpen;
        }

        /// <summary>
        /// Set the Work info text
        /// </summary>
        private void SetWorkInfoText(string title, string author)
        {
            // Set the title and author 
            titleText.text = title;
            authorText.text = author;
        }

        /// <summary>
        /// Set the score text
        /// </summary>
        private void SetScoreText(int score)
        {
            scoreText.text = $"{score}%";

            // Set the color of the score text depending on the score
            switch(score)
            {
                case <= 20:
                    scoreText.color = terribleColor;
                    break;

                case <= 40:
                    scoreText.color = poorColor;
                    break;

                case <= 60:
                    scoreText.color = neutralColor;
                    break;

                case <= 80:
                    scoreText.color = goodColor;
                    break;

                case <= 100:
                    scoreText.color = excellentColor;
                    break;
            }
        }

        /// <summary>
        /// Callback function to enqueue a window opening
        /// </summary>
        private void EnqueueWindow(ShowReviewWindow eventData)
        {
            // Enqueue to open the window
            openWindowQueue.Enqueue((false, eventData.AboutInfo, eventData.Score));
        }

        /// <summary>
        /// Show the Review window
        /// </summary>
        private void ShowWindow(AboutInfo info, int score)
        {
            // Pause the Calendar
            EventBus<ChangeCalendarPauseState>.Raise(new ChangeCalendarPauseState()
            {
                Paused = true,
                AllowSpeedChanges = false,
            });

            // Set the work info text
            SetWorkInfoText(info.Title, info.Author);

            // Set the score text
            SetScoreText(score);

            // Set the window's initial position to be off-screen above (adjust this value as needed)
            Vector3 startPos = new(
                originalPosition.x,
                originalPosition.y + translateValue,
                originalPosition.z
            );
            rectTransform.anchoredPosition = startPos;

            // Fade in
            Fade(1f, duration, () => {
                window.interactable = true;
                window.blocksRaycasts = true;

                // Create a seqeuence
                reviewTextSequence = DOTween.Sequence();

                // Iterate through each review group
                for (int i = 0; i < reviews.Length; i++)
                {
                    // Fade them in consecutively
                    reviewTextSequence.Append(reviews[i].Fade(1f));
                }

                // Zoom in the score text sequence while also fading it in
                reviewTextSequence.Append(
                    scoreText.transform.DOScale(Vector3.one, 0.4f)
                        .From(new Vector3(4f, 4f, 1f))
                        .SetEase(Ease.OutBounce)
                );
                reviewTextSequence.Join(scoreText.DOFade(1f, 0.6f).From(0f));

                // Fade in the exit button and set it to interactable when fully faded
                Tween exitButtonFade = button.image.DOFade(1f, duration).From(0f);
                exitButtonFade.onComplete += () =>
                {
                    button.interactable = true;
                };
                reviewTextSequence.Append(exitButtonFade);
            });

            // Translate down
            Translate(-translateValue, duration);
            
            // Set to open
            Open = true;
        }

        /// <summary>
        /// Hide the window with a little more flair for successful Work creation
        /// </summary>
        protected override void OnClick()
        {
            // Unpause the Calendar
            EventBus<ChangeCalendarPauseState>.Raise(new ChangeCalendarPauseState()
            {
                Paused = false,
                AllowSpeedChanges = true,
            });

            // Translate up
            Translate(
                translateValue * (3f / 4f),
                duration / 3f,
                () =>
                {
                    // When done, fade out
                    Fade(0f, duration, () =>
                    {
                        window.interactable = false;
                        window.blocksRaycasts = false;
                        canOpen = true;
                        button.interactable = false;

                        // Fade out each review
                        for (int i = 0; i < reviews.Length; i++)
                        {
                            reviews[i].Fade(0f);
                        }

                        // Fade out the button
                        button.image.DOFade(0f, 0f);
                    }, Ease.OutCirc);

                    // And translate downwards
                    Translate(-translateValue * 2f, duration, null, Ease.OutCirc);
                },
                Ease.OutBack
            );

            // Set the window closed
            Open = false;
        }

        /// <summary>
        /// Handle fading for the Window
        /// </summary>
        private void Fade(float endFadeValue, float duration, TweenCallback onEnd = null, Ease easeType = Ease.OutQuint)
        {
            // Kill the current fade tween if it exists
            fadeTween?.Kill(false);

            // Set the fade animation
            fadeTween = window.DOFade(endFadeValue, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onEnd == null) return;

            // Hook up callback events
            fadeTween.onComplete += onEnd;
        }

        /// <summary>
        /// Handle translating for the Window
        /// </summary>
        private void Translate(float endTranslateValue, float duration, TweenCallback onEnd = null, Ease easeType = Ease.OutQuint)
        {
            // Kill the current translate tween if it exists
            translateTween?.Kill(false);

            // Calculate the target position
            float targetPos = rectTransform.anchoredPosition.y + endTranslateValue;

            // Set the tween animation
            translateTween = rectTransform.DOAnchorPosY(targetPos, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onEnd == null) return;

            // Hook up callback events
            translateTween.onComplete += onEnd;
        }
    }
}