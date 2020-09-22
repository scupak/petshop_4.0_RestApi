using System;
using System.Collections.Generic;
using System.Text;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.Infrastructure.Db.Data.Repositories
{
  public  class OwnerRepository : IOwnerRepository
    {
        readonly Context _context;

        public OwnerRepository(Context context)
        {
            _context = context;
        }

        public Owner AddOwner(Owner owner)
        {
            var returnOwner = _context.Owners.Add(owner).Entity;
            _context.SaveChanges();
            return returnOwner;

        }

        public Owner EditOwner(Owner owner, int index)
        {
            throw new NotImplementedException();
        }

        public List<Owner> GetOwners()
        {
            throw new NotImplementedException();
        }

        public FilteredList<Owner> GetOwners(Filter filter)
        {
            throw new NotImplementedException();
        }
    }
}
