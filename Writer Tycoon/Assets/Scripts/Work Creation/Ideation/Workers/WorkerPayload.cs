using System.Collections.Generic;
using WriterTycoon.Entities;
using WriterTycoon.Patterns.Mediator;
using WriterTycoon.WorkCreation.Ideation.Review;
using WriterTycoon.WorkCreation.Ideation.TimeEstimation;

namespace WriterTycoon.WorkCreation.Ideation.Topics
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