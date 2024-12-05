using FluentAssertions;
using Xunit;

namespace EID.Tests
{
    public class ElfIDValidatorShould
    {
        [Theory]
        [InlineData("32403443")]
        [InlineData("22499941")]
        [InlineData("19845606")]
        [InlineData("30600233")]
        [InlineData("29999922")]
        [InlineData("11111151")]
        [InlineData("19800767")]
        public void ParseAValidElfId(string potentialElfId) =>
            ElfIdValidator.Validate(potentialElfId)
                .Should().BeTrue();
        public class Fail
        {
            [Theory]
            [InlineData(null, "Should not be null")]
            [InlineData("", "Should have a lenght of 8")]
            [InlineData("40003258", "Incorrect Sex")]
            [InlineData("00003258", "Incorrect Sex")]
            [InlineData("1ab14599", "Incorrect Birthdate")]
            [InlineData("19814x08", "Incorrect serial number")]
            [InlineData("19800023", "Incorrect serial number")]
            [InlineData("19912378", "Incorrect control")]
            [InlineData("199123lj", "Incorrect control")]
            public void ToParseAnInvalidElfId(string potentialElfId, string reason) =>
                ElfIdValidator.Validate(potentialElfId)
                    .Should().BeFalse(reason);
        }
    }
}