using Microsoft.EntityFrameworkCore;

namespace Todo.Infrastructure.DatabaseContext.Implementation
{
    internal class ApplicationDbContext : BaseTodoDbContext
    {
        public ApplicationDbContext(string connectionString) 
            : base(connectionString)
        {
        }

        protected override void OnConfiguringImpl(DbContextOptionsBuilder builder) =>
            builder.UseSqlServer(ConnectionString, provider => provider.CommandTimeout(60));
    }
}
