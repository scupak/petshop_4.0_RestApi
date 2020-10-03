using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Text;
using Petshop.Core.Entity;
using Petshop.Core.Filter;

namespace Petshop.core.DomainServices
{
  public  interface IColourRepository
    {
      public Colour AddColor(Colour color);

      public FilteredList<Colour> GetColors(Filter filter);
    }
}
