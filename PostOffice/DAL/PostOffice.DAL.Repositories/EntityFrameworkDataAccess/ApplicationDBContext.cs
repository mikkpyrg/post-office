using Microsoft.EntityFrameworkCore;
using PostOffice.DAL.DataModels.Entity;

namespace PostOffice.DAL.Repositories.EntityFrameworkDataAccess
{
	public class ApplicationDBContext : DbContext
	{
		public DbSet<Shipment> Shipment { get; set; }
		public DbSet<Parcel> Parcel { get; set; }
		public DbSet<Bag> Bag { get; set; }
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) 
		{
			modelBuilder.Entity<Parcel>()
				.HasIndex(x => x.ParcelNumber)
				.IsUnique();

			modelBuilder.Entity<Bag>()
				.HasIndex(x => x.BagNumber)
				.IsUnique();

			modelBuilder.Entity<Shipment>()
				.HasIndex(x => x.ShipmentNumber)
				.IsUnique();
		}

	}
}
