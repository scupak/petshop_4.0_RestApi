﻿using System;
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

        public FilteredList<Owner> GetOwners(Filter filter);

        public Owner GetOwnerById(int id);

        public Owner DeleteOwner(int id);

        public FilteredList<Owner> GetOwners();
    }
}
