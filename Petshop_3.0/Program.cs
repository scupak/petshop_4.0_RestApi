using Petshop.core.ApplicationServices;
using Petshop.core.DomainServices;
using Petshop.infraStructure.Data;

namespace Petshop.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            IPetRepository petRepository = new PetRepository();
            
            IPetService petService = new PetService(petRepository);

            IOwnerRepository ownerRepository = new OwnerRepository();
            
            IOwnerService ownerService = new OwnerService(ownerRepository);

            FakeDB.InitData();



            Printer print = new Printer(petService,ownerService);
            print.PrintMenu();
        }

    }
}
