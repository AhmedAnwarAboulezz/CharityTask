using Common.StandardInfrastructure;
using Orders.Data.Entities;
using Orders.Data.SeedData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Data
{
    public class OrdersContext : DbContext
    {
        private readonly IDataInitialize _dataInit;
        public OrdersContext(DbContextOptions<OrdersContext> options, IDataInitialize dataInit) : base(options)
        {
            _dataInit = dataInit;

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(_dataInit.AddCountries());
            modelBuilder.Entity<Gender>().HasData(_dataInit.AddGenders());
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Country> Countries { get; set; }
       
        public virtual DbSet<Gender> Gender { get; set; }
       
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged) continue;
                foreach (var property in entry.Properties.Where(q => Constants.PropertiesList.Contains(q.Metadata.Name)))
                {
                    var propertyName = property.Metadata.Name;
                    property.CurrentValue = entry.State switch
                    {
                        EntityState.Added => (propertyName switch
                        {
                            "CreatedDate" => DateTime.UtcNow,
                            _ => property.CurrentValue
                        }),
                        EntityState.Modified => (propertyName switch
                        {
                            "ModifiedDate" => DateTime.UtcNow,
                            _ => property.CurrentValue
                        }),
                        _ => property.CurrentValue
                    };
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        

    }
}
