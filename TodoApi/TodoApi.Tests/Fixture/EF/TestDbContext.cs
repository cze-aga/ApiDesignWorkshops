using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Todo.Infrastructure.DatabaseContext;
using Todo.Model;

namespace TodoApi.Tests.Fixture.EF
{
    internal class TestDbContext : BaseTodoDbContext
    {
        public TestDbContext()
            : base(DatabaseManager.ConnectionString)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                ConnectionString,
                options => options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().ToTable("Tasks");
            modelBuilder.Entity<Task>(entity => entity.HasKey(e => e.Id));
            base.OnModelCreating(modelBuilder);
        }
    }
}
