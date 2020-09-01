using System;
using System.Collections.Generic;
using System.Text;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;

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

       
    }
}
