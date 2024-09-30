using UnityEngine;

namespace WriterTycoon.WorkCreation.UI.Development.States
{
    public class ProgressPolishState : ProgressState
    {
        private ProgressCard progressCard;

        public ProgressPolishState(ProgressCard progressCard, CanvasGroup canvasGroup) : base(canvasGroup) 
        {
            this.progressCard = progressCard;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            // Set the canvas group settings to be interactable and to block raycasts
            progressCard.SetCanvasGroupSettings(true, true);
        }
    }
}