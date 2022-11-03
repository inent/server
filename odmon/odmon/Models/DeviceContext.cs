using Microsoft.EntityFrameworkCore;

namespace odmon.Models
{
	public class DeviceContext : DbContext
    {
        public DeviceContext(DbContextOptions<DeviceContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Alert> Alerts { get; set; }

        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<Recent> Recents { get; set; }

        public DbSet<AlertList> AlertLists { get; set; }

        public DbSet<AlertUser> AlertUsers { get; set; }

        public DbSet<GeoCode> GeoCodes { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Attrib> Attribs { get; set; }
        public DbSet<Device> Devices { get; set; }

        public DbSet<IDevice> IDevices { get; set; }
        public DbSet<JDevice> JDevices { get; set; }

        public DbSet<Coverage> Coverages { get; set; }
        public DbSet<DevMoni> DevMonis { get; set; }
        public DbSet<DevBound> DevBounds { get; set; }
        public DbSet<DevChart> DevCharts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coverage>().HasNoKey().ToView(null);
            modelBuilder.Entity<DevMoni>().HasNoKey().ToView(null);
            modelBuilder.Entity<DevBound>().HasNoKey().ToView(null);
            modelBuilder.Entity<DevChart>().HasNoKey().ToView(null);
        }

    }

}
