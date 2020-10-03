using Petshop.Core.Entity;
using System.Collections.Generic;
using Petshop.Core.Filter;

namespace Petshop.core.DomainServices
{
    public interface IPetRepository
    {
        public Pet AddPet(Pet pet);

        public Pet EditPet(Pet pet, int index);

        public Pet DeletePet(int id);

        public FilteredList<Pet> GetPets(Filter filter);

        public Pet GetPetById(int id);







    }
}
