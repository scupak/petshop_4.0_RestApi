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
   public class PetTypeRepository : IPetTypeRepository
    {
        readonly Context _context;

        public PetTypeRepository(Context context)
        {
            _context = context;
        }

        public PetType AddPetType(PetType petType)
        {
            var returnPetType = _context.PetTypes.Add(petType).Entity;
            _context.SaveChanges();
            return returnPetType;
        }

        public PetType EditPetType(PetType petType, int index)
        {
            PetType returnPetType = _context.PetTypes.Update(petType).Entity;

            _context.SaveChanges();

            return returnPetType;
        }

        public FilteredList<PetType> GetPetTypes(Filter filter)
        {
            var filteredList = new FilteredList<PetType>();

            filteredList.TotalCount = _context.PetTypes.ToList().Count;
            filteredList.FilterUsed = filter;

            IEnumerable<PetType> filtering = _context.PetTypes.AsNoTracking().ToList();

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                switch (filter.SearchField.ToLower())
                {
                    case "name":
                        filtering = filtering.Where(c => c.name.Contains(filter.SearchText));
                        break;

                }
            }

            if (!string.IsNullOrEmpty(filter.OrderDirection) && !string.IsNullOrEmpty(filter.OrderProperty))
            {
                var prop = typeof(PetType).GetProperty(filter.OrderProperty);
                filtering = "ASC".Equals(filter.OrderDirection) ?
                    filtering.OrderBy(c => prop.GetValue(c, null)) :
                    filtering.OrderByDescending(c => prop.GetValue(c, null));

            }

            filteredList.List = filtering.ToList();
            return filteredList;
        }

        
        public PetType GetPetTypeById(int id)
        {
            PetType petType = _context.PetTypes.AsNoTracking().ToList().Find(x => x.Id == id);

            PetType temPetType = new PetType
            {
                name = petType.name,
                Id = petType.Id



            };

            temPetType.Pets = _context.Pets.AsNoTracking().Include(p => p.PetType).ToList().Where(pet => pet.PetType.Id == petType.Id).ToList();


            return temPetType;
        }

        public PetType DeletePetType(int id)
        {
            PetType petType = _context.PetTypes.ToList().Find(x => x.Id == id);
            if (petType != null)
            {
                var returnPetType = _context.PetTypes.Remove(petType).Entity;
                _context.SaveChanges();
                return returnPetType;

            }

            throw new KeyNotFoundException("Could not find a PetType to delete ");
        }
    }
}
