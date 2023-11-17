namespace  TalentConsulting.TalentSuite.Shared.Common.Interfaces;

public interface IHandle<in T> where T : DomainEventBase
{
    Task HandleAsync(T args);
}