using System;

namespace DoIt.Domain.Common
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        public DateTimeOffset OccurredOn { get; }
    }
}