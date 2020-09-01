using System;
using System.Collections.Generic;
using System.Text;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;

namespace Petshop.core.ApplicationServices
{
   public class OwnerService : IOwnerService
    {
        private IOwnerRepository _ownerRepository;
        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }


        public Owner GetOwnerById(int id)
        {
            return _ownerRepository.GetOwners().Find(x => x.Id == id);
        }

        public List<Owner> GetOwners()
        {
            return _ownerRepository.GetOwners();
        }

        public Owner CreateOwner(Owner owner)
        {
            return _ownerRepository.AddOwner(owner);
        }

        public bool DeleteOwner(int id)
        {
            if (_ownerRepository.GetOwners().Find(x => x.Id == id) != null)
            {
                _ownerRepository.GetOwners().Remove(_ownerRepository.GetOwners().Find(x => x.Id == id));
                return true;

            }
            else
            {
                return false;
            }
        }

        public Owner EditOwner(Owner owner)
        {
            int index = _ownerRepository.GetOwners().FindLastIndex(c => c.Id == owner.Id);

            return _ownerRepository.EditOwner(owner, index);
        }
    }
}
