using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {       
         Task<IQueryable<T>> FindAll();
         Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression);
         Task<T> Create(T entity);
         Task<T> Update(T entity);
         Task<bool> Delete(T entity);
        
    }
}
