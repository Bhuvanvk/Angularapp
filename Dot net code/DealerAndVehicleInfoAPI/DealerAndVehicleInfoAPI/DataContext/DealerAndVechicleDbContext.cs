using DealerAndVehicleInfoAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace DealerAndVehicleInfoAPI.DataContext
{
    public class DealerAndVechicleDbContext:DbContext
    {
        
        public DealerAndVechicleDbContext(DbContextOptions<DealerAndVechicleDbContext> options)
        : base(options)
        {
        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<DealerToVehicle> DealerToVehicle { get; set; }
        public DbSet<APIDataLogEntry> APIDataLogEntry { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DealerToVehicle>()
                .HasKey(vdm => new { vdm.VehicleId, vdm.DealerId });

            modelBuilder.Entity<DealerToVehicle>()
                .HasOne(vdm => vdm.Vehicle)
                .WithMany(v => v.DealerToVehicle)
                .HasForeignKey(vdm => vdm.VehicleId);

            modelBuilder.Entity<DealerToVehicle>()
                .HasOne(vdm => vdm.Dealer)
                .WithMany(d => d.DealerToVehicle)
                .HasForeignKey(vdm => vdm.DealerId);
        }

    }
}
