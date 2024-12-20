using FluentAssertions;
using Xunit;
using static Travel.SantaTravelCalculator;

namespace Travel.Tests
{
    public class CalculatorTests
    {
        public static IEnumerable<object[]> ComputationDistanceData()
        {
            yield return new object[] { 1, 1 };
            yield return new object[] { 2, 3 };
            yield return new object[] { 5, 31 };
            yield return new object[] { 10, 1_023 };
            yield return new object[] { 20, 1_048_575 };
            yield return new object[] { 30, 1_073_741_823 };
        }

        [Theory]
        [MemberData(nameof(ComputationDistanceData))]
        public void Should_Calculate_The_DistanceRecursively_For(int numberOfReindeers, int expectedDistance)
            => CalculateTotalDistanceRecursively(numberOfReindeers)
                .Should()
                .Be(expectedDistance);


        [Theory]
        [MemberData(nameof(ComputationDistanceData))]
        public void Should_Calculate_The_DistanceInALoop_For(int numberOfReindeers, int expectedDistance)
            => CalculateTotalDistanceInALoop(numberOfReindeers)
                .Should()
                .Be(expectedDistance);

        
        [Theory]
        [MemberData(nameof(ComputationDistanceData))]
        public void Should_Calculate_The_DistanceUsingLinq_For(int numberOfReindeers, int expectedDistance)
            => CalculateTotalDistanceUsingLinq(numberOfReindeers)
                .Should()
                .Be(expectedDistance);
        
        [Theory]
        [MemberData(nameof(ComputationDistanceData))]
        public void Should_Calculate_The_DistanceDirectly_For(int numberOfReindeers, int expectedDistance)
            => CalculateTotalDistanceDirectly(numberOfReindeers)
                .Should()
                .Be(expectedDistance);

        [Theory]
        [InlineData(32)]
        [InlineData(50)]
        public void Fail_For_Numbers_Greater_Than_32(int numberOfReindeers)
            // TODO find a way to support those values greater than 32
            // I expect a distance of 1 125 899 906 842 623 for 50 reindeers 
            => ((Func<int>?)(() => CalculateTotalDistanceRecursively(numberOfReindeers)))
                .Should()
                .Throw<OverflowException>();
    }
}