using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Entities.Player.States;
using WriterTycoon.Input;
using WriterTycoon.Patterns.StateMachine;

namespace WriterTycoon.Entities.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameInputReader inputReader;
        [SerializeField] private float moveSpeed;

        private Animator animator;
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;

        private StateMachine stateMachine;

        private Vector2 velocity;

        private void Awake()
        {
            // Get components
            animator = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

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

        private void Update()
        {
            // Update input
            velocity.x = inputReader.NormMoveX;
            velocity.y = inputReader.NormMoveY;

            // Set player velocity
            rb.velocity = velocity * moveSpeed;

            // Update the state machine
            stateMachine.Update();
        }

        private void FixedUpdate()
        {
            // Fixed update the state machine
            stateMachine.FixedUpdate();
        }
    }
}