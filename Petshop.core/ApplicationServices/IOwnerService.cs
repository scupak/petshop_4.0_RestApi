using System;
using System.Collections.Generic;
using System.Text;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.core.ApplicationServices
{
   public interface IOwnerService
    {
        public Owner GetOwnerById(int id);

        public FilteredList<Owner> GetOwners(Filter filter);

        public FilteredList<Owner> GetOwners();

        public Owner CreateOwner(Owner owner);

        public Owner DeleteOwner(int id);

        public Owner EditOwner(Owner owner);

    }
}
