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
        public static void InitData()
        {
           

            
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
                    PetType = PetType.Cat,
                    Birthdate = DateTime.Now.AddYears(-12),
                    Color = "Blue",
                    //Owner = _owners[0],
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
    }
}