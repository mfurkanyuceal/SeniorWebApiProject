using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeniorWebApiProject.Domain.LocationModels;
using SeniorWebApiProject.Domain.UserModels;
using SeniorWepApiProject.Domain.AppUserModels;
using SeniorWepApiProject.Domain.Swap;

namespace SeniorWepApiProject.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Swap> Swaps { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<Message> Messages { get; set; }
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

            //Address One to Many Swaps
            modelBuilder.Entity<Address>()
                .HasMany(c => c.Swaps)
                .WithOne(e => e.Address);

            //City One to Many Districts
            modelBuilder.Entity<City>()
                .HasMany(c => c.Districts)
                .WithOne(e => e.City);

            //District One to Many Neigborhoods
            modelBuilder.Entity<District>()
                .HasMany(c => c.Neighborhoods)
                .WithOne(e => e.District);


            //Address One to One City
            modelBuilder.Entity<Address>()
                .HasOne(a => a.City)
                .WithOne(b => b.Address)
                .HasForeignKey<City>(b => b.AddressId);

            //Address One to One District
            modelBuilder.Entity<Address>()
                .HasOne(a => a.District)
                .WithOne(b => b.Address)
                .HasForeignKey<District>(b => b.AddressId);

            //Address One to One Neighborhood
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Neighborhood)
                .WithOne(b => b.Address)
                .HasForeignKey<Neighborhood>(b => b.AddressId);

            //AppUser One to Many Address
            modelBuilder.Entity<AppUser>()
                .HasMany(c => c.Addresses)
                .WithOne(e => e.AppUser);

            //Messages One to Many
            modelBuilder.Entity<AppUser>()
                .HasMany(c => c.OutgoingMessages)
                .WithOne(e => e.SenderUser);
            modelBuilder.Entity<AppUser>()
                .HasMany(c => c.InComingMessages)
                .WithOne(e => e.RecieverUser);


            //Swaps One to Many
            modelBuilder.Entity<AppUser>()
                .HasMany(c => c.OutgoingSwaps)
                .WithOne(e => e.SenderUser);
            modelBuilder.Entity<AppUser>()
                .HasMany(c => c.InComingSwaps)
                .WithOne(e => e.RecieverUser);

            //Ability Many to Many
            modelBuilder.Entity<UserAbility>()
                .HasKey(bc => new {bc.UserId, bc.AbilityId});
            modelBuilder.Entity<UserAbility>()
                .HasOne(bc => bc.Ability)
                .WithMany(b => b.UserAbilities)
                .HasForeignKey(bc => bc.AbilityId);
            modelBuilder.Entity<UserAbility>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.UserAbilities)
                .HasForeignKey(bc => bc.UserId);

            //Fancy Many to Many
            modelBuilder.Entity<UserFancy>()
                .HasKey(bc => new {bc.UserId, bc.FancyId});
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