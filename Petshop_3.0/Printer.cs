using Petshop.core.ApplicationServices;
using Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Petshop.core.DomainServices;

namespace Petshop.UI
{
    public class Printer
    {
        private IPetService _PetService;
        private IOwnerService _ownerService;
        
        public Printer(IPetService petService,IOwnerService ownerService)
        {
            _PetService = petService;
            _ownerService = ownerService;

        }

        public void PrintMenu()
        {
            //List<Video> videos = new List<Video>();

            string[] menuItems =
            {
                "Show list of all Pets",
                "show one pet by id",
                "Create a new Pet",
                "Delete pet",
                "Update a Pet",
                "Search Pets by Type",
                "Sort Pets By Price",
                "Get 5 cheapest available Pets",

                "Show a list of all Owners",
                "Show one owner by id",
                "create a new owner",
                "Update an owner",
                "Delete owner",
                

                "Exit"


            };

            var selection = 0;
            while (selection != 14)
            {


                selection = ShowMenu(menuItems);
                int idSelection;
                Console.ReadLine();

                switch (selection)
                {

                    case 1:
                        Console.WriteLine("List all pets");
                        for (int i = 0; i < _PetService.GetPets().Count; i++)
                        {
                            //Console.WriteLine((i +1) + ":" + menuItems[i]);
                            Console.WriteLine($"{(i + 1)}:{ _PetService.GetPets()[i]}");

                        }

                        Console.ReadLine();
                        break;
                    case 2:

                        Console.WriteLine("show single pet by id");
                        Console.Write("write id of the pet you want:");

                        while (!int.TryParse(Console.ReadLine(), out idSelection))
                        {
                            Console.WriteLine("You need to select an id");

                        }


                        // int showid = selection;

                        if (_PetService.GetPetById(idSelection) == null)
                        {
                            Console.WriteLine("could not find pet");
                            Console.ReadLine();
                        }
                        else
                        {


                            Console.WriteLine(_PetService.GetPetById(idSelection));
                            Console.ReadLine();
                        }


                        break;
                    case 3:
                        // TODO: add input validation
                        Console.WriteLine("Add pet");
                        Console.WriteLine("Enter name");
                        string name = Console.ReadLine();
                        DateTime birthDate;

                        Console.WriteLine("Enter Birthdate, day/month/year");
                        while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
                        {
                            Console.WriteLine("You need to select a valid date");

                        }

                        DateTime solddate;
                        Console.WriteLine("Enter solddate, day/month/year");
                        while (!DateTime.TryParse(Console.ReadLine(), out solddate))
                        {
                            Console.WriteLine("You need to select a valid date");

                        }



                        Console.ReadLine();
                        Console.WriteLine("Enter color");
                        string color = Console.ReadLine();

                        Console.ReadLine();
                        Console.WriteLine("Enter Owner");
                        Console.WriteLine("write id of new owner:");
                        Console.WriteLine("write -1 to make owner null:");
                        string Ownerinput;
                        int createownerselection;
                        Owner createOwner = null;
                        while (true)
                        {
                            for (int i = 0; i < _ownerService.GetOwners().Count; i++)
                            {
                                //Console.WriteLine((i +1) + ":" + menuItems[i]);
                                Console.WriteLine($"{(i + 1)}:{_ownerService.GetOwners()[i]}");

                            }


                            Ownerinput = Console.ReadLine();

                            while (!int.TryParse(Ownerinput, out createownerselection) && !Ownerinput.Equals("-1"))
                            {
                                Console.WriteLine("You need to select an id");
                                Ownerinput = Console.ReadLine();
                            }

                            if (Ownerinput.Equals("-1"))
                            {
                                break;
                            }


                            // int showid = selection;

                            if (_ownerService.GetOwnerById(createownerselection) == null)
                            {
                                Console.WriteLine("could not find owner");
                                Console.ReadLine();
                            }
                            else
                            {

                                createOwner = _ownerService.GetOwnerById(createownerselection);
                                Console.WriteLine(createOwner);
                                Console.ReadLine();
                                break;
                            }


                        }

                       











                        double price;
                        while (!double.TryParse(Console.ReadLine(), out price))
                        {
                            Console.WriteLine("You need to select a price");

                        }

                        PetType types = PetType.Cat;
                        while (true)
                        {
                            Console.WriteLine("Select a pet type:");
                            Console.WriteLine("1 cat");
                            Console.WriteLine("2 dog");
                            Console.WriteLine("3 goat");
                            int Typeselection;

                            while (!int.TryParse(Console.ReadLine(), out Typeselection) || (Typeselection < 1 || Typeselection > 3))
                            {
                                Console.WriteLine("You need to select an option");

                            }

                            switch (Typeselection)
                            {
                                case 1:
                                    types = PetType.Cat;
                                    break;

                                case 2:
                                    types = PetType.Dog;
                                    break;

                                case 3:
                                    types = PetType.Goat;
                                    break;


                            }
                            break;



                        }




                        _PetService.CreatePet(new Pet(name, birthDate, color, createOwner, price, types, solddate));

                        Console.ReadLine();
                        break;
                    case 4:
                        // TODO: finish creating deletion. 
                        Console.WriteLine("Delete Pet");
                        Console.Write("write the id of the pet you wish to delete:");
                        while (!int.TryParse(Console.ReadLine(), out idSelection))
                        {
                            Console.WriteLine("You need to select an id");

                        }

                        Console.WriteLine(_PetService.DeletePet(idSelection)
                            ? "pet was deleted successfully"
                            : "pet could not be deleted or the wrong id was typed");
                        Console.ReadLine();

                        break;
                    case 5:
                        Console.WriteLine("Edit pet");
                        Console.Write("write the id of the pet you wish to edit:");
                        while (!int.TryParse(Console.ReadLine(), out idSelection))
                        {
                            Console.WriteLine("You need to select an id");

                        }

                        Pet selectedPet;
                        selectedPet = _PetService.GetPetById(idSelection);

                        if (selectedPet == null)
                        {
                            Console.WriteLine("could not find pet");
                            Console.ReadLine();
                        }
                        else
                        {


                            Console.WriteLine(_PetService.GetPetById(idSelection));
                            Pet editedPet = new Pet(selectedPet.Name,selectedPet.Birthdate,selectedPet.Color,selectedPet.Owner,selectedPet.Price,selectedPet.PetType,selectedPet.SoldDate);
                            editedPet.PetId = selectedPet.PetId;
                            Console.ReadLine();
                            Console.WriteLine("Select what part of the pet you want to edit");

                            string[] updateMenuItems =
                            {
                                //string name, DateTime birthdate, string color , string previousOwner,double price, PetType petType, DateTime soldDate
                                "Edit name",
                                "Edit birthdate",
                                "Edit color",
                                "Edit Owner",
                                "Edit price",
                                "Edit petType",
                                "Edit solddate",
                                
                                "Exit"
                            };
                            var editSelection = 0;

                            while (editSelection != 8)
                            {


                                editSelection = ShowMenu(updateMenuItems);
                                //int editIdSelection;
                                Console.ReadLine();


                                switch (editSelection)
                                {


                                    case 1:
                                        string editName;
                                        Console.WriteLine("Edit Name");
                                        Console.Write("write new Name:");
                                        editName = Console.ReadLine();

                                        while (editName == null || editName.Length <= 0)
                                        {
                                            Console.Write("title has to have a length higher then 0:");
                                            editName = Console.ReadLine();
                                        }

                                        //editedPet = selectedPet;
                                        editedPet.Name = editName;
                                       
                                        if (_PetService.EditPet(editedPet) != null)
                                        {
                                            Console.WriteLine("the update was successful");
                                            Console.WriteLine(_PetService.GetPetById(idSelection));
                                            Console.ReadLine();

                                        }
                                        else
                                        {
                                            Console.Write("the update was unsuccessful");
                                            Console.ReadLine();
                                        }
                                        

                                        break;

                                    case 2:
                                        DateTime editBirthday;
                                        Console.WriteLine("Edit Birthday");

                                        Console.WriteLine("Enter Birthday, day/month/year");
                                        while (!DateTime.TryParse(Console.ReadLine(), out editBirthday))
                                        {
                                            Console.WriteLine("You need to select a valid date");

                                        }


                                        //editedPet = selectedPet;
                                        editedPet.Birthdate = editBirthday;

                                        if (_PetService.EditPet(editedPet) != null)
                                        {
                                            Console.WriteLine("the update was successful");
                                            Console.WriteLine(_PetService.GetPetById(idSelection));
                                            Console.ReadLine();

                                        }
                                        else
                                        {
                                            Console.Write("the update was unsuccessful");
                                            Console.ReadLine();
                                        }

                                        break;

                                    case 3:
                                        string editColor;
                                        Console.WriteLine("Edit color");
                                        Console.Write("write a new color:");
                                        editColor = Console.ReadLine();

                                        while (editColor == null || editColor.Length <= 0)
                                        {
                                            Console.Write("the color has to have a length higher then 0:");
                                            editColor = Console.ReadLine();
                                        }

                                        //editedPet = selectedPet;
                                        editedPet.Color = editColor;

                                        if (_PetService.EditPet(editedPet) != null)
                                        {
                                            Console.WriteLine("the update was successful");
                                            Console.WriteLine(_PetService.GetPetById(idSelection));
                                            Console.ReadLine();

                                        }
                                        else
                                        {
                                            Console.Write("the update was unsuccessful");
                                            Console.ReadLine();
                                        }

                                        break;

                                    case 4:
                                        string input;
                                        Owner editOwner;
                                        int ownerselection;
                                        Console.WriteLine("Edit Owner");
                                        Console.WriteLine("write id of new owner:");
                                        Console.WriteLine("write -1 to exit:");

                                        while (true)
                                        {
                                            

                                            for (int i = 0; i < _ownerService.GetOwners().Count; i++)
                                            {
                                                //Console.WriteLine((i +1) + ":" + menuItems[i]);
                                                Console.WriteLine($"{(i + 1)}:{_ownerService.GetOwners()[i]}");

                                            }

                                            
                                            input = Console.ReadLine();

                                            while (!int.TryParse(input, out ownerselection) && !input.Equals("-1"))
                                            {
                                                Console.WriteLine("You need to select an id");
                                                input = Console.ReadLine();
                                            }

                                            if (input.Equals("-1"))
                                            {
                                                break;
                                            }
                                            

                                            // int showid = selection;

                                            if (_ownerService.GetOwnerById(ownerselection) == null)
                                            {
                                                Console.WriteLine("could not find owner");
                                                Console.ReadLine();
                                            }
                                            else
                                            {

                                                editOwner = _ownerService.GetOwnerById(ownerselection);
                                                Console.WriteLine(editOwner);
                                                Console.ReadLine();
                                                break;
                                            }
                                            

                                        }

                                        if (input.Equals("-1"))
                                        {
                                            break;
                                        }



                                        //editedPet = selectedPet;
                                        editedPet.Owner = _ownerService.GetOwnerById(ownerselection);
                                        if (_PetService.EditPet(editedPet) != null)
                                        {
                                            Console.WriteLine("the update was successful");
                                            Console.WriteLine(_PetService.GetPetById(idSelection));
                                            Console.ReadLine();

                                        }
                                        else
                                        {
                                            Console.Write("the update was unsuccessful");
                                            Console.ReadLine();
                                        }

                                        break;

                                    case 5:
                                        double editprice;
                                        Console.WriteLine("Edit price");
                                        Console.Write("write new price:");

                                        while (!double.TryParse(Console.ReadLine(), out editprice))
                                        {
                                            Console.WriteLine("You need to select a price");

                                        }

                                        //editedPet = selectedPet;
                                        editedPet.Price = editprice;

                                        if (_PetService.EditPet(editedPet) != null)
                                        {
                                            Console.WriteLine("the update was successful");
                                            Console.WriteLine(_PetService.GetPetById(idSelection));
                                            Console.ReadLine();

                                        }
                                        else
                                        {
                                            Console.Write("the update was unsuccessful");
                                            Console.ReadLine();
                                        }

                                        break;
                                    case 6:

                                        PetType Edittypes = PetType.Cat;

                                        Console.WriteLine("Select a pet type:");
                                        Console.WriteLine("1 cat");
                                        Console.WriteLine("2 dog");
                                        Console.WriteLine("3 goat");
                                        int Typeselection;

                                        while (!int.TryParse(Console.ReadLine(), out Typeselection) ||
                                               (Typeselection < 1 || Typeselection > 3))
                                        {
                                            Console.WriteLine("You need to select an option");

                                        }

                                        switch (Typeselection)
                                        {
                                            case 1:
                                                Edittypes = PetType.Cat;
                                                break;

                                            case 2:
                                                Edittypes = PetType.Dog;
                                                break;

                                            case 3:
                                                Edittypes = PetType.Goat;
                                                break;


                                        }


                                        //editedPet = selectedPet;
                                        editedPet.PetType = Edittypes;

                                        if (_PetService.EditPet(editedPet) != null)
                                        {
                                            Console.WriteLine("the update was successful");
                                            Console.WriteLine(_PetService.GetPetById(idSelection));
                                            Console.ReadLine();

                                        }
                                        else
                                        {
                                            Console.Write("the update was unsuccessful");
                                            Console.ReadLine();
                                        }

                                        break;

                                    case 7:
                                        DateTime editsoldDate;
                                        Console.WriteLine("Edit soldDate");

                                        Console.WriteLine("Enter soldDate, day/month/year");
                                        while (!DateTime.TryParse(Console.ReadLine(), out editsoldDate))
                                        {
                                            Console.WriteLine("You need to select a valid date");

                                        }


                                        //editedPet = selectedPet;
                                        editedPet.Birthdate = editsoldDate;

                                        if (_PetService.EditPet(editedPet) != null)
                                        {
                                            Console.WriteLine("the update was successful");
                                            Console.WriteLine(_PetService.GetPetById(idSelection));
                                            Console.ReadLine();

                                        }
                                        else
                                        {
                                            Console.Write("the update was unsuccessful");
                                            Console.ReadLine();
                                        }

                                        break;



                                }




                            }

                        }

                        break;
                    case 6:
                        Console.WriteLine("Search Pets by Type");
                        Console.ReadLine();

                        Console.WriteLine("please select the type of pet you want\n");
                        Console.WriteLine("1: Dog");
                        Console.WriteLine("2: Cat");
                        Console.WriteLine("3: Goat");

                        int searchTypeselection;

                        while (!int.TryParse(Console.ReadLine(), out searchTypeselection) ||
                               (searchTypeselection < 1 || searchTypeselection > 3))
                        {
                            Console.WriteLine("You need to select an option");

                        }

                        List<Pet> pets = _PetService.GetPets();
                        List<Pet> searchPet;

                        switch (searchTypeselection)
                        {

                            case 1:
                                searchPet = pets.FindAll(pet => pet.PetType.Equals(PetType.Dog));
                                break;
                            case 2:
                                searchPet = pets.FindAll(pet => pet.PetType.Equals(PetType.Cat));
                                break;
                            default:
                                searchPet = pets.FindAll(pet => pet.PetType.Equals(PetType.Goat));
                                break;
                        }

                        if(searchPet == null || searchPet.Count == 0) {
                            Console.WriteLine("did not find any pets with that type");

                        }

                        foreach (var pet in searchPet)
                        {
                            Console.WriteLine(pet.ToString());
                        }

                        Console.ReadLine();
                        break;

                    case 7:
                        Console.WriteLine("Sort Pets By Price");
                        Console.ReadLine();
                        foreach (var pet in _PetService.GetPets().OrderBy(o => o.Price).ToList())
                        {
                            Console.WriteLine(pet.ToString());
                        }
                        Console.ReadLine();

                        break;

                    case 8:
                        Console.WriteLine("Get 5 cheapest available Pets");
                        Console.ReadLine();

                        List<Pet> cheapestPets = _PetService.GetPets().OrderBy(o => o.Price).ToList();

                        if (cheapestPets.Count! < 5)
                        {

                            for (int i = 0; i < 5; i++)
                            {
                                Console.WriteLine(cheapestPets[i]);

                            }

                        }
                        else
                        {
                            foreach (Pet pet in cheapestPets)
                            {
                                Console.WriteLine(pet);
                            }

                        }
                        Console.ReadLine();
                        break;


                    case 9:
                        Console.WriteLine("List all owners");
                        for (int i = 0; i < _ownerService.GetOwners().Count; i++)
                        {
                            //Console.WriteLine((i +1) + ":" + menuItems[i]);
                            Console.WriteLine($"{(i + 1)}:{ _ownerService.GetOwners()[i]}");

                        }

                        Console.ReadLine();
                        break;

                    case 10:

                        Console.WriteLine("show single owner by id");
                        Console.Write("write id of the owner you want:");

                        while (!int.TryParse(Console.ReadLine(), out idSelection))
                        {
                            Console.WriteLine("You need to select an id");

                        }


                        // int showid = selection;

                        if (_ownerService.GetOwnerById(idSelection) == null)
                        {
                            Console.WriteLine("could not find owner");
                            Console.ReadLine();
                        }
                        else
                        {


                            Console.WriteLine(_ownerService.GetOwnerById(idSelection));
                            Console.ReadLine();
                        }


                        break;

                    case 11:
                        //public Owner(string firstName, string lastName, string address, string phoneNumber, string email)
                        // TODO: add input validation
                        Console.WriteLine("Add owner");
                        Console.WriteLine("Enter firstName");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Enter lastName");
                        string lastName = Console.ReadLine();
                        Console.WriteLine("Enter address");
                        string address = Console.ReadLine();
                        Console.WriteLine("Enter phoneNumber");
                        string phoneNumber = Console.ReadLine();
                        Console.WriteLine("Enter  email");
                        string email = Console.ReadLine();


                       Console.WriteLine(_ownerService.CreateOwner(new Owner(firstName,lastName,address,phoneNumber,email)));

                        Console.ReadLine();
                        break;

                    case 12:
                        Console.WriteLine("Edit owner");
                        Console.Write("write the id of the owner you wish to edit:");
                        while (!int.TryParse(Console.ReadLine(), out idSelection))
                        {
                            Console.WriteLine("You need to select an id");

                        }

                        Owner selectedOwner;
                        selectedOwner = _ownerService.GetOwnerById(idSelection);


                        if (selectedOwner == null)
                        {
                            Console.WriteLine("could not find owner");
                            Console.ReadLine();
                        }
                        else
                        {


                            Console.WriteLine(_ownerService.GetOwnerById(idSelection));
                            Console.ReadLine();
                            Console.WriteLine("Select what part of the owner you want to edit");
                            
                           Owner editedOwner = new Owner(selectedOwner.FirstName,selectedOwner.LastName, selectedOwner.Address, selectedOwner.PhoneNumber, selectedOwner.Email);
                           editedOwner.Id = selectedOwner.Id;
                            string[] updateMenuItems =
                            {
                                //string name, DateTime birthdate, string color , string previousOwner,double price, PetType petType, DateTime soldDate
                                "Edit firstName",
                                "Edit lastName",
                                "Edit address",
                                "Edit phonenumber",
                                "Edit email",
                                "Exit"
                            };
                            var editSelection = 0;

                            while (editSelection != 6)
                            {


                                editSelection = ShowMenu(updateMenuItems);
                                //int editIdSelection;
                                Console.ReadLine();


                                switch (editSelection)
                                {
                                    case 1:

                                        UpdateOwner("firstName",editedOwner,UpdateTypes.FirstName);
                                        break;
                                    case 2:
                                        UpdateOwner("LastName", editedOwner,UpdateTypes.LastName);
                                        break;
                                    case 3:
                                        UpdateOwner("address", editedOwner,UpdateTypes.Address);
                                        break;
                                    case 4:
                                        UpdateOwner("phonenumber", editedOwner,UpdateTypes.Phonenumber);
                                        break;

                                    case 5:
                                        UpdateOwner("email", editedOwner,UpdateTypes.Email);
                                        break;


                                }




                            }

                        }
                        break;
                    case 13:
                        // TODO: finish creating deletion. 
                        Console.WriteLine("Delete Owner");
                        Console.Write("write the id of the owner you wish to delete:");
                        while (!int.TryParse(Console.ReadLine(), out idSelection))
                        {
                            Console.WriteLine("You need to select an id");

                        }

                        Console.WriteLine(_ownerService.DeleteOwner(idSelection)
                            ? "Owner was deleted successfully"
                            : "Owner could not be deleted or the wrong id was typed");
                        Console.ReadLine();

                        break;


                    case 14:
                        Console.WriteLine("Exit");
                        Console.ReadLine();
                        break;








                }

            }
        }

        private static int ShowMenu(string[] menuItems)
        {
            Console.Clear();


            Console.WriteLine("Select what you want to  do");
            Console.WriteLine("");
            /*
            foreach (var menuItem in menuItems)
            {
                Console.WriteLine(menuItem);

            }
            */

            for (int i = 0; i < menuItems.Length; i++)
            {
                //Console.WriteLine((i +1) + ":" + menuItems[i]);
                Console.WriteLine($"{(i + 1)}:{menuItems[i]}");

            }

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > menuItems.Length)
            {
                Console.WriteLine("You need to select a menu item");

            }
            Console.WriteLine("Selection " + selection);
            return selection;
        }

        private void UpdateOwner(string input, Owner editedOwner,UpdateTypes type)
        {
            string text = input;
            Console.WriteLine($" edit {input}");
            Console.Write($" write new {input}");
            input = Console.ReadLine();

            while (input == null || input.Length <= 0)
            {
                Console.Write($"{text} has to have a length higher then 0:");
                input = Console.ReadLine();
            }

            switch (type)
            {

                case UpdateTypes.FirstName:
                   //editedOwner = selectedOwner;
                    editedOwner.FirstName = input;
                    break;
                case UpdateTypes.LastName:
                    //editedOwner = selectedOwner;
                    editedOwner.LastName = input;
                    break;
                case UpdateTypes.Email:
                    //editedOwner = selectedOwner;
                    editedOwner.Email = input;
                    break;

                case UpdateTypes.Address:
                    //editedOwner = selectedOwner;
                    editedOwner.Address = input;
                    break;

                case UpdateTypes.Phonenumber:
                    //editedOwner = selectedOwner;
                    editedOwner.PhoneNumber = input;
                    break;



            }

            if (_ownerService.EditOwner(editedOwner) != null)
            {
                Console.WriteLine("the update was successful");
                Console.WriteLine(_ownerService.GetOwnerById(editedOwner.Id));
                Console.ReadLine();

            }
            else
            {
                Console.Write("the update was unsuccessful");
                Console.ReadLine();
            }
        }
    }
}
