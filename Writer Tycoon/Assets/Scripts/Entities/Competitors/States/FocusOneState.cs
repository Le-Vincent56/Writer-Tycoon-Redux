namespace GhostWriter.Entities.Competitors.States
{
    public class FocusOneState : CompetitorState
    {
        public FocusOneState(NPCCompetitor competitor) : base(competitor) { }

        public override void OnEnter()
        {
            // Start working
            competitor.SetDaysToWork();
            competitor.Learn(Learning.Problem.FocusOne);
        }
    }
}
