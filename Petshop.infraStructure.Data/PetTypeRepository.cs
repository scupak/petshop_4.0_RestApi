using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.infraStructure.Data
{
    public class PetTypeRepository : IPetTypeRepository
    {
        public PetType AddPetType(PetType petType)
        {
            return FakeDB.AddPetType(petType);
        }

        public PetType EditPetType(PetType petType, int index)
        {
            FakeDB._PetTypes[index] = petType;
            return FakeDB._PetTypes[index];
        }

        public List<PetType> GetPetTypes()
        {
            return FakeDB._PetTypes;
        }

        public FilteredList<PetType> GetPetTypes(Filter filter)
        {

            var filteredList = new FilteredList<PetType>();

            filteredList.TotalCount = FakeDB._PetTypes.Count;
            filteredList.FilterUsed = filter;

            IEnumerable<PetType> filtering = FakeDB._PetTypes;

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
            throw new NotImplementedException();
        }

        public PetType DeletePetType(int id)
        {
            throw new NotImplementedException();
        }
    }
}
