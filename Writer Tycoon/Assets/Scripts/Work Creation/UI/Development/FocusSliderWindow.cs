using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Patterns.StateMachine;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.Development.UI.States;

namespace WriterTycoon.WorkCreation.Development.FocusSliders
{
    public class FocusSliderWindow : MonoBehaviour
    {
        [SerializeField] private CanvasGroup window;
        [SerializeField] private DevelopmentPhase phase;
        [SerializeField] private CanvasGroup[] screens = new CanvasGroup[4];

        [SerializeField] private float translateValue;
        [SerializeField] private float duration;
        private Vector3 originalPosition;
        private Tween fadeTween;
        private Tween translateTween;

        private StateMachine stateMachine;

        private EventBinding<SetPhaseSlider> showPhaseSliderEvent;
        private EventBinding<HandleSliderWindow> handleSliderWindowEvent;
        private EventBinding<CloseSliderWindow> closeSliderWindowEvent;

        private void Awake()
        {
            // Set the ideation state
            phase = DevelopmentPhase.PhaseOne;

            // Initialize the state machine
            stateMachine = new StateMachine();

            // Construct states
            PhaseOneState phaseOneState = new(screens[0]);
            PhaseTwoState phaseTwoState = new(screens[1]);
            PhaseThreeState phaseThreeState = new(screens[2]);

            // Set state transitions
            stateMachine.At(phaseOneState, phaseTwoState, new FuncPredicate(() => phase == DevelopmentPhase.PhaseTwo));
            stateMachine.At(phaseTwoState, phaseThreeState, new FuncPredicate(() => phase == DevelopmentPhase.PhaseThree));
            stateMachine.At(phaseThreeState, phaseOneState, new FuncPredicate(() => phase == DevelopmentPhase.PhaseOne));

            // Set the initial state
            stateMachine.SetState(phaseOneState);
        }

        private void OnEnable()
        {
            showPhaseSliderEvent = new EventBinding<SetPhaseSlider>(SetPhaseState);
            EventBus<SetPhaseSlider>.Register(showPhaseSliderEvent);

            handleSliderWindowEvent = new EventBinding<HandleSliderWindow>(HandleSliderWindow);
            EventBus<HandleSliderWindow>.Register(handleSliderWindowEvent);

            closeSliderWindowEvent = new EventBinding<CloseSliderWindow>(CloseSliderWindow);
            EventBus<CloseSliderWindow>.Register(closeSliderWindowEvent);
        }

        private void OnDisable()
        {
            EventBus<SetPhaseSlider>.Deregister(showPhaseSliderEvent);
            EventBus<HandleSliderWindow>.Deregister(handleSliderWindowEvent);
            EventBus<CloseSliderWindow>.Deregister(closeSliderWindowEvent);
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
        /// Set the phase state
        /// </summary>
        /// <param name="eventData"></param>
        private void SetPhaseState(SetPhaseSlider eventData)
        {
            phase = eventData.Phase;
        }

        /// <summary>
        /// Callback for handling the Slider window
        /// </summary>
        /// <param name="eventData"></param>
        private void HandleSliderWindow(HandleSliderWindow eventData)
        {
            if (eventData.IsOpening)
                ShowWindow();
            else
                HideWindow();
        }

        private void CloseSliderWindow(CloseSliderWindow eventData)
        {
            // Hide the window with flair
            ConfirmChoiceClose();
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
        /// Hide the window with a little more flair for successful Work creation
        /// </summary>
        private void ConfirmChoiceClose()
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