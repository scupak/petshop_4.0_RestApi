using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.infraStructure.Data 
{
   public class OwnerRepository : IOwnerRepository
   {
         

        public OwnerRepository()
        {
            
        }
        public Owner AddOwner(Owner owner)
        {
            return FakeDB.AddOwner(owner);
        }

        public Owner EditOwner(Owner owner, int index)
        {
            FakeDB._owners[index] = owner;
            return FakeDB._owners[index];
        }

        public List<Owner> GetOwners()
        {
            return FakeDB._owners;
        }

        public FilteredList<Owner> GetOwners(Filter filter)
        {
            var filteredList = new FilteredList<Owner>();

            filteredList.TotalCount = FakeDB._owners.Count;
            filteredList.FilterUsed = filter;

            IEnumerable<Owner> filtering = FakeDB._owners;

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
            throw new NotImplementedException();
        }

        public Owner DeleteOwner(int id)
        {
            throw new NotImplementedException();
        }
   }
}
