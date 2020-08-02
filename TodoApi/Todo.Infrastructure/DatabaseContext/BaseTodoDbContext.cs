using Microsoft.EntityFrameworkCore;

using Todo.Model;

namespace Todo.Infrastructure.DatabaseContext
{
    public abstract class BaseTodoDbContext : DbContext
    {
        protected BaseTodoDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                ConnectionString,
                provider => provider.CommandTimeout(60));
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Task> Tasks { get; }

        protected string ConnectionString { get; }
    }
}
