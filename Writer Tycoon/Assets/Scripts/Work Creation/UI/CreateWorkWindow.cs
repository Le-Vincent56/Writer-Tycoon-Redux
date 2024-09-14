using UnityEngine;
using DG.Tweening;
using WriterTycoon.WorkCreation.UI.States;
using WriterTycoon.Patterns.StateMachine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.UI
{
    public class CreateWorkWindow : MonoBehaviour
    {
        private bool canOpenWindow;

        private StateMachine stateMachine;
        [SerializeField] private CanvasGroup window;
        [SerializeField] private int state;
        [SerializeField] private CanvasGroup[] screens = new CanvasGroup[4];

        [SerializeField] private float translateValue;
        [SerializeField] private float duration;

        private EventBinding<OpenCreateWorkMenu> openWorkMenuEvent;
        private EventBinding<NotifySuccessfulCreation> notifySuccessfulCreationEvent;

        private Vector3 originalPosition;
        private Tween fadeTween;
        private Tween translateTween;

        public int IDEATION { get => 0; }
        public int TOPIC { get => 1; }
        public int GENRE { get => 2; }
        public int REVIEW { get => 3; }

        private void OnEnable()
        {
            openWorkMenuEvent = new EventBinding<OpenCreateWorkMenu>(HandleWorkMenu);
            EventBus<OpenCreateWorkMenu>.Register(openWorkMenuEvent);

            notifySuccessfulCreationEvent = new EventBinding<NotifySuccessfulCreation>(HandleSuccessfulCreation);
            EventBus<NotifySuccessfulCreation>.Register(notifySuccessfulCreationEvent);
        }

        private void OnDisable()
        {
            EventBus<OpenCreateWorkMenu>.Deregister(openWorkMenuEvent);
            EventBus<NotifySuccessfulCreation>.Deregister(notifySuccessfulCreationEvent);
        }

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
            ReviewState reviewState = new ReviewState(screens[REVIEW]);

            // Set state transitions
            stateMachine.At(ideationState, topicState, new FuncPredicate(() => state == TOPIC));

            stateMachine.At(topicState, ideationState, new FuncPredicate(() => state == IDEATION));
            stateMachine.At(topicState, genreState, new FuncPredicate(() => state == GENRE));

            stateMachine.At(genreState, topicState, new FuncPredicate(() => state == TOPIC));
            stateMachine.At(genreState, reviewState, new FuncPredicate(() => state == REVIEW));

            stateMachine.At(reviewState, genreState, new FuncPredicate(() => state == GENRE));
            stateMachine.At(reviewState, ideationState, new FuncPredicate(() => state == IDEATION));

            // Set the initial state
            stateMachine.SetState(ideationState);

            // Set variables
            originalPosition = window.transform.localPosition;
            canOpenWindow = true;
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
        /// Callback for handling the Work menu
        /// </summary>
        private void HandleWorkMenu(OpenCreateWorkMenu eventData)
        {
            if (eventData.IsOpening)
                ShowWindow();
            else
                HideWindow();
        }

        /// <summary>
        /// Callback for handling a successfully beginning the creation of a Work
        /// </summary>
        private void HandleSuccessfulCreation(NotifySuccessfulCreation eventData)
        {
            // Hide the window
            HideWindowSuccess();

            // Don't allow the player to open the window again
            canOpenWindow = false;
        }

        /// <summary>
        /// Show the Work window
        /// </summary>
        private void ShowWindow()
        {
            // Exit case - if cannot open the window
            if (!canOpenWindow) return;

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
        /// <param name="endTranslateValue"></param>
        /// <param name="duration"></param>
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