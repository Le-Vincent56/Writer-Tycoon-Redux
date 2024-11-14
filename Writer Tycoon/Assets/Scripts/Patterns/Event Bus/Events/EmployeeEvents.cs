using System.Collections.Generic;
using GhostWriter.Entities;

namespace GhostWriter.Patterns.EventBus
{
    public struct AvailableWorkersUpdated : IEvent
    {
        public List<IWorker> AvailableWorkers;
    }
}
