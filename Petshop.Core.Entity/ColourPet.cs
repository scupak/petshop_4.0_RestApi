using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.Entity
{
 public class ColourPet
    {
        public int ColourId { get; set; }
        public int PetId { get; set; }

        //-----------------------------
        //Relationships

        public Colour Colour { get; set; }

        public Pet Pet { get; set; }

    }
}
