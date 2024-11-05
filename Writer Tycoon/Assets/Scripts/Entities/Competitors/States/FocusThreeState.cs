namespace WriterTycoon.Entities.Competitors.States
{
    public class FocusThreeState : CompetitorState
    {
        public FocusThreeState(NPCCompetitor competitor) : base(competitor) { }

        public override void OnEnter()
        {
            // Start working
            competitor.SetDaysToWork();
            competitor.Learn(Learning.Problem.FocusThree);
        }

        public override void OnExit()
        {
            competitor.Rate();
        }
    }
}