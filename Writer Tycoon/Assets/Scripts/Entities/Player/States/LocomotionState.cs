using UnityEngine;

namespace GhostWriter.Entities.Player.States
{
    public class LocomotionState : PlayerState
    {
        public LocomotionState(PlayerController controller, SpriteRenderer renderer) 
            : base(controller, renderer) 
        {

        }

        public override void OnEnter()
        {
            renderer.color = Color.red;
        }
    }
}