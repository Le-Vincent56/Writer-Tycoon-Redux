using System.Collections.Generic;
using GhostWriter.Entities;
using GhostWriter.Patterns.Mediator;
using GhostWriter.WorkCreation.Ideation.Review;
using GhostWriter.WorkCreation.Ideation.TimeEstimation;

namespace GhostWriter.WorkCreation.Ideation.Topics
{
    public class WorkerPayload : Payload<List<IWorker>>
    {
        public override List<IWorker> Content { get; set; }

        public override void Visit<T>(T visitable)
        {
            // Verify types for mediation
            if (visitable is TimeEstimator timeEstimator) timeEstimator.SetWorkers(Content);
            if (visitable is IdeaReviewer ideaReviewer) ideaReviewer.SetWorkers(Content);
        }
    }
}