namespace Reindeer.Web.Tests.Fixtures;

[CollectionDefinition(Name)]
public class ReindeerApiCollection : ICollectionFixture<ReindeerApiFixture>
{
    public const string Name = "ReindeerApiCollection";
    
}