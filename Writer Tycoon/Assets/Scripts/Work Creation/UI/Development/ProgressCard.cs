using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Patterns.StateMachine;
using WriterTycoon.WorkCreation.Mediation;
using WriterTycoon.WorkCreation.UI.Development.States;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public enum ProgressStage
    {
        Development,
        Error,
        Polish
    }

    public class ProgressCard : MonoBehaviour
    {
        [SerializeField] private ProgressStage currentStage;
        [SerializeField] private ProgressTitle progressTitle;
        [SerializeField] private ProgressBar progressBar;

        [SerializeField] private CanvasGroup[] canvasGroups;
        [SerializeField] private float translateValue;
        [SerializeField] private float animateDuration;
        private RectTransform rectTransform;
        private LayoutElement layoutElement;
        private CanvasGroup card;
        private Vector3 originalPosition;
        private Tween fadeTween;
        private Tween translateTween;

        private StateMachine stateMachine;

        private EventBinding<EndEditing> endEditingEvent;

        private void OnEnable()
        {
            endEditingEvent = new EventBinding<EndEditing>(Hide);
            EventBus<EndEditing>.Register(endEditingEvent);
        }

        private void OnDisable()
        {
            EventBus<EndEditing>.Deregister(endEditingEvent);
        }

        private void Update()
        {
            // Update the state machine
            stateMachine.Update();
        }

        private void FixedUpdate()
        {
            // Fixed update the state machine
            stateMachine.FixedUpdate();
        }

        public void Initialize(string title)
        {
            // Verify the Progress Title
            if (progressTitle == null)
                progressTitle = GetComponentInChildren<ProgressTitle>();

            // Verify the Rect Transform
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            // Verify the Layout Element
            if (layoutElement == null)
                layoutElement = GetComponent<LayoutElement>();

            // Verify the Canvas Group
            if (card == null)
                card = GetComponent<CanvasGroup>();

            // Set variables
            originalPosition = rectTransform.localPosition;
            currentStage = ProgressStage.Development;

            // Initialize the Progress Title
            progressTitle.Initialize(title);

            InitializeStateMachine();
        }

        /// <summary>
        /// Initialize the state machine for the Progress Card
        /// </summary>
        private void InitializeStateMachine()
        {
            // Initialize the state machine
            stateMachine = new StateMachine();

            // Create the states
            ProgressDevelopmentState developmentState = new(canvasGroups[0]);
            ProgressErrorState errorState = new(canvasGroups[1]);
            ProgressPolishState polishState = new(canvasGroups[2]);

            // Define state transitions
            stateMachine.At(developmentState, errorState, new FuncPredicate(() => currentStage == ProgressStage.Error));
            stateMachine.At(errorState, polishState, new FuncPredicate(() => currentStage == ProgressStage.Polish));

            // Set an initial state
            stateMachine.SetState(developmentState);
        }

        /// <summary>
        /// Show the Progress Bar
        /// </summary>
        public void Show()
        {
            // Ignore the layout
            layoutElement.ignoreLayout = true;

            // Set the window's initial position to be below
            Vector3 startPos = new Vector3(
                originalPosition.x,
                originalPosition.y - translateValue,
                originalPosition.z
            );
            rectTransform.localPosition = startPos;

            // Fade in
            Fade(1f, animateDuration);

            // Translate up
            Translate(translateValue, animateDuration, () => layoutElement.ignoreLayout = false);
        }

        /// <summary>
        /// Hide the Progress Bar
        /// </summary>
        public void Hide(bool destroy = false)
        {
            // Ignore the layout
            layoutElement.ignoreLayout = true;

            // Fade out
            Fade(0f, animateDuration, null, Ease.OutQuint);

            // Translate down
            Translate(-translateValue, animateDuration, () => {
                // Ignore the layout
                layoutElement.ignoreLayout = false;

                // Check whether or not to destroy the object
                if (destroy)
                    // If so, destroy the object
                    Destroy(this);
            }, Ease.OutQuint
            );
        }

        /// <summary>
        /// Handle fading for the Progress Bar
        /// </summary>
        private void Fade(float endFadeValue, float duration, TweenCallback onEnd = null, Ease easeType = Ease.InQuint)
        {
            // Kill the current fade tween if it exists
            fadeTween?.Kill(false);

            // Set the fade animation
            fadeTween = card.DOFade(endFadeValue, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onEnd == null) return;

            // Hook up callback events
            fadeTween.onComplete += onEnd;
        }

        /// <summary>
        /// Handle translating for the Window
        /// </summary>
        private void Translate(float endTranslateValue, float duration, TweenCallback onEnd = null, Ease easeType = Ease.InQuint)
        {
            // Kill the current translate tween if it exists
            translateTween?.Kill(false);

            // Calculate the target position
            float targetPos = rectTransform.localPosition.y + endTranslateValue;

            // Set the tween animation
            translateTween = rectTransform.DOLocalMoveY(targetPos, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onEnd == null) return;

            // Hook up callback events
            translateTween.onComplete += onEnd;
        }
    }
}