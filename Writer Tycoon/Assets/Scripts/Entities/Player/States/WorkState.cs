using UnityEngine;

namespace WriterTycoon.Entities.Player.States
{
    public class WorkState : PlayerState
    {
        private Rigidbody2D rb;

        public WorkState(PlayerController controller, SpriteRenderer renderer, Rigidbody2D rb)
            : base(controller, renderer)
        {
            this.rb = rb;
        }

        public override void OnEnter()
        {
            renderer.color = Color.green;
        }

        public override void Update()
        {
            // Zero out Player velocity
            rb.velocity = Vector2.zero;
        }
    }
}
