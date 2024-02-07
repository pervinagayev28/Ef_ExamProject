using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatAppModelsLibrary.Models.BaseModels;

namespace Whatsapp.Repositories.BaseRepositories
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : class, IPrimaryKey<TKey>
    {
        //Task<IQueryable<TEntity>> GetAll();
        IQueryable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllListAsync();
        ValueTask<TEntity> Get(TKey Id);

        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Commit();

    }
}
