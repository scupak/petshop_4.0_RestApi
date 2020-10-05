using System.Collections.Generic;
using Petshop.Core.Filter;

namespace Petshop.core.DomainServices
{
    public interface IRepository<T>
    {
        FilteredList<T> GetAll();
        T Get(long id);
        void Add(T entity);
        void Edit(T entity);
        void Remove(long id);
    }
}
