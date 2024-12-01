using Communication.Tests.Doubles;
using FluentAssertions;
using Xunit;
using static Communication.Tests.Builders.ReinderBuilder;

namespace Communication.Tests
{
    public class SantaCommunicatorTests
    {
        private const string Dasher = "Dasher";
        private const string NorthPole = "North Pole";
        private const int NumberOfDaysToRest = 2;
        private const int NumberOfDayBeforeChristmas = 24;
        private readonly TestLogger _logger = new();
        private readonly SantaCommunicator _communicator = new(new RestDays(NumberOfDaysToRest), new DaysUntilChrismasCounter(NumberOfDayBeforeChristmas));

        [Fact]
        public void ComposeMessage()
            => _communicator.ComposeMessage(AReinder()
                    .WithName(Dasher)
                    .WithLocation(NorthPole, 5)
                    .Build())
                .Should()
                .Be("Dear Dasher, please return from North Pole in 17 day(s) to be ready and rest before Christmas.");

        [Fact]
        public void ShouldDetectOverdueReindeer()
        {
            var overdue = _communicator.IsOverdue(AReinder()
                    .WithName(Dasher)
                    .WithLocation(NorthPole, NumberOfDayBeforeChristmas)
                    .Build(),
                _logger);

            overdue.Should().BeTrue();
            _logger.LoggedMessage().Should().Be("Overdue for Dasher located North Pole.");
        }

        [Fact]
        public void ShouldReturnFalseWhenNoOverdue()
            => _communicator.IsOverdue(
                    AReinder().WithName(Dasher)
                        .WithLocation(NorthPole, NumberOfDayBeforeChristmas - NumberOfDaysToRest - 1)
                        .Build(),
                    _logger)
                .Should()
                .BeFalse();

        [Fact]
        public void ComposeMessageSafe()
        {
            var message = _communicator.ComposeMessageSafe(AReinder()
                .WithName(Dasher)
                .WithLocation(NorthPole, 5)
                .Build());

            message.IsRight.Should().BeTrue();
            message.IfRight(m => m.Should().Be("Dear Dasher, please return from North Pole in 17 day(s) to be ready and rest before Christmas."));
        }

        [Fact]
        public void ComposeMessageOverdue()
        {
            var message = _communicator.ComposeMessageSafe(AReinder()
                .WithName(Dasher)
                .WithLocation(NorthPole, NumberOfDayBeforeChristmas)
                .Build());

            message.IsLeft.Should().BeTrue();
            message.IfLeft(m => m.Should().Be("Overdue for Dasher located North Pole."));
        }
    }
}