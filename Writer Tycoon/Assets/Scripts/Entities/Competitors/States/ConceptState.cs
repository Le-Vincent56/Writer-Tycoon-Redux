namespace WriterTycoon.Entities.Competitors.States
{
    public class ConceptState : CompetitorState
    {
        public ConceptState(NPCCompetitor competitor) : base(competitor) { }

        public override void OnEnter()
        {
            // Start working
            competitor.SetDaysToWork();
            competitor.Learn(Learning.Problem.Concept);
        }
    }
}
