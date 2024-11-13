using UnityEngine;

namespace WriterTycoon.MainMenu.States
{
    public class NewGameState : MenuState
    {
        public NewGameState(MainMenuController controller, Animator animator, CanvasGroup stateGroup)
            : base(controller, animator, stateGroup)
        { }

        public override void OnEnter()
        {
            animator.CrossFade(ForwardHash, 0f);

            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();

            animator.CrossFade(BackwardHash, 0f);
        }
    }
}
