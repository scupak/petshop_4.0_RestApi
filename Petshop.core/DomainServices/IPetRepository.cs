using Petshop.Core.Entity;
using System.Collections.Generic;

namespace Petshop.core.DomainServices
{
    public interface IPetRepository
    {
        public Pet AddPet(Pet pet);

        public Pet EditPet(Pet pet, int index);

        public List<Pet> GetPets();

        

    }
}
