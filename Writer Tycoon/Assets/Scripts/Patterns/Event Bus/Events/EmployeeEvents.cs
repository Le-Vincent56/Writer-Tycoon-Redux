using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Entities;
using WriterTycoon.Entities.Player;

namespace WriterTycoon.Patterns.EventBus
{
    public struct AvailableWorkersUpdated : IEvent
    {
        public List<IWorker> AvailableWorkers;
    }
}
