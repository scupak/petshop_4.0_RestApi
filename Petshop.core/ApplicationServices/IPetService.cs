using Petshop.Core.Entity;
using System.Collections.Generic;

namespace Petshop.core.ApplicationServices
{
    public interface IPetService
    {
        public Pet GetPetById(int id);

        public List<Pet> GetPets();

        public Pet CreatePet(Pet pet);

        public bool DeletePet(int id);

        public Pet EditPet(Pet pet);



    }
}
