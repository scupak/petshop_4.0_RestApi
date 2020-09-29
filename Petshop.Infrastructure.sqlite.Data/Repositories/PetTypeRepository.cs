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
            throw new NotImplementedException();
        }

        public PetType EditPetType(PetType petType, int index)
        {
            throw new NotImplementedException();
        }

        public FilteredList<PetType> GetPetTypes(Filter filter)
        {
            var filteredList = new FilteredList<PetType>();

            filteredList.TotalCount = _context.PetTypes.ToList().Count;
            filteredList.FilterUsed = filter;

            IEnumerable<PetType> filtering = _context.PetTypes.ToList();

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
    }
}
