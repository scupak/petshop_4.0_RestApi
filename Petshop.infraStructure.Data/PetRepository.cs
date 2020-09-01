using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using System;
using System.Collections.Generic;

namespace Petshop.infraStructure.Data
{
    public class PetRepository : IPetRepository
    {
        
        public PetRepository()
        {

        }

       

        public Pet AddPet(Pet pet)
        {

            return FakeDB.AddPet(pet);




        }

        public Pet EditPet(Pet pet, int index)
        {

            FakeDB._pets[index] = pet;
            return FakeDB._pets[index];

        }

        public List<Pet> GetPets()
        {
            return FakeDB._pets;
        }
    }
}
