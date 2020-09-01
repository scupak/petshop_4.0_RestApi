using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Petshop.core.ApplicationServices
{
    public class PetService : IPetService
    {
        private IPetRepository _petRepository;

        public PetService(IPetRepository petRepository)
        {
            _petRepository = petRepository;


        }

        public Pet GetPetById(int id)
        {
            return _petRepository.GetPets().Find(x => x.Id == id);
        }

        public List<Pet> GetPets()
        {
            return _petRepository.GetPets();
        }

        public Pet CreatePet(Pet pet)
        {
            return _petRepository.AddPet(pet);


        }

        /**
         * TODO refactor so you dont have to use find twice. 
         */
        public bool DeletePet(int id)
        {
            if (_petRepository.GetPets().Find(x => x.Id == id) != null)
            {
                _petRepository.GetPets().Remove(_petRepository.GetPets().Find(x => x.Id == id));
                return true;

            }
            else
            {
                return false;
            }

        }

        public Pet EditPet(Pet pet)
        {
            int index = _petRepository.GetPets().FindLastIndex(c => c.Id == pet.Id);

            if (index == -1)
            {
                return null;
            }

            return _petRepository.EditPet(pet, index);

        }
    }
}
