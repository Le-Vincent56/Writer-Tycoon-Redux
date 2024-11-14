using UnityEngine;
using GhostWriter.Patterns.EventBus;

namespace GhostWriter.Entities.Player.States
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
