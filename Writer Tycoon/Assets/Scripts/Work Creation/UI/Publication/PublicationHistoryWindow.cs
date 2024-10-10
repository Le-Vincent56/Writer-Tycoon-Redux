using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.WorkCreation.Publication;

namespace WriterTycoon.WorkCreation.UI.Publication
{
    public class PublicationHistoryWindow : MonoBehaviour
    {
        [Header("Instantiation")]
        [SerializeField] private GameObject publishedWorkCard;
        [SerializeField] private Transform container;

        private CanvasGroup window;
        private Vector3 originalPosition;

        [SerializeField] private float translateValue;
        [SerializeField] private float animationDuration;
        private Tween fadeTween;
        private Tween translateTween;

        private Dictionary<int, PublicationCard> publicationCardsDict;

        private EventBinding<OpenPublicationHistory> openPublicationHistoryEvent;
        private EventBinding<CreatePublicationCard> createPublicationCardEvent;
        private EventBinding<UpdatePublicationCard> updatePublicationCardsEvent;
        private EventBinding<ClosePublicationHistory> closePublicationHistoryEvent;

        private void Awake()
        {
            // Verify the CanvasGroup component
            if(window == null)
                window = GetComponent<CanvasGroup>();

            // Instantiate the dictionary
            publicationCardsDict = new();

            // Set the original window position
            originalPosition = window.transform.localPosition;
        }

        private void OnEnable()
        {
            openPublicationHistoryEvent = new EventBinding<OpenPublicationHistory>(ShowWindow);
            EventBus<OpenPublicationHistory>.Register(openPublicationHistoryEvent);

            createPublicationCardEvent = new EventBinding<CreatePublicationCard>(CreatePublicationCard);
            EventBus<CreatePublicationCard>.Register(createPublicationCardEvent);

            updatePublicationCardsEvent = new EventBinding<UpdatePublicationCard>(UpdatePublicationCard);
            EventBus<UpdatePublicationCard>.Register(updatePublicationCardsEvent);

            closePublicationHistoryEvent = new EventBinding<ClosePublicationHistory>(HideWindow);
            EventBus<ClosePublicationHistory>.Register(closePublicationHistoryEvent);
        }

        private void OnDisable()
        {
            EventBus<OpenPublicationHistory>.Deregister(openPublicationHistoryEvent);
            EventBus<CreatePublicationCard>.Deregister(createPublicationCardEvent);
            EventBus<UpdatePublicationCard>.Deregister(updatePublicationCardsEvent);
            EventBus<ClosePublicationHistory>.Deregister(closePublicationHistoryEvent);
        }

        /// <summary>
        /// Callback function to create a Publication Card
        /// </summary>
        private void CreatePublicationCard(CreatePublicationCard eventData)
        {
            // Instantiate the Game Object
            GameObject cardObj = Instantiate(publishedWorkCard, container);

            // Get the Publication Card component
            PublicationCard publication = cardObj.GetComponent<PublicationCard>();

            // Initialize the Publication Card
            publication.Initialize(eventData.PublishedWork);

            // Add the Publication Card to the Dictionary
            publicationCardsDict.Add(eventData.PublishedWork.Hash, publication);
        }

        /// <summary>
        /// Callback function to update a Publication Card
        /// </summary>
        private void UpdatePublicationCard(UpdatePublicationCard eventData)
        {
            // Exit case - the Publication Card associated with the given Hash does not exist
            if (!publicationCardsDict.TryGetValue(eventData.Hash, out PublicationCard value))
                return;

            // Set the published work data
            value.SetData(eventData.PublishedWork);
        }

        /// <summary>
        /// Show the Publication History Window
        /// </summary>
        private void ShowWindow()
        {
            // Set the window's initial position to be off-screen above (adjust this value as needed)
            Vector3 startPos = new(
                originalPosition.x,
                originalPosition.y + translateValue,
                originalPosition.z
            );
            window.transform.localPosition = startPos;

            // Fade in
            Fade(1f, animationDuration, () => {
                window.interactable = true;
                window.blocksRaycasts = true;
            });

            // Translate down
            Translate(-translateValue, animationDuration);
        }

        /// <summary>
        /// Hide the Publication History Window
        /// </summary>
        private void HideWindow()
        {
            // Fade out
            Fade(0f, animationDuration, () => {
                window.interactable = false;
                window.blocksRaycasts = false;
            });

            // Translate down
            Translate(-translateValue, animationDuration);
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