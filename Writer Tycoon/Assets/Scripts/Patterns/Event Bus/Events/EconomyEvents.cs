using System.Collections.Generic;
using GhostWriter.Entities;
using GhostWriter.WorkCreation.Publication;

namespace GhostWriter.Patterns.EventBus
{
    public struct SellWork : IEvent
    {
        public PublishedWork WorkToSell;
    }

    public struct DisplayBank : IEvent
    {
        public ICompetitor Competitor;
        public float BankAmount;
        public float Revenue;
    }

    public struct CreateSalesGraph : IEvent
    {
        public PublishedWork WorkToGraph;
    }

    public struct UpdateSalesGraph : IEvent
    {
        public int Hash;
        public int Sales;
    }

    public struct StopSalesGraph : IEvent
    {
        public int Hash;
    }

    public struct DestroySalesGraph : IEvent
    {
        public int Hash;
    }
}
