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

        private Animator animator;
        private SpriteRenderer spriteRenderer;

        private StateMachine stateMachine;

        private Vector2 velocity;

        private void Awake()
        {
            // Get components
            animator = GetComponentInChildren<Animator>();
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
            stateMachine.Update();
        }

        private void FixedUpdate()
        {
            velocity.x = inputReader.NormMoveX;
            velocity.y = inputReader.NormMoveY;

            stateMachine.FixedUpdate();
        }
    }
}