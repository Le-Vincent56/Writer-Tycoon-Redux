using WriterTycoon.Patterns.StateMachine;

namespace WriterTycoon.Entities.Competitors.States
{
    public class CompetitorState : IState
    {
        protected readonly NPCCompetitor controller;

        public CompetitorState(NPCCompetitor controller)
        {
            this.controller = controller;
        }

        public virtual void OnEnter() { }

        public void Update() { }

        public void FixedUpdate() { }

        public void OnExit() { }
    }
}