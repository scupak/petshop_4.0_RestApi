using System;
using System.Collections.Generic;
using System.Text;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.core.DomainServices
{
 public interface IUserRepository
    {
        public FilteredList<User> GetAll();

        public User Get(long id);

        public void Add(User entity);

        public void Edit(User entity);

        public void Remove(long id);
    }
}
