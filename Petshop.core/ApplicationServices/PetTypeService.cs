﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.core.ApplicationServices
{
  public  class PetTypeService : IPetTypeService
    {
        private IPetTypeRepository _petTypeRepository;
        private IPetRepository _petRepository;

        public PetTypeService(IPetTypeRepository petTypeRepository, IPetRepository petRepository)
        {
            _petTypeRepository = petTypeRepository;
            _petRepository = petRepository;
        }

        public PetType GetPetTypeById(int id)
        {
           PetType petType = _petTypeRepository.GetPetTypes().Find(x => x.Id == id);

           PetType temPetType = new PetType
           {
               name = petType.name,
               Id = petType.Id



           };

           temPetType.Pets = _petRepository.GetPets().List.Where(pet => pet.PetType.Id == petType.Id).ToList();

           return temPetType;
        }

        public List<PetType> GetPetTypes()
        {
            return _petTypeRepository.GetPetTypes();
        }

        public FilteredList<PetType> GetPetTypes(Filter filter)
        {
            if (!string.IsNullOrEmpty(filter.SearchText) && string.IsNullOrEmpty(filter.SearchField))
            {
                filter.SearchField = "name";
            }

            return _petTypeRepository.GetPetTypes(filter);
        }

        public PetType CreatePetType(PetType petType)
        {
            return _petTypeRepository.AddPetType(petType);
        }

        public PetType DeletePetType(int id)
        {
            PetType petType = _petTypeRepository.GetPetTypes().Find(x => x.Id == id);
            if (petType != null)
            {
                _petTypeRepository.GetPetTypes().Remove(petType);
                return petType;

            }
            else
            {
                throw new KeyNotFoundException("Could not find an owner to delete ");
            }
        }

        public PetType EditPetType(PetType petType)
        {
            int index = _petTypeRepository.GetPetTypes().FindLastIndex(c => c.Id == petType.Id);

            return _petTypeRepository.EditPetType(petType, index);
        }
    }
}
