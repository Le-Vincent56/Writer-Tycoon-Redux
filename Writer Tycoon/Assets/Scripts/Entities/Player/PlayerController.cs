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
        [SerializeField] private float moveSpeed;
        [SerializeField] private bool canMove;

        private Animator animator;
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;

        private StateMachine stateMachine;

        private Vector2 velocity;

        EventBinding<PauseCalendarEvent> pauseCalendarEvent;

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

            // Define state transitions
            stateMachine.At(idleState, locomotionState, new FuncPredicate(() => velocity != Vector2.zero));
            stateMachine.At(locomotionState, idleState, new FuncPredicate(() => velocity == Vector2.zero));

            // Set an initial state
            stateMachine.SetState(idleState);
        }

        private void OnEnable()
        {
            pauseCalendarEvent = new EventBinding<PauseCalendarEvent>(ToggleMove);
            EventBus<PauseCalendarEvent>.Register(pauseCalendarEvent);
        }

        private void OnDisable()
        {
            EventBus<PauseCalendarEvent>.Deregister(pauseCalendarEvent);
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
        /// Toggle whether or not the player can or cannot move
        /// </summary>
        private void ToggleMove(PauseCalendarEvent pauseCalendarEvent)
        {
            canMove = !pauseCalendarEvent.Paused;
        }
    }
}