using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.Entity
{
    public class PetType
    {
        public int Id { get; set; }

        public string name { get; set; }

        public List<Pet> Pets { get; set; }

    }
}
