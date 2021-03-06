﻿using System;
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
            Owner owner = _ownerRepository.GetOwners().Find(x => x.ColourId == id);

            Owner tempOwner = new Owner
            {
                FirstName = owner.FirstName, 
                Email = owner.Email,
                LastName = owner.LastName,
                Address = owner.Address,
                PhoneNumber = owner.PhoneNumber, 
                ColourId = owner.ColourId


            };


            tempOwner.Pets = _petRepository.GetPets().List.Where(pet => pet.Owner.ColourId == owner.ColourId).ToList();

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

        public FilteredList<Owner> GetOwners()
        {
           return _ownerRepository.GetOwners();
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
            if (owner == null)
            {
                throw new ArgumentException("The sent data is null");
            }

            int index = _ownerRepository.GetOwners().List.FindLastIndex(c => c.Id == owner.Id);

            if (index == -1)
            {
                throw  new KeyNotFoundException("owner needs to exist in database");

            }

            return _ownerRepository.EditOwner(owner, index);
        }
    }
}
