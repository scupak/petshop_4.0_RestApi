using System;

namespace Petshop.Core.Entity
{
    public class Pet
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime SoldDate { get; set; }
        public string Color { get; set; }
        public Owner Owner { get; set; }
        public double Price { get; set; }
        public PetType PetType { get; set; }

        public Pet(string name, DateTime birthdate, string color, Owner owner, double price, PetType petType, DateTime soldDate)
        {
            Name = name;
            Birthdate = birthdate;
            Color = color;
            Owner = owner;
            Price = price;
            PetType = petType;
            SoldDate = soldDate;

        }
        public Pet(string name, DateTime birthdate,string color, double price, PetType petType, DateTime soldDate)
        {
            Name = name;
            Birthdate = birthdate;
            Color = color;
            Price = price;
            PetType = petType;
            SoldDate = soldDate;

        }

        public Pet()
        {
        }

        public override string ToString()
        {
            return $"{nameof(PetId)}: {PetId}, {nameof(Name)}: {Name}, {nameof(Birthdate)}: {Birthdate}, {nameof(SoldDate)}: {SoldDate}, {nameof(Color)}: {Color}, {nameof(Owner)}: {Owner}, {nameof(Price)}: {Price}, {nameof(PetType)}: {PetType}";
        }
    }
}
