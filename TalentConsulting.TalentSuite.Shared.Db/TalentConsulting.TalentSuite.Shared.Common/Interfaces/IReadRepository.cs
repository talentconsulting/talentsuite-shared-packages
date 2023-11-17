using Ardalis.Specification;

namespace  TalentConsulting.TalentSuite.Shared.Common.Interfaces;


public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
