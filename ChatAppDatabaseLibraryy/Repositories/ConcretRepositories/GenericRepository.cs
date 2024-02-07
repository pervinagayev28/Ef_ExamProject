using ChatAppDatabaseLibraryy.Contexts;
using ChatAppModelsLibrary.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whatsapp.Repositories.BaseRepositories;

namespace Whatsapp.Repositories.ConcretRepositories
{
    internal class GenericRepository<TEntity, TKey>
                     : IGenericRepository<TEntity, TKey>
                           where TEntity : class, IPrimaryKey<TKey>

    {

        private readonly DbContext context;
        private readonly DbSet<TEntity> set;
        public GenericRepository(DbContext context)
        {
            this.context = context;
            set = context.Set<TEntity>();

        }
        //Crud Functionally
        public async Task<IEnumerable<TEntity>> GetAllListAsync() =>
            await set.ToListAsync();
        public IQueryable<TEntity> GetAll() =>
            set;

        public async Task Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            //context.Entry(entity).State = EntityState.Added;
            await set.AddAsync(entity);
        }

        public async Task Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Delete(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
            //context.Entry(entity).State  &= ~EntityState.Deleted;
            context.Entry(entity).State = EntityState.Deleted ;
        }

        public async ValueTask<TEntity> Get(TKey Id)=>
            await set.FindAsync(Id);
        

        //Commit 
        public async Task Commit() =>
            await context.SaveChangesAsync();

     
    }
}
