using System.Data.Common;

using Microsoft.EntityFrameworkCore;

using Todo.Infrastructure.DatabaseContext;
using Todo.Model;

namespace Todo.Tests.Fixture.EF
{
    internal class TestDbContext : BaseTodoDbContext
    {
        public TestDbContext()
            : base(DatabaseManager.ConnectionString)
        {
        }

        protected override void OnConfiguringImpl(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite((DbConnection)DatabaseManager.Connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().ToTable("Tasks");
            modelBuilder.Entity<Task>(entity => entity.HasKey(e => e.Id));
            base.OnModelCreating(modelBuilder);
        }
    }
}
