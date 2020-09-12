using System;
using System.Collections.Generic;
using System.Text;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;

namespace Petshop.core.ApplicationServices
{
    class PetTypeService : IPetTypeService
    {
        private IPetTypeRepository _petTypeRepository;

        public PetTypeService(IPetTypeRepository petTypeRepository)
        {
            _petTypeRepository = petTypeRepository;
        }

        public PetType GetPetTypeById(int id)
        {
            return _petTypeRepository.GetPetTypes().Find(x => x.Id == id);
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
