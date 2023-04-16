using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class,IEntity
    {
        protected RepositoryContext RepositoryContext { get; set; }
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public async Task<IQueryable<T>> FindAll()
        {
            return await Task.FromResult(RepositoryContext.Set<T>().AsNoTracking());
        }

        public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return await Task.FromResult(RepositoryContext.Set<T>().Where(expression).AsNoTracking());
        }

        public async Task<T> Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
            return await Task.FromResult(entity);
        }

        public async Task<T> Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
            return await Task.FromResult(entity);
        }

        public async Task<bool> Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
            return await Task.FromResult(true);
        }


    }
}