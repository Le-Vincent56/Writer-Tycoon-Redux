using System.Collections.Generic;
using WriterTycoon.Entities;
using WriterTycoon.WorkCreation.Publication;

namespace WriterTycoon.Patterns.EventBus
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
