using UnityEngine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.Entities.Player.States
{
    public class WorkState : PlayerState
    {
        public WorkState(PlayerController controller, SpriteRenderer renderer)
            : base(controller, renderer)
        {
        }

        public override void OnEnter()
        {
            renderer.color = Color.green;

            // Confirm the player is working
            EventBus<ConfirmPlayerWorkState>.Raise(new ConfirmPlayerWorkState()
            {
                Working = true
            });
        }

        public override void OnExit()
        {
            // Confirm the player is not working
            EventBus<ConfirmPlayerWorkState>.Raise(new ConfirmPlayerWorkState()
            {
                Working = false
            });
        }
    }
}
