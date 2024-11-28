using System;

namespace GhostWriter.Entities.Competitors.States
{
    public class ConceptState : CompetitorState
    {
        public ConceptState(NPCCompetitor competitor) : base(competitor) { }

        public override void OnEnter()
        {
            // Start working
            competitor.SetDaysToWork(UnityEngine.Random.Range(5, 11));
            competitor.Learn(Learning.Problem.Concept);
        }
    }
}
