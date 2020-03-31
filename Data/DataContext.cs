using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeniorWebApiProject.Domain.UserModels;
using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWepApiProject.Data
{
    public class DataContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Swap> Swaps { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Fancy> Fancies { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<UserAbility> UserAbilities { get; set; }
        public DbSet<UserFancy> UserFancies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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