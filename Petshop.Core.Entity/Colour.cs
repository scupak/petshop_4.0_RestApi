using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.Entity
{
   public class Colour
    {
        public int ColourId { get; set; }

        public string Name { get; set; }

        public List<ColourPet> ColourPets;

    }
}
