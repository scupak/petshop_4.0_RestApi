using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Petshop.Core.Entity;

namespace Petshop.Infrastructure.Db.Data
{
  public  class Context : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<PetColor> PetColors { get; set; }

        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>()
                .HasOne(pet => pet.PetType)
                .WithMany(petType => petType.Pets)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Pet>()
                .HasOne(pet => pet.Owner)
                .WithMany(owner => owner.Pets)
                .OnDelete(DeleteBehavior.SetNull);



        }
    }
        
}
