using UnityEngine;
using WriterTycoon.WorkCreation.UI.States;
using WriterTycoon.Patterns.StateMachine;

namespace WriterTycoon.WorkCreation.UI
{
    public class CreateGameWindow : MonoBehaviour
    {
        private StateMachine stateMachine;
        [SerializeField] private int state;
        [SerializeField] private GameObject[] screens = new GameObject[2];

        public int IDEATION { get => 0; }
        public int TOPIC { get => 1; }
        public int GENRE { get => 2; }

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
    }
}