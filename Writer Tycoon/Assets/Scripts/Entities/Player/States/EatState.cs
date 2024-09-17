using UnityEngine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.Entities.Player.States
{
    public class EatState : PlayerState
    {
        public EatState(PlayerController controller, SpriteRenderer renderer)
            : base(controller, renderer)
        {
        }

        public override void OnEnter()
        {
            renderer.color = Color.yellow;

            // Confirm the player is eating
            EventBus<ConfirmPlayerEatState>.Raise(new ConfirmPlayerEatState()
            {
                Eating = true
            });
        }

        public override void OnExit()
        {
            // Confirm the player is not eating
            EventBus<ConfirmPlayerEatState>.Raise(new ConfirmPlayerEatState()
            {
                Eating = false
            });
        }
    }
}