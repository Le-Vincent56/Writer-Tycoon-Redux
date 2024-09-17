using UnityEngine;
using WriterTycoon.Patterns.StateMachine;

namespace WriterTycoon.Entities.Player.States
{
    public abstract class PlayerState : IState
    {
        protected readonly PlayerController controller;
        protected readonly Animator animator;
        protected readonly SpriteRenderer renderer;

        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        protected static readonly int WorkHash = Animator.StringToHash("Work");

        protected const float crossFadeDuration = 0.1f;

        public PlayerState(PlayerController controller, /*Animator animator*/ SpriteRenderer renderer)
        {
            this.controller = controller;
            //this.animator = animator;
            this.renderer = renderer;
        }

        public virtual void OnEnter() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void OnExit() { }
    }
}