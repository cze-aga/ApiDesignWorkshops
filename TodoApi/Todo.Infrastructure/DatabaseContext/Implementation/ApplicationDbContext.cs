namespace Todo.Infrastructure.DatabaseContext.Implementation
{
    internal class ApplicationDbContext : BaseTodoDbContext
    {
        public ApplicationDbContext(string connectionString) 
            : base(connectionString)
        {
        }
    }
}
