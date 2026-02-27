using ElRawabi_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ElRawabi_Backend.Data
{
    public class ElRawabiDbContext : DbContext
    {
        public ElRawabiDbContext(DbContextOptions<ElRawabiDbContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Apartment> Apartments { get ; set; }
        public DbSet<BuildingTimeLine> BuildingsTimeLine { get; set; }
        public DbSet<BuildingImg> BuildingImgs { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.HasIndex(a => a.Area);
                entity.Property(a => a.Area)
                      .HasPrecision(18, 2);

                entity.Property(a => a.PricePerMeter)
                      .HasPrecision(18, 2);
            });


            modelBuilder.Entity<Building>()
                .HasOne(b => b.Project)
                .WithMany(p => p.Buildings)
                .HasForeignKey(b => b.ProjectId);

            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.Building)
                .WithMany(b => b.Apartments)
                .HasForeignKey(a => a.BuildingId);

            modelBuilder.Entity<Apartment>()
               .Property(a => a.Area)
               .HasPrecision(18, 2);

            modelBuilder.Entity<BuildingTimeLine>()
                .HasOne(bt => bt.Building)
                .WithMany(b => b.buildingTimeLines)
                .HasForeignKey(bt => bt.BuildingId);

            modelBuilder.Entity<BuildingImg>()
                .HasOne(bi => bi.Building)
                .WithMany(b => b.BuildingImgs)
                .HasForeignKey(bi => bi.BuildingId);

            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.User)
                .WithMany(u => u.Apartments)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
