using System;
using System.Collections.Generic;
using System.Text;
using Petshop.Core.Entity;

namespace Petshop.core.ApplicationServices
{
   public interface IOwnerService
    {
        public Owner GetOwnerById(int id);

        public List<Owner> GetOwners();

        public Owner CreateOwner(Owner owner);

        public bool DeleteOwner(int id);

        public Owner EditOwner(Owner owner);

    }
}
