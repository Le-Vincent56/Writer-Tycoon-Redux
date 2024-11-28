using UnityEngine;
using DG.Tweening;
using GhostWriter.WorkCreation.UI.Ideation.States;
using GhostWriter.Patterns.StateMachine;
using GhostWriter.Patterns.EventBus;
using GhostWriter.World.GeneralUI;
using GhostWriter.Patterns.ServiceLocator;

namespace GhostWriter.WorkCreation.UI.Ideation
{
    public class CreateWorkWindow : MonoBehaviour, IWindow
    {
        private StateMachine stateMachine;
        [SerializeField] private CanvasGroup window;
        [SerializeField] private int state;
        [SerializeField] private CanvasGroup[] screens = new CanvasGroup[4];

        [SerializeField] private float translateValue;
        [SerializeField] private float duration;
        private Vector3 originalPosition;
        private Tween fadeTween;
        private Tween translateTween;

        private EventBinding<OpenCreateWorkMenu> openWorkMenuEvent;
        private EventBinding<CloseCreateWorkMenu> closeWorkMenuEvent;
        private EventBinding<NotifySuccessfulCreation> notifySuccessfulCreationEvent;
        private EventBinding<EndDevelopment> endDevelopmentEvent;

        public int IDEATION { get => 0; }
        public int TOPIC { get => 1; }
        public int GENRE { get => 2; }
        public int WORKERS { get => 3; }
        public int REVIEW { get => 4; }

        public bool Open { get; set; }

        private void Awake()
        {
            // Set the ideation state
            state = IDEATION;

            // Initialize the state machine
            stateMachine = new StateMachine();

            // Construct states
            IdeationState ideationState = new IdeationState(screens[IDEATION]);
            TopicState topicState = new TopicState(screens[TOPIC]);
            GenreState genreState = new GenreState(screens[GENRE]);
            WorkersState workersState = new WorkersState(screens[WORKERS]);
            ReviewState reviewState = new ReviewState(screens[REVIEW]);

            // Set state transitions
            stateMachine.At(ideationState, topicState, new FuncPredicate(() => state == TOPIC));

            stateMachine.At(topicState, ideationState, new FuncPredicate(() => state == IDEATION));
            stateMachine.At(topicState, genreState, new FuncPredicate(() => state == GENRE));

            stateMachine.At(genreState, topicState, new FuncPredicate(() => state == TOPIC));
            stateMachine.At(genreState, workersState, new FuncPredicate(() => state == WORKERS));

            stateMachine.At(workersState, genreState, new FuncPredicate(() => state == GENRE));
            stateMachine.At(workersState, reviewState, new FuncPredicate(() => state == REVIEW));

            stateMachine.At(reviewState, workersState, new FuncPredicate(() => state == WORKERS));
            stateMachine.At(reviewState, ideationState, new FuncPredicate(() => state == IDEATION));

            // Set the initial state
            stateMachine.SetState(ideationState);

            // Set variables
            originalPosition = window.transform.localPosition;
        }

        private void OnEnable()
        {
            openWorkMenuEvent = new EventBinding<OpenCreateWorkMenu>(OpenWorkMenu);
            EventBus<OpenCreateWorkMenu>.Register(openWorkMenuEvent);

            closeWorkMenuEvent = new EventBinding<CloseCreateWorkMenu>(CloseWorkMenu);
            EventBus<CloseCreateWorkMenu>.Register(closeWorkMenuEvent);

            notifySuccessfulCreationEvent = new EventBinding<NotifySuccessfulCreation>(HandleSuccessfulCreation);
            EventBus<NotifySuccessfulCreation>.Register(notifySuccessfulCreationEvent);

            endDevelopmentEvent = new EventBinding<EndDevelopment>(HandleDevelopmentEnd);
            EventBus<EndDevelopment>.Register(endDevelopmentEvent);
        }

        private void OnDisable()
        {
            EventBus<OpenCreateWorkMenu>.Deregister(openWorkMenuEvent);
            EventBus<CloseCreateWorkMenu>.Deregister(closeWorkMenuEvent);
            EventBus<NotifySuccessfulCreation>.Deregister(notifySuccessfulCreationEvent);
            EventBus<EndDevelopment>.Deregister(endDevelopmentEvent);
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

        private void FixedUpdate()
        {
            // Update the state machine
            stateMachine.FixedUpdate();
        }

        /// <summary>
        /// Set the state of Game Creation
        /// </summary>
        public void SetState(int state)
        {
            this.state = state;
        }
        
        /// <summary>
        /// Callback for opening the Work menu
        /// </summary>
        private void OpenWorkMenu()
        {
            // Close the interact menus
            EventBus<CloseInteractMenus>.Raise(new CloseInteractMenus());

            // Pause the Calendar
            EventBus<ChangeCalendarPauseState>.Raise(new ChangeCalendarPauseState()
            {
                Paused = true,
                AllowSpeedChanges = false
            });

            // Don't allow the player to interact with outside objects
            EventBus<SetCanInteract>.Raise(new SetCanInteract()
            {
                CanInteract = false
            });

            // Show the window
            ShowWindow();
        }

        /// <summary>
        /// Callback for closing the Work menu
        /// </summary>
        public void CloseWorkMenu()
        {
            // Hide the window
            HideWindow();

            // Ensure the calendar is unpaused
            EventBus<ChangeCalendarPauseState>.Raise(new ChangeCalendarPauseState()
            {
                Paused = false,
                AllowSpeedChanges = true
            });

            // Allow the player to interact with outside objects
            EventBus<SetCanInteract>.Raise(new SetCanInteract()
            {
                CanInteract = true
            });
        }

        /// <summary>
        /// Callback for handling a successfully beginning the creation of a Work
        /// </summary>
        private void HandleSuccessfulCreation(NotifySuccessfulCreation eventData)
        {
            // Hide the window
            HideWindowSuccess();

            // Set the state to ideation
            state = IDEATION;
        }

        /// <summary>
        /// Callback for handling the end of development
        /// </summary>
        private void HandleDevelopmentEnd(EndDevelopment eventData)
        {
            // Set the state to ideation
            state = IDEATION;
        }

        /// <summary>
        /// Show the Work window
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
            Fade(1f, duration, () => {
                window.interactable = true;
                window.blocksRaycasts = true;
            });

            // Translate down
            Translate(-translateValue, duration);

            // Set the window to open
            Open = true;
        }

        /// <summary>
        /// Hide the Work window
        /// </summary>
        private void HideWindow()
        {
            // Fade out
            Fade(0f, duration, () => {
                window.interactable = false;
                window.blocksRaycasts = false;
            });

            // Translate down
            Translate(-translateValue, duration);

            // Set the window to closed
            Open = false;
        }

        /// <summary>
        /// Hide the window with a little more flair for successful Work creation
        /// </summary>
        private void HideWindowSuccess()
        {
            // Translate up
            Translate(
                translateValue * (3f/4f), 
                duration / 3f,
                () =>
                    {
                        // When done, fade out
                        Fade(0f, duration, () =>
                        {
                            window.interactable = false;
                            window.blocksRaycasts = false;
                        }, Ease.OutCirc);

                        // And translate downwards
                        Translate(-translateValue * 2f, duration, null, Ease.OutCirc);
                    }, 
                Ease.OutBack
            );

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