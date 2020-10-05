using System.Linq;
using Microsoft.EntityFrameworkCore;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.Infrastructure.Db.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context db;



        public UserRepository(Context context)
        {
            db = context;
        }

        public FilteredList<User> GetAll()
        {
            var filteredList = new FilteredList<User>();

            filteredList.TotalCount = db.Users.ToList().Count;

            filteredList.List = db.Users.ToList();

            return filteredList;
        }

        public User Get(long id)
        {
            return db.Users.FirstOrDefault(b => b.UserId == id);
        }

        public void Add(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
        }

        public void Edit(User entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Remove(long id)
        {
            var item = db.Users.FirstOrDefault(b => b.UserId == id);
            db.Users.Remove(item);
            db.SaveChanges();
        }
    }
}
