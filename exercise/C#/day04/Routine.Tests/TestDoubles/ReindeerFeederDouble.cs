namespace Routine.Tests.TestDoubles;

public class ReindeerFeederDouble : IReindeerFeeder
{
    public bool WasFeedReindeersCalled { get; private set; }
    public void FeedReindeers() => WasFeedReindeersCalled = true;
}