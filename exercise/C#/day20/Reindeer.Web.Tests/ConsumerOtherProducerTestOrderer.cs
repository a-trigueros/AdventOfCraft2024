using Xunit.Abstractions;

namespace Reindeer.Web.Tests;

// ReSharper disable once UnusedType.Global
public class ConsumerOtherProducerTestOrderer : ITestCollectionOrderer
{
    const string Consumer = "Consumer";
    const string Producer = "Producer";

    public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
    {
        var collections = testCollections as ITestCollection[] ?? testCollections.ToArray();
        return GetConsumers(collections)
            .Concat(GetOther(collections))
            .Concat(GetProducers(collections));
    }

    private IEnumerable<ITestCollection> GetOther(IEnumerable<ITestCollection> testCollections) =>
        testCollections.Where(x =>
            !Is(Consumer)(x)
            && !Is(Producer)(x));

    private IEnumerable<ITestCollection> GetProducers(IEnumerable<ITestCollection> testCollections) => 
        testCollections.Where(Is(Producer));

    private IEnumerable<ITestCollection> GetConsumers(IEnumerable<ITestCollection> testCollections) => 
        testCollections.Where(Is(Consumer));

    private static Func<ITestCollection, bool> Is(string consumer) => 
        x => x.DisplayName.Contains(consumer);
}