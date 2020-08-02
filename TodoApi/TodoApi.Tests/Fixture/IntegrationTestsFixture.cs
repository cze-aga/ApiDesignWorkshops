using System;
using System.Net.Http;

using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc.Testing;

namespace TodoApi.Tests.Fixture
{
    public sealed class IntegrationTestsFixture : IDisposable
    {
        private WebApplicationFactory<Startup> webApplicationFactory;

        private DatabaseManager manager;

        public IntegrationTestsFixture()
        {
            webApplicationFactory = new TestWebApplicationFactory();

            var lifetimeScope = webApplicationFactory.Services.GetAutofacRoot();

            manager = new DatabaseManager(lifetimeScope);
        }

        internal void ClearDatabase() => manager.ClearDatabase();

        internal HttpClient CreateClient() =>
            webApplicationFactory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

        public void Dispose()
        {
            manager.Dispose();
            webApplicationFactory.Dispose();
        }
    }
}
