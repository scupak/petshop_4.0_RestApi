using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using System;
using System.Collections.Generic;

namespace Petshop.infraStructure.Data
{
    public class PetRepository : IPetRepository
    {
        public int Id;
        public List<Pet> Pets;


        public PetRepository()
        {
            Id = 0;
            Pets = new List<Pet>();


        }

       

        public Pet AddPet(Pet pet)
        {

            Id++;
            pet.Id = Id;
            Pets.Add(pet);
            return pet;




        }

        public Pet EditPet(Pet pet, int index)
        {

            Pets[index] = pet;
            return Pets[index];

        }

        public List<Pet> GetPets()
        {
            return Pets;
        }
    }
}
