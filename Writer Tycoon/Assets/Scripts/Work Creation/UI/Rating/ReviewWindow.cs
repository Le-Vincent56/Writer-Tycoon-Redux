using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Ideation.About;

namespace WriterTycoon.WorkCreation.UI.Rating
{
    public class ReviewWindow : MonoBehaviour
    {
        [Header("Review Window Queue")]
        [SerializeField] private bool canOpen;
        [SerializeField] private Queue<(bool CanOpen, AboutInfo Info)> openWindowQueue;

        [Header("UI References")]
        [SerializeField] private CanvasGroup window;
        [SerializeField] private Text titleText;
        [SerializeField] private Text authorText;

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
        private EventBinding<HideReviewWindow> hideReviewWindowEvent;

        private void Awake()
        {
            // Verify the Canvas Group component
            if (window == null)
                window = GetComponent<CanvasGroup>();

            // Set variables
            originalPosition = window.transform.localPosition;
            canOpen = true;
        }

        private void OnEnable()
        {
            showReviewWindowEvent = new EventBinding<ShowReviewWindow>(EnqueueWindow);
            EventBus<ShowReviewWindow>.Register(showReviewWindowEvent);

            hideReviewWindowEvent = new EventBinding<HideReviewWindow>(HideWindow);
            EventBus<HideReviewWindow>.Register(hideReviewWindowEvent);
        }

        private void OnDisable()
        {
            EventBus<ShowReviewWindow>.Deregister(showReviewWindowEvent);
            EventBus<HideReviewWindow>.Deregister(hideReviewWindowEvent);
        }

        private void Update()
        {
            // Exit case - if the window cannot be opened
            if (!canOpen) return;

            // Exit case - if the Queue is empty
            if (openWindowQueue.Count <= 0) return;

            // Dequeue the item
            (bool CanOpen, AboutInfo Info) = openWindowQueue.Dequeue();

            // Show the window
            ShowWindow(Info);

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
        /// Callback function to enqueue a window opening
        /// </summary>
        private void EnqueueWindow(ShowReviewWindow eventData)
        {
            // Enqueue to open the window
            openWindowQueue.Enqueue((false, eventData.AboutInfo));
        }

        /// <summary>
        /// Show the Review window
        /// </summary>
        private void ShowWindow(AboutInfo info)
        {
            // Set the work info text
            SetWorkInfoText(info.Title, info.Author);

            // Set the window's initial position to be off-screen above (adjust this value as needed)
            Vector3 startPos = new(
                originalPosition.x,
                originalPosition.y + translateValue,
                originalPosition.z
            );
            window.transform.localPosition = startPos;

            // Fade in
            Fade(1f, duration, () => {
                window.interactable = true;
                window.blocksRaycasts = true;

                // Create a seqeuence
                reviewTextSequence = DOTween.Sequence();

                // Iterate through each review group
                for(int i = 0; i < reviews.Length; i++)
                {
                    // Fade them in consecutively
                    reviewTextSequence.Append(reviews[i].Fade(1f));
                }
            });

            // Translate down
            Translate(-translateValue, duration);
        }

        /// <summary>
        /// Hide the window with a little more flair for successful Work creation
        /// </summary>
        private void HideWindow()
        {
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
                    }, Ease.OutCirc);

                    // And translate downwards
                    Translate(-translateValue * 2f, duration, null, Ease.OutCirc);
                },
                Ease.OutBack
            );
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
            float targetPos = window.transform.localPosition.y + endTranslateValue;

            // Set the tween animation
            translateTween = window.transform.DOLocalMoveY(targetPos, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onEnd == null) return;

            // Hook up callback events
            translateTween.onComplete += onEnd;
        }
    }
}