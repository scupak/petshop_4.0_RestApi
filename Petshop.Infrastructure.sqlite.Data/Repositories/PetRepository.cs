using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.Infrastructure.Db.Data.Repositories
{
   public class PetRepository : IPetRepository
    {
        readonly Context _context;

        public PetRepository(Context context)
        {
            _context = context;
        }

        public Pet AddPet(Pet pet)
        {
            /**
             *  return FakeDB.AddPet(pet);
             */
            var returnPet =_context.Pets.Add(pet).Entity;
            _context.SaveChanges();
            return returnPet;

        }

        public Pet EditPet(Pet pet, int index)
        {
            throw new NotImplementedException();
        }

        public Pet DeletePet(int id)
        {
            Pet pet = _context.Pets.ToList().Find(x => x.Id == id);
            if (pet != null)
            {
                var returnPet = _context.Pets.Remove(pet).Entity;
                _context.SaveChanges();
                return returnPet;

            }
            else
            {
                throw new KeyNotFoundException("Could not find a pet to delete ");
            }
        }

        public FilteredList<Pet> GetPets(Filter filter)
        {


            var filteredList = new FilteredList<Pet>();

            filteredList.TotalCount = _context.Pets.Count();
            filteredList.FilterUsed = filter;

            IEnumerable<Pet> filtering = _context.Pets.Include(p => p.PetType).ToList();

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

        public FilteredList<Pet> GetPets()
        {
            var filteredList = new FilteredList<Pet>();

            filteredList.List = _context.Pets.ToList();
            return filteredList;
        }
    }
}
