using FluentAssertions;
using Xunit;
using static GiftWish.Tests.Builder.ChildBuilder;

namespace GiftWish.Tests
{
    public class SantaServiceTests
    {
        private readonly SantaService _service = new();

        [Fact]
        public void RequestIsApprovedForNiceChildWithFeasibleGift()
        {
            _service.EvaluateRequest(AChild()
                .WhoIsNice()
                .AndWhishForAFeasibleGift()
                .Build()
            ).Should().BeTrue();
        }

        [Fact]
        public void RequestIsDeniedForNaughtyChild() =>
            _service.EvaluateRequest(AChild()
                .WhoIsNaughty()
                .AndWhishForAFeasibleGift()
                .Build()
            ).Should().BeFalse();

        [Fact]
        public void RequestIsDeniedForNiceChildWithInfeasibleGift() =>
            _service.EvaluateRequest(AChild()
                .WhoIsNice()
                .AndWishForAnInfeasibleGift()
                .Build()
            ).Should().BeFalse();
    }
}