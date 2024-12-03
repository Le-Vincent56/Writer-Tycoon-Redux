using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using GhostWriter.Patterns.EventBus;
using GhostWriter.Patterns.StateMachine;
using GhostWriter.WorkCreation.UI.Publication.States;
using GhostWriter.World.GeneralUI;
using GhostWriter.Patterns.ServiceLocator;

namespace GhostWriter.WorkCreation.UI.Publication
{
    public class PublicationHistoryWindow : MonoBehaviour, IWindow
    {
        [Header("State")]
        [SerializeField] private int state;
        private Dictionary<int, PublicationCard> publicationCardsDict;

        [Header("Instantiation")]
        [SerializeField] private GameObject publishedWorkCard;
        [SerializeField] private Transform container;
        private RectTransform rectTransform;

        [SerializeField] private List<CanvasGroup> canvasGroups;
        private CanvasGroup window;
        private Vector3 originalPosition;

        [Header("Tweening Variables")]
        [SerializeField] private float translateValue;
        [SerializeField] private float animationDuration;
        private Tween fadeTween;
        private Tween translateTween;

        private StateMachine stateMachine;

        private EventBinding<OpenPublicationHistory> openPublicationHistoryEvent;
        private EventBinding<SetPublicationHistoryState> setPublicationHistoryEvent;
        private EventBinding<CreatePublicationCard> createPublicationCardEvent;
        private EventBinding<UpdatePublicationCard> updatePublicationCardsEvent;
        private EventBinding<ClosePublicationHistory> closePublicationHistoryEvent;

        public int SHELF { get => 0; }
        public int DETAILS { get => 1; }

        public bool Open { get; set; }

        private void Awake()
        {
            // Verify the CanvasGroup component
            if(window == null)
                window = GetComponent<CanvasGroup>();

            // Verify the RectTransform component
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            // Instantiate the dictionary
            publicationCardsDict = new();

            // Set the original window position
            originalPosition = rectTransform.anchoredPosition;

            // Initialize the state machine
            stateMachine = new();

            // Create states
            ShelfState shelfState = new ShelfState(canvasGroups[SHELF]);
            DetailsState detailsState = new DetailsState(canvasGroups[DETAILS]);

            // Define state transitions
            stateMachine.At(shelfState, detailsState, new FuncPredicate(() => state == 1));
            stateMachine.At(detailsState, shelfState, new FuncPredicate(() => state == 0));

            // Set an initial state
            state = 0;
            stateMachine.SetState(shelfState);
        }

        private void OnEnable()
        {
            openPublicationHistoryEvent = new EventBinding<OpenPublicationHistory>(ShowWindow);
            EventBus<OpenPublicationHistory>.Register(openPublicationHistoryEvent);

            setPublicationHistoryEvent = new EventBinding<SetPublicationHistoryState>(SetState);
            EventBus<SetPublicationHistoryState>.Register(setPublicationHistoryEvent);

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
            EventBus<SetPublicationHistoryState>.Deregister(setPublicationHistoryEvent);
            EventBus<CreatePublicationCard>.Deregister(createPublicationCardEvent);
            EventBus<UpdatePublicationCard>.Deregister(updatePublicationCardsEvent);
            EventBus<ClosePublicationHistory>.Deregister(closePublicationHistoryEvent);
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
            // Update the state machine
            stateMachine.Update();
        }

        /// <summary>
        /// Callback function set the Publication History state
        /// </summary>
        /// <param name="eventData"></param>
        private void SetState(SetPublicationHistoryState eventData)
        {
            state = eventData.State;
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

            // Set the window to open
            Open = true;
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

            // Set the window to closed
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