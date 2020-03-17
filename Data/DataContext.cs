using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeniorWebApiProject.Domain.LocationModels;
using SeniorWebApiProject.Domain.UserModels;
using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Domain.IdentityModels;

namespace SeniorWepApiProject.Data
{
    public class DataContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Swap> Swaps { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Fancy> Fancies { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<UserAbility> UserAbilities { get; set; }
        public DbSet<UserFancy> UserFancies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Ability
                modelBuilder.Entity<UserAbility>()
                .HasKey(bc => new { bc.UserId, bc.AbilityId });  
            modelBuilder.Entity<UserAbility>()
                .HasOne(bc => bc.Ability)
                .WithMany(b => b.UserAbilities)
                .HasForeignKey(bc => bc.AbilityId);  
            modelBuilder.Entity<UserAbility>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.UserAbilities)
                .HasForeignKey(bc => bc.UserId);

            //Fancy
                modelBuilder.Entity<UserFancy>()
                .HasKey(bc => new { bc.UserId, bc.FancyId });  
            modelBuilder.Entity<UserFancy>()
                .HasOne(bc => bc.Fancy)
                .WithMany(b => b.UserFancies)
                .HasForeignKey(bc => bc.FancyId);  
            modelBuilder.Entity<UserFancy>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.UserFancies)
                .HasForeignKey(bc => bc.UserId);
            
        }
    }
}
