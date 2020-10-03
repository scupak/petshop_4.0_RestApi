using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.Infrastructure.Db.Data.Repositories
{
    public class ColourRepository : IColourRepository
    {

        readonly Context _context;

        public ColourRepository(Context context)
        {
            _context = context;
        }

        public Colour AddColor(Colour color)
        {
            throw new NotImplementedException();
        }

        public FilteredList<Colour> GetColors(Filter filter)
        {
            throw new NotImplementedException();
        }
    }
}
