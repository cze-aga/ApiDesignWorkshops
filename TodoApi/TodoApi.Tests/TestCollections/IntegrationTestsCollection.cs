using TodoApi.Tests.Fixture;
using TodoApi.Tests.TestCollections.Names;

using Xunit;

namespace TodoApi.Tests.TestCollections
{
    [CollectionDefinition(CollectionNames.IntegrationTests)]
    public class IntegrationTestsCollection : ICollectionFixture<IntegrationTestsFixture>
    {
    }
}
