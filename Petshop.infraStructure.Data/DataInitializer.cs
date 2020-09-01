using System;
using System.Collections.Generic;
using System.Text;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;

namespace Petshop.infraStructure.Data
{
   public class DataInitializer
   {
       private IOwnerRepository OwnerRepository;
       private IPetRepository PetRepository;
        public DataInitializer(IPetRepository petRepository,IOwnerRepository ownerRepository)
        {
            PetRepository = petRepository;
            OwnerRepository = ownerRepository;
            
        }

        public void InitData()
        {
            OwnerRepository.AddOwner(new Owner("greg", "davidson", "999manstreet", "29999", "mike@email.com"));
            OwnerRepository.AddOwner(new Owner("alex", "davidson", "999manstreet", "29999", "mike@email.com"));
            OwnerRepository.AddOwner(new Owner("mike", "davidson", "999manstreet", "29999", "mike@email.com"));

            PetRepository.AddPet(new Pet("hulk", DateTime.Now, "green", OwnerRepository.GetOwners()[0], 40, PetType.Goat, DateTime.Now));
            PetRepository.AddPet(new Pet("hulk", DateTime.Now, "green", OwnerRepository.GetOwners()[0], 20, PetType.Goat, DateTime.Now));
            PetRepository.AddPet(new Pet("hulk", DateTime.Now, "green", OwnerRepository.GetOwners()[1], 50, PetType.Goat, DateTime.Now));
            PetRepository.AddPet(new Pet("hulk", DateTime.Now, "green", OwnerRepository.GetOwners()[1], 30, PetType.Goat, DateTime.Now));
            PetRepository.AddPet(new Pet("hulk", DateTime.Now, "green", OwnerRepository.GetOwners()[2], 40, PetType.Goat, DateTime.Now));
            PetRepository.AddPet(new Pet("dongo", DateTime.Now, "orange", null, 40, PetType.Dog, DateTime.Now));
        }
    }
}
