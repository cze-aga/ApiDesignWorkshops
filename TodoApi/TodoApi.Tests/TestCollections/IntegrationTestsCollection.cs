using Todo.Tests.Fixture;
using Todo.Tests.TestCollections.Names;

using Xunit;

namespace Todo.Tests.TestCollections
{
    [CollectionDefinition(CollectionNames.IntegrationTests)]
    public class IntegrationTestsCollection : ICollectionFixture<IntegrationTestsFixture>
    {
    }
}
