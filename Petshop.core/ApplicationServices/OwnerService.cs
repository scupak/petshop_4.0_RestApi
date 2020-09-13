using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;

namespace Petshop.core.ApplicationServices
{
   public class OwnerService : IOwnerService
    {
        private IOwnerRepository _ownerRepository;
        private IPetRepository _petRepository;
        public OwnerService(IOwnerRepository ownerRepository, IPetRepository petRepository)
        {
            _ownerRepository = ownerRepository;
            _petRepository = petRepository;
        }


        public Owner GetOwnerById(int id)
        {
            Owner owner = _ownerRepository.GetOwners().Find(x => x.Id == id);

            Owner tempOwner = new Owner
            {
                FirstName = owner.FirstName, 
                Email = owner.Email,
                LastName = owner.LastName,
                Address = owner.Address,
                PhoneNumber = owner.PhoneNumber, 
                Id = owner.Id


            };


            tempOwner.Pets = _petRepository.GetPets().Where(pet => pet.Owner.Id == owner.Id).ToList();

            return tempOwner;
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
