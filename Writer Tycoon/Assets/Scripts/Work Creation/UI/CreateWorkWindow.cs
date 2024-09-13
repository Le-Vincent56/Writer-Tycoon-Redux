using UnityEngine;
using DG.Tweening;
using WriterTycoon.WorkCreation.UI.States;
using WriterTycoon.Patterns.StateMachine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.WorkCreation.UI
{
    public class CreateWorkWindow : MonoBehaviour
    {
        private StateMachine stateMachine;
        [SerializeField] private CanvasGroup window;
        [SerializeField] private int state;
        [SerializeField] private CanvasGroup[] screens = new CanvasGroup[3];

        [SerializeField] private float translateValue;
        [SerializeField] private float duration;

        EventBinding<OpenWorkMenuEvent> openWorkMenuEvent;

        private Vector3 originalPosition;
        private Tween fadeTween;
        private Tween translateTween;

        public int IDEATION { get => 0; }
        public int TOPIC { get => 1; }
        public int GENRE { get => 2; }

        private void OnEnable()
        {
            openWorkMenuEvent = new EventBinding<OpenWorkMenuEvent>(HandleWorkMenu);
            EventBus<OpenWorkMenuEvent>.Register(openWorkMenuEvent);
        }

        private void OnDisable()
        {
            EventBus<OpenWorkMenuEvent>.Deregister(openWorkMenuEvent);
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

            // Set state transitions
            stateMachine.At(ideationState, topicState, new FuncPredicate(() => state == TOPIC));

            stateMachine.At(topicState, ideationState, new FuncPredicate(() => state == IDEATION));
            stateMachine.At(topicState, genreState, new FuncPredicate(() => state == GENRE));

            stateMachine.At(genreState, topicState, new FuncPredicate(() => state == TOPIC));

            // Set the initial state
            stateMachine.SetState(ideationState);

            // Set variables
            originalPosition = window.transform.localPosition;
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
        public void SetState(int state) => this.state = state;
        
        /// <summary>
        /// Callback for handling the Work menu
        /// </summary>
        public void HandleWorkMenu(OpenWorkMenuEvent oenWorkMenuEvent)
        {
            if (oenWorkMenuEvent.IsOpening)
                ShowWindow();
            else
                HideWindow();
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
        /// Handle fading for the Window
        /// </summary>
        /// <param name="endFadeValue"></param>
        /// <param name="duration"></param>
        /// <param name="onEnd"></param>
        private void Fade(float endFadeValue, float duration, TweenCallback onEnd)
        {
            // Kill the current fade tween if it exists
            fadeTween?.Kill(false);

            // Set the fade animation
            fadeTween = window.DOFade(endFadeValue, duration)
                .SetEase(Ease.OutQuint);

            // Hook up callback events
            fadeTween.onComplete += onEnd;
        }

        /// <summary>
        /// Handle translating for the Window
        /// </summary>
        /// <param name="endTranslateValue"></param>
        /// <param name="duration"></param>
        private void Translate(float endTranslateValue, float duration)
        {
            // Kill the current translate tween if it exists
            translateTween?.Kill(false);

            // Calculate the target position
            float targetPos = window.transform.localPosition.y + endTranslateValue;

            // Set the tween animation
            translateTween = window.transform.DOLocalMoveY(targetPos, duration)
                .SetEase(Ease.OutQuint);
        }
    }
}