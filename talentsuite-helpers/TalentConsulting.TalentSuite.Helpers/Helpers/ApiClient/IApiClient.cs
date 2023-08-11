using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentConsulting.TalentSuite.Helpers.Helpers
{
    public interface IApiClient<T> : IDisposable
    {
        Task<List<T>> GetAll();

        Task<T> Get(int? id);

        Task Create(T item);

        Task Update(int? id, T item);

        Task Delete(int id);
    }
}