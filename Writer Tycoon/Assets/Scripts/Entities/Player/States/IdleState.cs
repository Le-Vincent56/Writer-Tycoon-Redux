using UnityEngine;

namespace GhostWriter.Entities.Player.States
{
    public class IdleState : PlayerState
    {
        public IdleState(PlayerController controller, SpriteRenderer renderer) 
            : base(controller, renderer) 
        {

        }

        public override void OnEnter()
        {
            renderer.color = Color.white;
        }
    }
}