using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeniorWepApiProject.Domain;

namespace SeniorWepApiProject.DbContext
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
        public DbSet<FieldOfInterest> FieldOfInterests { get; set; }
        public DbSet<UserFieldOfInterest> UserFieldOfInterests { get; set; }

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
            modelBuilder.Entity<UserFieldOfInterest>()
                .HasKey(bc => new {bc.UserId, bc.FieldOfInterestName});
            modelBuilder.Entity<UserFieldOfInterest>()
                .HasOne(bc => bc.FieldOfInterest)
                .WithMany(b => b.UserFieldOfInterests)
                .HasForeignKey(bc => bc.FieldOfInterestName);
            modelBuilder.Entity<UserFieldOfInterest>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.UserAbilities)
                .HasForeignKey(bc => bc.UserId);
        }
    }
}