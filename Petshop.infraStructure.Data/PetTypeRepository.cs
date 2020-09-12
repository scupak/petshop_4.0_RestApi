using System;
using System.Collections.Generic;
using System.Text;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;

namespace Petshop.infraStructure.Data
{
    class PetTypeRepository : IPetTypeRepository
    {
        public PetType AddPetType(PetType petType)
        {
            return FakeDB.AddPetType(petType);
        }

        public PetType EditPetType(PetType petType, int index)
        {
            FakeDB._PetTypes[index] = petType;
            return FakeDB._PetTypes[index];
        }

        public List<PetType> GetPetTypes()
        {
            return FakeDB._PetTypes;
        }
    }
}
