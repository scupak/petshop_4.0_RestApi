using System;
using System.Collections.Generic;
using System.Text;
using Petshop.Core.Entity;

namespace Petshop.core.DomainServices
{
   public interface IPetTypeRepository
    {
        public PetType AddPetType(PetType petType);

        public PetType EditPetType(PetType petType, int index);

        public List<PetType> GetPetTypes();
    }
}
