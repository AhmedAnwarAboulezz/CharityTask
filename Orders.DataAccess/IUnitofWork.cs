using Common.StandardInfrastructure.Interface;
using Common.StandardInfrastructure.Repository;
using System;
using System.Threading.Tasks;

namespace Orders.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryAsync<T> GetRepository<T>() where T : class, IBaseEntityModel;
        Task<int> SaveChangesAsync();
    }
}