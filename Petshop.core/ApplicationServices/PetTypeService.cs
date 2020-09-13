using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;

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

        public PetType CreatePetType(PetType petType)
        {
            return _petTypeRepository.AddPetType(petType);
        }

        public bool DeletePetType(int id)
        {
            if (_petTypeRepository.GetPetTypes().Find(x => x.Id == id) != null)
            {
                _petTypeRepository.GetPetTypes().Remove(_petTypeRepository.GetPetTypes().Find(x => x.Id == id));
                return true;

            }
            else
            {
                return false;
            }
        }

        public PetType EditPetType(PetType petType)
        {
            int index = _petTypeRepository.GetPetTypes().FindLastIndex(c => c.Id == petType.Id);

            return _petTypeRepository.EditPetType(petType, index);
        }
    }
}
