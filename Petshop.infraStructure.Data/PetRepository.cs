using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Petshop.Core.Filter;

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

        public Pet DeletePet(int id)
        {
            Pet pet = FakeDB._pets.Find(x => x.Id == id);
            if (pet != null)
            {
                FakeDB._pets.Remove(pet);
                return pet;

            }
            else
            {
                throw new KeyNotFoundException("Could not find a pet to delete ");
            }
        }

        public FilteredList<Pet> GetPets(Filter filter)
        {

            var filteredList = new FilteredList<Pet>();

            filteredList.TotalCount = FakeDB._pets.Count;
            filteredList.FilterUsed = filter;

            IEnumerable<Pet> filtering = FakeDB._pets;

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                switch (filter.SearchField.ToLower())
                {
                    case "name":
                        filtering = filtering.Where(c => c.Name.Contains(filter.SearchText));
                        break;
                    case "color":
                        filtering = filtering.Where(c => c.Color.Contains(filter.SearchText));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(filter.OrderDirection) && !string.IsNullOrEmpty(filter.OrderProperty))
            {
                var prop = typeof(Pet).GetProperty(filter.OrderProperty);
                filtering = "ASC".Equals(filter.OrderDirection) ?
                    filtering.OrderBy(c => prop.GetValue(c, null)) :
                    filtering.OrderByDescending(c => prop.GetValue(c, null));

            }

            filteredList.List = filtering.ToList();
            return filteredList;
        }

        public Pet GetPetById(int id)
        {
            throw new NotImplementedException();
        }

        public FilteredList<Pet> GetPets()
        {
            var filteredList = new FilteredList<Pet>();

            filteredList.List = FakeDB._pets;
            return filteredList;
        }
    }
}
