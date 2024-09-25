using System.Collections.Generic;
using WriterTycoon.Entities;

namespace WriterTycoon.Patterns.EventBus
{
    public struct AvailableWorkersUpdated : IEvent
    {
        public List<IWorker> AvailableWorkers;
    }
}
