using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using WaProject.Domain.Entities;

namespace WaProject.Infra.Data.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public override int SaveChanges()
        {
            PopulateAuditFields();
            return base.SaveChanges();
        }

        private void PopulateAuditFields()
        {
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is BaseEntity))
            {
                var actualItem = (BaseEntity)item.Entity;
                actualItem.CreatedDate = DateTime.Now;
            }

            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Modified || e.Collections.Any(c => c.IsModified))))
            {
                var actualItem = (BaseEntity)item.Entity;
                Entry(actualItem).Property(p => p.CreatedDate).IsModified = false;
            }
        }

        public DbSet<Job> Job { get; set; }
    }
}
