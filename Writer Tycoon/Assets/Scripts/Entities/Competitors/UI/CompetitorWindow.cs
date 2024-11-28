using DG.Tweening;
using GhostWriter.Entities.Competitors.UI.States;
using GhostWriter.Patterns.EventBus;
using GhostWriter.Patterns.ServiceLocator;
using GhostWriter.Patterns.StateMachine;
using GhostWriter.World.GeneralUI;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace GhostWriter.Entities.Competitors.UI
{
    public class CompetitorWindow : MonoBehaviour, IWindow
    {
        [SerializeField] private CompetitorList competitorList;
        private CanvasGroup canvasGroup;

        [Header("States")]
        [SerializeField] private int state;
        private StateMachine stateMachine;
        private const int LIST = 0;
        private const int HISTORY = 1;
        [SerializeField] private CanvasGroup[] states;

        private EventBinding<OpenCompetitorWindow> openCompetitorWindowEvent;
        private EventBinding<CloseCompetitorWindow> closeCompetitorWindowEvent;
        private EventBinding<SetCompetitorWindowState> setCompetitorWindowEvent;

        [Header("Tweening Variables")]
        [SerializeField] private float fadeDuration;
        private Tween fadeTween;

        public bool Open { get; set; }

        private void Awake()
        {
            // Get components
            competitorList = GetComponentInChildren<CompetitorList>();
            canvasGroup = GetComponent<CanvasGroup>();

            // Set the initial state
            state = LIST;

            // Initialize the State Machine
            InitializeStateMachine();
        }

        private void OnEnable()
        {
            openCompetitorWindowEvent = new EventBinding<OpenCompetitorWindow>(Show);
            EventBus<OpenCompetitorWindow>.Register(openCompetitorWindowEvent);

            closeCompetitorWindowEvent = new EventBinding<CloseCompetitorWindow>(Hide);
            EventBus<CloseCompetitorWindow>.Register(closeCompetitorWindowEvent);

            setCompetitorWindowEvent = new EventBinding<SetCompetitorWindowState>(SetState);
            EventBus<SetCompetitorWindowState>.Register(setCompetitorWindowEvent);
        }

        private void OnDisable()
        {
            EventBus<OpenCompetitorWindow>.Deregister(openCompetitorWindowEvent);
            EventBus<CloseCompetitorWindow>.Deregister(closeCompetitorWindowEvent);
            EventBus<SetCompetitorWindowState>.Deregister(setCompetitorWindowEvent);
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
            // Update the State Machine
            stateMachine.Update();
        }

        /// <summary>
        /// Initialize the State Machine
        /// </summary>
        private void InitializeStateMachine()
        {
            // Initialize the State Machine
            stateMachine = new StateMachine();

            // Create states
            ListState listState = new ListState(this, states[LIST]);
            HistoryState historyState = new HistoryState(this, states[HISTORY]);

            // Define state transitions
            stateMachine.At(listState, historyState, new FuncPredicate(() => state == HISTORY));
            stateMachine.At(historyState, listState, new FuncPredicate(() => state == LIST));

            // Set the initial state
            stateMachine.SetState(listState);
        }

        /// <summary>
        /// Callback function to handle opening the Competitor Window
        /// </summary>
        private void Show()
        {
            // Fully update the Competitor List
            EventBus<UpdateCompetitorList>.Raise(new UpdateCompetitorList()
            {
                FullUpdate = true
            });

            // Fade in
            Fade(1f, fadeDuration, () =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });

            // Set to open
            Open = true;
        }

        /// <summary>
        /// Callback function to handle closing the Competitor Window
        /// </summary>
        private void Hide() 
        {
            // Fade out
            Fade(0f, fadeDuration, () =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;

                state = LIST;
            });

            // Set to closed
            Open = false;
        }

        /// <summary>
        /// Callback function to handle Competitor Window State setting
        /// </summary>
        public void SetState(SetCompetitorWindowState eventData) => state = eventData.State;

        /// <summary>
        /// Handle fading for the Competitor Window
        /// </summary>
        private void Fade(float endValue, float duration, TweenCallback onComplete = null)
        {
            // Kill the Fade Tween if it already exists
            fadeTween?.Kill();

            // Set the Fade Tween
            fadeTween = canvasGroup.DOFade(endValue, duration);

            // Exit case - no completion action was given
            if (onComplete == null) return;

            // Hook up completion actions
            fadeTween.onComplete += onComplete;
        }
    }
}
