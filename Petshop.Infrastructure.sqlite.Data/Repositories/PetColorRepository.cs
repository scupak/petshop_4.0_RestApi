using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.Infrastructure.Db.Data.Repositories
{
    public class PetColorRepository : IPetColorRepository
    {

        readonly Context _context;

        public PetColorRepository(Context context)
        {
            _context = context;
        }

        public PetColor AddColor(PetColor color)
        {
            throw new NotImplementedException();
        }

        public FilteredList<PetColor> GetColors(Filter filter)
        {
            throw new NotImplementedException();
        }
    }
}
