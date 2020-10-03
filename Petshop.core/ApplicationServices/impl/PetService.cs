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
           return _petRepository.GetPetById(id);
        }

        public FilteredList<Pet> GetPets(Filter filter)
        {
            if (!string.IsNullOrEmpty(filter.SearchText) && string.IsNullOrEmpty(filter.SearchField))
            {
                filter.SearchField = "name";
            }

            return _petRepository.GetPets(filter);
        }


        public Pet CreatePet(Pet pet)
        {
            if (pet.PetType != null)
            {
                var petType = _petTypeRepository.GetPetTypes(new Filter()).List.FirstOrDefault(p => p.Id == pet.PetType.Id);
                if (petType == null)
                {
                    throw new InvalidDataException("PetType must exist in the database");
                }


                if (pet.Owner != null)
                {
                    var owner = _ownerRepository.GetOwners(new Filter()).List.FirstOrDefault(o => o.Id == pet.Owner.Id);

                    if (owner == null)
                    {
                        throw new InvalidDataException("Owner must exist in the database");
                    }
                }

                return _petRepository.AddPet(pet);

            }

            throw new InvalidDataException("PetType must exist in the database");



        }

        /**
         * TODO refactor so you dont have to use find twice. 
         */
        public Pet DeletePet(int id)
        {
            return _petRepository.DeletePet(id);

        }

        public Pet EditPet(Pet pet)
        {

            if (pet.PetType != null)
            {
                int index = _petRepository.GetPets(new Filter()).List.FindLastIndex(c => c.Id == pet.Id);

                if (index == -1)
                {
                    return null;
                }


                var petType = _petTypeRepository.GetPetTypes(new Filter()).List.FirstOrDefault(p => p.Id == pet.PetType.Id);
                if (petType == null)
                {
                    throw new InvalidDataException("PetType must exist in the database");
                }


                if (pet.Owner != null)
                {
                    var owner = _ownerRepository.GetOwners(new Filter()).List.FirstOrDefault(o => o.Id == pet.Owner.Id);

                    if (owner == null)
                    {
                        throw new InvalidDataException("Owner must exist in the database");
                    }
                }

                return _petRepository.EditPet(pet, index);

            }

            throw new InvalidDataException("PetType must exist in the database");


        }
    }
}
