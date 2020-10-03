using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

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
            /*
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


            tempOwner.Pets = _petRepository.GetPets().List.Where(pet => pet.Owner.Id == owner.Id).ToList();

            return tempOwner;
            */
          return  _ownerRepository.GetOwnerById(id);
        }

        public FilteredList<Owner> GetOwners(Filter filter)
        {
            if (!string.IsNullOrEmpty(filter.SearchText) && string.IsNullOrEmpty(filter.SearchField))
            {
                filter.SearchField = "firstname";
            }

            return _ownerRepository.GetOwners(filter);
        }

        public Owner CreateOwner(Owner owner)
        {
            return _ownerRepository.AddOwner(owner);
        }

        public Owner DeleteOwner(int id)
        {
            return _ownerRepository.DeleteOwner(id);
        }

        public Owner EditOwner(Owner owner)
        {
            int index = _ownerRepository.GetOwners(new Filter()).List.FindLastIndex(c => c.Id == owner.Id);

            return _ownerRepository.EditOwner(owner, index);
        }
    }
}
