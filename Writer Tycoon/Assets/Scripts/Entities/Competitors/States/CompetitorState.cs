using WriterTycoon.Patterns.StateMachine;

namespace WriterTycoon.Entities.Competitors.States
{
    public class CompetitorState : IState
    {
        protected readonly NPCCompetitor competitor;

        public CompetitorState(NPCCompetitor competitor)
        {
            this.competitor = competitor;
        }

        public virtual void OnEnter() { }

        public void Update() { }

        public void FixedUpdate() { }

        public void OnExit() { }
    }
}