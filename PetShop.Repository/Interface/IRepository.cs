using PetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> include = null);
        T Get(Guid? id, Func<IQueryable<T>, IQueryable<T>> include = null);
        T Insert(T entity);
        T Update(T entity);
        T Delete(Guid? id);
        IEnumerable<T> FindBy(Func<T, bool> predicate);
    }
}
