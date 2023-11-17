using Ardalis.Specification;

namespace  TalentConsulting.TalentSuite.Shared.Common.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}

