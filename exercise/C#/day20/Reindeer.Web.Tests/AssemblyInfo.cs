// Set the orderer
[assembly: TestCollectionOrderer("Reindeer.Web.Tests.ConsumerOtherProducerTestOrderer", "Reindeer.Web.Tests")]

// Need to turn off test parallelization so we can validate the run order
[assembly: CollectionBehavior(DisableTestParallelization = true)]