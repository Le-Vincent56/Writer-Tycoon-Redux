using UnityEngine;
using WriterTycoon.Entities.Player.States;
using WriterTycoon.Input;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Patterns.StateMachine;

namespace WriterTycoon.Entities.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameInputReader inputReader;
        [SerializeField] private bool canMove;
        [SerializeField] private float moveSpeed;
        [SerializeField] private bool working;

        private Animator animator;
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;

        private StateMachine stateMachine;

        private Vector2 velocity;

        private EventBinding<CalendarPauseStateChanged> pauseCalendarEvent;
        private EventBinding<ChangePlayerWorkState> changePlayerWorkStateEvent;

        private void Awake()
        {
            // Get components
            animator = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            // Set variables
            canMove = true;

            // Initialize the state machine
            stateMachine = new StateMachine();

            // Create states
            IdleState idleState = new(this, spriteRenderer);
            LocomotionState locomotionState = new(this, spriteRenderer);
            WorkState workState = new(this, spriteRenderer, rb);

            // Define state transitions
            stateMachine.At(idleState, locomotionState, new FuncPredicate(() => velocity != Vector2.zero));
            stateMachine.At(idleState, workState, new FuncPredicate(() => working));

            stateMachine.At(locomotionState, idleState, new FuncPredicate(() => velocity == Vector2.zero));
            stateMachine.At(locomotionState, workState, new FuncPredicate(() => working));

            stateMachine.At(workState, idleState, new FuncPredicate(() => !working && velocity == Vector2.zero));
            stateMachine.At(workState, locomotionState, new FuncPredicate(() => !working && velocity != Vector2.zero));

            // Set an initial state
            stateMachine.SetState(idleState);
        }

        private void OnEnable()
        {
            inputReader.Move += OnMove;

            pauseCalendarEvent = new EventBinding<CalendarPauseStateChanged>(HandleCalendarPause);
            EventBus<CalendarPauseStateChanged>.Register(pauseCalendarEvent);

            changePlayerWorkStateEvent = new EventBinding<ChangePlayerWorkState>(HandlePlayerWorkStateChange);
            EventBus<ChangePlayerWorkState>.Register(changePlayerWorkStateEvent);
        }

        private void OnDisable()
        {
            inputReader.Move -= OnMove;

            EventBus<CalendarPauseStateChanged>.Deregister(pauseCalendarEvent);
            EventBus<ChangePlayerWorkState>.Deregister(changePlayerWorkStateEvent);
        }

        private void Update()
        {
            // Check if the player can move
            if (canMove)
            {
                // Update input
                velocity.x = inputReader.NormMoveX;
                velocity.y = inputReader.NormMoveY;

                // Set player velocity
                rb.velocity = velocity * moveSpeed;
            } else
            {
                // Zero out the velocity
                rb.velocity = Vector2.zero;
            }

            // Update the state machine
            stateMachine.Update();
        }

        private void FixedUpdate()
        {
            // Fixed update the state machine
            stateMachine.FixedUpdate();
        }

        /// <summary>
        /// Callback handler for when the Calendar pause state has changed
        /// </summary>
        private void HandleCalendarPause(CalendarPauseStateChanged eventData)
        {
            // Don't allow the player to move if the Calendar is paused
            canMove = !eventData.Paused;
        }

        /// <summary>
        /// Callback handler for when the Player work state is to change
        /// </summary>
        private void HandlePlayerWorkStateChange(ChangePlayerWorkState eventData)
        {
            // Set the player to working
            working = eventData.Working;
        }

        /// <summary>
        /// Handle Player movement input
        /// </summary>
        private void OnMove(Vector2 movementVector, bool started)
        {
            // Check if the control has just been pressed and the Player is working
            if (started && working) 
                // If so, stop them from working
                working = false;
        }
    }
}