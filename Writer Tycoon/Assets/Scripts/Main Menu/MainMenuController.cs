using UnityEngine;
using WriterTycoon.MainMenu.States;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Patterns.StateMachine;

namespace WriterTycoon.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        private StateMachine stateMachine;

        [SerializeField] private bool active = false;
        [SerializeField] private CanvasGroup[] screens;

        private int state = 0;

        private EventBinding<ActivateMainMenu> activateMainMenuEvent;

        public int Initial { get => 0; }
        public int NewGame { get => 1; }
        public int LoadGame { get => 2; }

        private void OnEnable()
        {
            activateMainMenuEvent = new EventBinding<ActivateMainMenu>(Activate);
            EventBus<ActivateMainMenu>.Register(activateMainMenuEvent);
        }

        private void OnDisable()
        {
            EventBus<ActivateMainMenu>.Deregister(activateMainMenuEvent);
        }

        private void Update()
        {
            // Exit case - not active
            if (!active) return;

            // Update the State Machine
            stateMachine.Update();
        }

        private void SetupStateMachine(Animator animator)
        {
            // Initialize the State Machine
            stateMachine = new StateMachine();

            // Create states
            InitialState initial = new InitialState(this, animator, screens[Initial]);
            NewGameState newGame = new NewGameState(this, animator, screens[NewGame]);
            LoadGameState loadGame = new LoadGameState(this, animator, screens[LoadGame]);

            // Define state transitions
            stateMachine.At(initial, newGame, new FuncPredicate(() => state == NewGame));
            stateMachine.At(initial, loadGame, new FuncPredicate(() => state == LoadGame));

            stateMachine.At(newGame, initial, new FuncPredicate(() => state == Initial));

            stateMachine.At(loadGame, initial, new FuncPredicate(() => state == Initial));

            // Set the initial state
            stateMachine.SetState(initial);
        }

        /// <summary>
        /// Activate the Main Menu Controller
        /// </summary>
        public void Activate(ActivateMainMenu eventData)
        {
            // Set up the State Machine
            SetupStateMachine(eventData.Animator);

            // Set initial variables
            state = 0;
            active = true;
        }

        /// <summary>
        /// Set the Main Menu Controller's state
        /// </summary>
        public void SetState(int state) => this.state = state;
    }
}