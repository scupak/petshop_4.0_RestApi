using System;
using System.Collections.Generic;
using System.Text;
using Petshop.Core.Entity;

namespace Petshop.core.DomainServices
{
   public interface IOwnerRepository
    {
        public Owner AddOwner(Owner owner);

        public Owner EditOwner(Owner owner, int index);

        public List<Owner> GetOwners();

        
    }
}
