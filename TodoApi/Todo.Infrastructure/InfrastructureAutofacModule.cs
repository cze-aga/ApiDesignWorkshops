using Autofac;

using Microsoft.EntityFrameworkCore;

using Todo.Infrastructure.DatabaseContext.Implementation;

namespace Todo.Infrastructure
{
    public class InfrastructureAutofacModule : Module
    {
        private readonly string connectionString;

        public InfrastructureAutofacModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new ApplicationDbContext(connectionString))
                .As<DbContext>()
                .SingleInstance();
        }
    }
}
