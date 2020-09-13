using System;
using System.Collections.Generic;
using System.Text;
using Petshop.Core.Entity;


namespace Petshop.infraStructure.Data
{
    public static class FakeDB
    {
        private static int _PetId = 1;
        public static List<Pet> _pets;
        private static int _OwnerId = 1;
        public static List<Owner> _owners;
        private static int _PetTypeId = 1;
        public static List<PetType> _PetTypes;
        public static void InitData()
        {
            _PetTypes = new List<PetType>
            {
                new PetType
                {
                    name = "dog",
                    Id = _PetTypeId++,
                    
                }
            };
           

            
            _owners = new List<Owner>
            {
                new Owner
                {
                    Id = _OwnerId++,
                    FirstName = "mike",
                    LastName = "davidson",
                    Address = "999manstreet",
                    PhoneNumber = "29999",
                    Email = "mike@email.com"


                }



            };

            _pets = new List<Pet>
            {
                new Pet
                {

                    Id = _PetId++,
                    Name = "Jerry",
                    PetType = new PetType
                    {
                        Id = 1,
                    },
                    Birthdate = DateTime.Now.AddYears(-12),
                    Color = "Blue",
                    Owner = new Owner
                    {
                        Id = 1,

                    },
                    Price = 50,
                    SoldDate = DateTime.Now.AddYears(-2),

                }

            };
        }

        public static Pet AddPet(Pet pet)
        {
            pet.Id = _PetId++;
            _pets.Add(pet);
            return pet;
        }

        public static Owner AddOwner(Owner owner)
        {
            
            owner.Id = _OwnerId++;
            _owners.Add(owner);
            return owner;
        }

        public static PetType AddPetType(PetType petType)
        {

            petType.Id = _PetTypeId++;
            _PetTypes.Add(petType);
            return petType;
        }
    }
}