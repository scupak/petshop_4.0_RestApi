using Petshop.Core.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Petshop.Core.Filter;

namespace Petshop.core.ApplicationServices
{
    public interface IPetService
    {
        public Pet GetPetById(int id);

        public FilteredList<Pet> GetPets(Filter filter);

        public FilteredList<Pet> GetPets();

        public Pet CreatePet(Pet pet);

        public bool DeletePet(int id);

        public Pet EditPet(Pet pet);



    }
}
