using System;
using System.Collections.Generic;
using System.Text;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.core.ApplicationServices
{
   public interface IPetTypeService
    {
        public PetType GetPetTypeById(int id);

        public FilteredList<PetType> GetPetTypes(Filter filter);

        public PetType CreatePetType(PetType petType);

        public PetType DeletePetType(int id);

        public PetType EditPetType(PetType petType);
    }
}
