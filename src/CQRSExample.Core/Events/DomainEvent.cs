using System;

namespace CQRSExample.Core.Events
{
    public class GenericEventWrapper
    {
        public Guid DomainEventId { get; set; }
        public string EventOperationCode { get; set; }
        public DateTime Timestamp { get; set; }
        public string AggregateId { get; set; }
        public DomainEvent Data { get; set; }
    }

    public class DomainEvent {

    }
}
