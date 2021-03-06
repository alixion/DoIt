using System;

namespace DoIt.Domain.Common
{
    public abstract class DomainEventBase:IDomainEvent
    {
        public Guid Id { get; }

        public DateTimeOffset OccurredOn { get; }

        public DomainEventBase()
        {
            this.Id = Guid.NewGuid();
            this.OccurredOn = DateTimeOffset.UtcNow;
        }
    }
}