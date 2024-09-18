using UnityEngine;
using WriterTycoon.Patterns.StateMachine;

namespace WriterTycoon.WorkCreation.Development.UI.States
{
    public class SliderState : IState
    {
        protected CanvasGroup canvasGroup;

        public SliderState(CanvasGroup canvasGroup)
        {
            this.canvasGroup = canvasGroup;
        }

        public virtual void OnEnter() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void OnExit() { }
    }
}