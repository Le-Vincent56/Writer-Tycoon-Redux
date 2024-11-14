namespace GhostWriter.Entities.Competitors.States
{
    public class FocusTwoState : CompetitorState
    {
        public FocusTwoState(NPCCompetitor competitor) : base(competitor) { }

        public override void OnEnter()
        {
            // Start working
            competitor.SetDaysToWork();
            competitor.Learn(Learning.Problem.FocusTwo);
        }
    }
}
