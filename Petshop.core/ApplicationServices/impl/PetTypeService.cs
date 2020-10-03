using System;
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
            /*
           PetType petType = _petTypeRepository.GetPetTypes(new Filter()).List.Find(x => x.ColourId == id);

           PetType temPetType = new PetType
           {
               name = petType.name,
               ColourId = petType.ColourId



           };

           temPetType.Pets = _petRepository.GetPets(new Filter()).List.Where(pet => pet.PetType.ColourId == petType.ColourId).ToList();

           return temPetType;
            */
            return _petTypeRepository.GetPetTypeById(id);
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
            /*
            PetType petType = _petTypeRepository.GetPetTypes(new Filter()).List.Find(x => x.ColourId == id);
            if (petType != null)
            {
                _petTypeRepository.GetPetTypes(new Filter()).List.Remove(petType);

                //delete all the pets with the deleted petType. 
                foreach (Pet pet in _petRepository.GetPets(new Filter()).List.Where(pet => pet.PetType.ColourId == petType.ColourId).ToList())
                {
                    _petRepository.GetPets(new Filter()).List.Remove(pet);

                }

                return petType;

            }
            else
            {
                throw new KeyNotFoundException("Could not find an owner to delete ");
            }
            */

            return _petTypeRepository.DeletePetType(id);
        }

        public PetType EditPetType(PetType petType)
        {
            /* if (owner == null)
            {
                throw new ArgumentException("The sent data is null");
            }

            int index = _ownerRepository.GetOwners(new Filter()).List.FindLastIndex(c => c.ColourId == owner.ColourId);

            if (index == -1)
            {
                throw  new KeyNotFoundException("owner needs to exist in database");
*/

            if (petType == null)
            {
                throw new ArgumentException("The sent data is null");
            }

            


            if (_petTypeRepository.GetPetTypeById(petType.Id) == null)
            {
                throw new KeyNotFoundException("petType needs to exist in database");

            }
            
            return _petTypeRepository.EditPetType(petType, 1);
        }
    }
}
