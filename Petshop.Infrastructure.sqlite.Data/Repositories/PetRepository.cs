﻿using System;
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
            Pet pet = _context.Pets.ToList().Find(x => x.Id == id);
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

            IEnumerable<Pet> filtering = _context.Pets.AsNoTracking().Include(p => p.PetType).Include(p => p.Owner).ToList();

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
            Pet pet = _context.Pets.ToList().Find(x => x.Id == id);

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
                Id = pet.Id,
                Price = pet.Price,
                SoldDate = pet.SoldDate,



            };

            /*
            temppet.Owner = _ownerRepository.GetOwners().Find(x => x.Id == pet.Owner.Id);

            temppet.PetType = _petTypeRepository.GetPetTypes().Find(x => x.Id == pet.PetType.Id);
            */
            return temppet;

        }
    }
}
