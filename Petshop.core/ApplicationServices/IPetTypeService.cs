using System;
using System.Collections.Generic;
using System.Text;
using Petshop.Core.Entity;

namespace Petshop.core.ApplicationServices
{
   public interface IPetTypeService
    {
        public PetType GetPetTypeById(int id);

        public List<PetType> GetPetTypes();

        public PetType CreatePetType(PetType petType);

        public bool DeletePetType(int id);

        public PetType EditPetType(PetType petType);
    }
}
