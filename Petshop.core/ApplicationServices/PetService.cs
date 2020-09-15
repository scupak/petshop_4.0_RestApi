using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Petshop.Core.Filter;

namespace Petshop.core.ApplicationServices
{
    public class PetService : IPetService
    {
        private IPetRepository _petRepository;
        private IOwnerRepository _ownerRepository;
        private IPetTypeRepository _petTypeRepository;

        public PetService(IPetRepository petRepository, IOwnerRepository ownerRepository, IPetTypeRepository petTypeRepository)
        {
            _petRepository = petRepository;
            _ownerRepository = ownerRepository;
            _petTypeRepository = petTypeRepository;
        }

        public Pet GetPetById(int id)
        {
            Pet pet = _petRepository.GetPets().List.Find(x => x.Id == id);

            Pet temppet = new Pet
            {
                Owner = pet.Owner,
                PetType = pet.PetType,
                Name = pet.Name, 
                Birthdate = pet.Birthdate,
                Color = pet.Color,
                Id = pet.Id,
                Price = pet.Price,
                SoldDate = pet.SoldDate,



            };


            temppet.Owner = _ownerRepository.GetOwners().Find(x => x.Id == pet.Owner.Id);

            temppet.PetType = _petTypeRepository.GetPetTypes().Find(x => x.Id == pet.PetType.Id);

            return temppet;

        }

        public FilteredList<Pet> GetPets(Filter filter)
        {
            if (!string.IsNullOrEmpty(filter.SearchText) && string.IsNullOrEmpty(filter.SearchField))
            {
                filter.SearchField = "name";
            }

            return _petRepository.GetPets(filter);
        }

        public FilteredList<Pet> GetPets()
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
        public Pet DeletePet(int id)
        {
            Pet pet = _petRepository.GetPets().List.Find(x => x.Id == id);
            if ( pet != null)
            {
                _petRepository.GetPets().List.Remove(pet);
                return pet;

            }
            else
            {
                 throw new KeyNotFoundException("Could not find a pet to delete ");
            }

        }

        public Pet EditPet(Pet pet)
        {
            int index = _petRepository.GetPets().List.FindLastIndex(c => c.Id == pet.Id);

            if (index == -1)
            {
                return null;
            }

            return _petRepository.EditPet(pet, index);

        }
    }
}
