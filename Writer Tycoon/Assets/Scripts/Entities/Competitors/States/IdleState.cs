namespace WriterTycoon.Entities.Competitors.States
{
    public class IdleState : CompetitorState
    {
        public IdleState(NPCCompetitor competitor) : base(competitor) { }

        public override void OnEnter()
        {
            // Set the amount of days to idle
            competitor.SetDaysToIdle();
        }
    }
}