using MediatR;

namespace  TalentConsulting.TalentSuite.Shared.Common;

public abstract class DomainEventBase : INotification
{
    public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
}

