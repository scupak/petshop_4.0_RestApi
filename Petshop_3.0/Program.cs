using Petshop.core.ApplicationServices;
using Petshop.core.ApplicationServices.impl;
using Petshop.core.DomainServices;
using Petshop.infraStructure.Data;

namespace Petshop.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            IPetRepository petRepository = new PetRepository();
            
            IPetTypeRepository petTypeRepository = new PetTypeRepository();

            IOwnerRepository ownerRepository = new OwnerRepository();

            IPetService petService = new PetService(petRepository,ownerRepository,petTypeRepository);
            
            IOwnerService ownerService = new OwnerService(ownerRepository,petRepository);

            FakeDB.InitData();



            Printer print = new Printer(petService,ownerService);
            print.PrintMenu();
        }

    }
}
