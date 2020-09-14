using System;
using System.Collections.Generic;
using System.Text;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.core.DomainServices
{
   public interface IOwnerRepository
    {
        public Owner AddOwner(Owner owner);

        public Owner EditOwner(Owner owner, int index);

        public List<Owner> GetOwners();
        public FilteredList<Owner> GetOwners(Filter filter);


    }
}
