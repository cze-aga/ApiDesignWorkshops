using System;

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

        public DbSet<Task> Tasks { get; protected set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            OnConfiguringImpl(builder);
            base.OnConfiguring(builder);
        }

        protected abstract void OnConfiguringImpl(DbContextOptionsBuilder builder);

        protected string ConnectionString { get; }
    }
}
