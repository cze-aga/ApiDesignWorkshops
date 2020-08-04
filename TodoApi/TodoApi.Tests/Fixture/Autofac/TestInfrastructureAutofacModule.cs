using Autofac;

using Microsoft.EntityFrameworkCore;

using Todo.Infrastructure;
using Todo.Tests.Fixture.EF;

namespace Todo.Tests.Fixture.Autofac
{
    public class TestInfrastructureAutofacModule : InfrastructureAutofacModule
    {
        public TestInfrastructureAutofacModule()
            : base(DatabaseManager.ConnectionString)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestDbContext>()
                .As<DbContext>()
                .SingleInstance();
        }
    }
}
