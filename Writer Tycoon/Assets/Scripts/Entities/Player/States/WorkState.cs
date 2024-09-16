using UnityEngine;

namespace WriterTycoon.Entities.Player.States
{
    public class WorkState : PlayerState
    {
        public WorkState(PlayerController controller, SpriteRenderer renderer)
            : base(controller, renderer)
        { }

        public override void OnEnter()
        {
            renderer.color = Color.green;
        }
    }
}
