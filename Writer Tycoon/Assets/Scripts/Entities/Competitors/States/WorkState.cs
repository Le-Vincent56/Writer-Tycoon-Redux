namespace WriterTycoon.Entities.Competitors.States
{
    public class WorkState : CompetitorState
    {
        public WorkState(NPCCompetitor competitor) : base(competitor) { }

        public override void OnEnter()
        {
            // Start working
            competitor.StartWorking();
        }
    }
}
