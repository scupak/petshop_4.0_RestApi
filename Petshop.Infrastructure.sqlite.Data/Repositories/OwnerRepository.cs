using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
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
          Owner returnOwner = _context.Owners.Update(owner).Entity;

          _context.SaveChanges();

          return returnOwner;

        }

        public FilteredList<Owner> GetOwners(Filter filter)
        {
            var filteredList = new FilteredList<Owner>();

            filteredList.TotalCount = _context.Owners.Count();
            filteredList.FilterUsed = filter;

            IEnumerable<Owner> filtering = _context.Owners.AsNoTracking();

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                switch (filter.SearchField.ToLower())
                {
                    case "firstname":
                        filtering = filtering.Where(c => c.FirstName.Contains(filter.SearchText));
                        break;
                    case "lastname":
                        filtering = filtering.Where(c => c.LastName.Contains(filter.SearchText));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(filter.OrderDirection) && !string.IsNullOrEmpty(filter.OrderProperty))
            {
                var prop = typeof(Owner).GetProperty(filter.OrderProperty);
                filtering = "ASC".Equals(filter.OrderDirection) ?
                    filtering.OrderBy(c => prop.GetValue(c, null)) :
                    filtering.OrderByDescending(c => prop.GetValue(c, null));

            }

            filteredList.List = filtering.ToList();
            return filteredList;
        }

        public Owner GetOwnerById(int id)
        {
            //var changeTracker = _context.ChangeTracker.Entries<Pet>();

            return _context.Owners
                .Include(owner => owner.Pets)
                .FirstOrDefault(owner => owner.Id == id);
        }

        public Owner DeleteOwner(int id)
        {
            Owner owner = _context.Owners.ToList().Find(x => x.Id == id);
            if (owner != null)
            {
                var returnOwner = _context.Owners.Remove(owner).Entity;
                _context.SaveChanges();
                return returnOwner;

            }

            throw new KeyNotFoundException("Could not find a Owner to delete ");
        }

        public FilteredList<Owner> GetOwners()
        {
            FilteredList<Owner> filteredList = new FilteredList<Owner>();

            filteredList.List = _context.Owners.ToList();

            return filteredList;
        }
    }
    }


