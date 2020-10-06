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
        {/**
          * if (address.City != null)
            {
                _ctx.Attach(address.City).State = EntityState.Unchanged;
            }
            var addressEntry = _ctx.Add(address);
            _ctx.SaveChanges();
            return addressEntry.Entity;

            /**
             *  return FakeDB.AddPet(pet);
             */


            if (pet.PetType != null)
            {
                _context.Attach(pet.PetType).State = EntityState.Unchanged;
            }

            if (pet.Owner != null)
            {

                _context.Attach(pet.Owner).State = EntityState.Unchanged;
            }


            var returnPet =_context.Pets.Add(pet).Entity;
            _context.SaveChanges();
            return returnPet;

        }

        public Pet EditPet(Pet editpet, int index)
        {
            /**
             * FakeDB._pets[index] = pet;
               return FakeDB._pets[index];
             */
            if (editpet.PetType != null)
            {
                _context.Attach(editpet.PetType).State = EntityState.Unchanged;
            }

            if (editpet.Owner != null)
            {

                _context.Attach(editpet.Owner).State = EntityState.Unchanged;
            }
            else
            {
                _context.Entry(editpet).Reference(pet => pet.Owner).IsModified = true;
            }

            var returnPet = _context.Pets.Update(editpet).Entity;
            _context.SaveChanges();
            return returnPet;

        }

        public Pet DeletePet(int id)
        {
            Pet pet = _context.Pets.ToList().Find(x => x.PetId == id);
            if (pet != null)
            {
                var returnPet = _context.Pets.Remove(pet).Entity;
                _context.SaveChanges();
                return returnPet;

            }

            throw new KeyNotFoundException("Could not find a pet to delete ");
        }

        public FilteredList<Pet> GetPets(Filter filter)
        {


            var filteredList = new FilteredList<Pet>();

            filteredList.TotalCount = _context.Pets.Count();
            filteredList.FilterUsed = filter;
            filteredList.TotalCount = _context.Pets.Count();

            if (filter.CurrentPage == 0)
            {
                filter.CurrentPage = 1;
            }

            if (filter.ItemsPrPage == 0)
            {
                filter.ItemsPrPage = 10;
            }


            IEnumerable<Pet> filtering = _context
                .Pets.AsNoTracking()
                .Include(p => p.PetType)
                .Include(p => p.Owner)
                .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                .Take(filter.ItemsPrPage)
                .ToList();


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
            Pet pet = _context.Pets.Include(p => p.ColourPets).ThenInclude(colorPet => colorPet.Colour).ToList().Find(x => x.PetId == id);

            if (pet == null)
            {
                return null;

            }

            Pet temppet = new Pet
            {
                Owner = pet.Owner,
                PetType = pet.PetType,
                Name = pet.Name,
                Birthdate = pet.Birthdate,
                Color = pet.Color,
                PetId = pet.PetId,
                Price = pet.Price,
                SoldDate = pet.SoldDate,
                ColourPets = pet.ColourPets



            };

            /*
            temppet.Owner = _ownerRepository.GetOwners().Find(x => x.ColourId == pet.Owner.ColourId);

            temppet.PetType = _petTypeRepository.GetPetTypes().Find(x => x.ColourId == pet.PetType.ColourId);
            */
            return temppet;

        }


        
    }
}
