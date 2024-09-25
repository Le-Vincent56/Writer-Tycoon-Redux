using DG.Tweening;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Patterns.StateMachine;
using WriterTycoon.WorkCreation.Development.FocusSliders;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.WorkCreation.Development.UI.States;

namespace WriterTycoon.WorkCreation.UI.Development
{
    public class FocusSliderWindow : MonoBehaviour
    {
        [SerializeField] private CanvasGroup window;
        [SerializeField] private int currentHash;
        [SerializeField] private DevelopmentPhase currentPhase;
        [SerializeField] private CanvasGroup[] screens = new CanvasGroup[3];
        [SerializeField] private FocusSlider[] focusSliders = new FocusSlider[9];
        [SerializeField] private ConfirmSlidersButton[] confirmButtons = new ConfirmSlidersButton[3];

        [SerializeField] private float translateValue;
        [SerializeField] private float duration;
        private Vector3 originalPosition;
        private Tween fadeTween;
        private Tween translateTween;

        private StateMachine stateMachine;

        private EventBinding<SetDevelopmentPhase> setDevelopmentPhaseEvent;
        private EventBinding<OpenSliderWindow> openSliderWindowEvent;
        private EventBinding<CloseSliderWindow> closeSliderWindowEvent;

        private void Awake()
        {
            // Set the ideation state
            currentPhase = DevelopmentPhase.PhaseOne;

            // Initialize the state machine
            stateMachine = new StateMachine();

            // Construct states
            PhaseOneState phaseOneState = new(screens[0]);
            PhaseTwoState phaseTwoState = new(screens[1]);
            PhaseThreeState phaseThreeState = new(screens[2]);

            // Set state transitions
            stateMachine.At(phaseOneState, phaseTwoState, new FuncPredicate(() => currentPhase == DevelopmentPhase.PhaseTwo));
            stateMachine.At(phaseTwoState, phaseThreeState, new FuncPredicate(() => currentPhase == DevelopmentPhase.PhaseThree));
            stateMachine.At(phaseThreeState, phaseOneState, new FuncPredicate(() => currentPhase == DevelopmentPhase.PhaseOne));

            // Set the initial state
            stateMachine.SetState(phaseOneState);

            // Iterate through each confirm button
            foreach(ConfirmSlidersButton button in confirmButtons)
            {
                // Initialize the button
                button.Initialize(this);
            }

            // Iterate through each Focus Slider
            foreach(FocusSlider slider in focusSliders)
            {
                // Initialize the slider
                slider.Initialize(this);
            }
        }

        private void OnEnable()
        {
            setDevelopmentPhaseEvent = new EventBinding<SetDevelopmentPhase>(SetSliderPhase);
            EventBus<SetDevelopmentPhase>.Register(setDevelopmentPhaseEvent);

            openSliderWindowEvent = new EventBinding<OpenSliderWindow>(OpenSliderWindow);
            EventBus<OpenSliderWindow>.Register(openSliderWindowEvent);

            closeSliderWindowEvent = new EventBinding<CloseSliderWindow>(CloseSliderWindow);
            EventBus<CloseSliderWindow>.Register(closeSliderWindowEvent);
        }

        private void OnDisable()
        {
            EventBus<OpenSliderWindow>.Deregister(openSliderWindowEvent);
            EventBus<CloseSliderWindow>.Deregister(closeSliderWindowEvent);
            EventBus<SetDevelopmentPhase>.Deregister(setDevelopmentPhaseEvent);
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
        /// Get the current hash
        /// </summary>
        public int GetCurrentHash() => currentHash;

        /// <summary>
        /// Callback function to set the slider phase
        /// </summary>
        private void SetSliderPhase(SetDevelopmentPhase eventData)
        {
            currentHash = eventData.Hash;
            currentPhase = eventData.Phase;
        }

        /// <summary>
        /// Callback for opening the Focus Slider window
        /// </summary>
        /// <param name="eventData"></param>
        private void OpenSliderWindow(OpenSliderWindow eventData)
        {
            // Close the interact menus
            EventBus<CloseInteractMenus>.Raise(new CloseInteractMenus());

            // Ensure the calendar is paused
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
        /// Callback function to closing the Focus Slider window
        /// </summary>
        /// <param name="eventData"></param>
        private void CloseSliderWindow(CloseSliderWindow eventData)
        {
            // Hide the window with flair
            ConfirmChoiceClose();

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