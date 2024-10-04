using System.Collections.Generic;
using WriterTycoon.WorkCreation.Publication;

namespace WriterTycoon.Patterns.EventBus
{
    public struct SellWork : IEvent
    {
        public PublishedWork WorkToSell;
    }

    public struct CreateSalesGraph : IEvent
    {
        public PublishedWork WorkToGraph;
    }

    public struct DestroySalesGraph : IEvent
    {
        public int Hash;
    }
}
