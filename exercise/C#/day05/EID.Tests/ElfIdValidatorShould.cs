using FluentAssertions;
using Xunit;

namespace EID.Tests
{
    public class ElfIdValidatorShould
    {
        public static IEnumerable<object[]> ValidElfIds()
        {
            yield return ["32403443"];
            yield return ["22499941"];
            yield return ["19845606"];
            yield return ["30600233"];
            yield return ["29999922"];
            yield return ["11111151"];
            yield return ["19800767"];
        }

        [Theory]
        [MemberData(nameof(ValidElfIds))]
        public void ParseAValidElfIdUsingTestDrivenValidator(string potentialElfId) =>
            TestDrivenElfIdValidator.Validate(potentialElfId)
                .Should().BeTrue();

        [Theory]
        [MemberData(nameof(ValidElfIds))]
        public void ParseAValidElfIdUsingRegexValidator(string potentialElfId) =>
            RegexElfIdValidator.Validate(potentialElfId)
                .Should().BeTrue();
        
        public class Fail
        {
            public static IEnumerable<object[]> InvalidElfIdsWithReason()
            {
                yield return [null!, "Should not be null"];
                yield return ["", "Should have a lenght of 8"];
                yield return ["40003258", "Incorrect Sex"];
                yield return ["00003258", "Incorrect Sex"];
                yield return ["1ab14599", "Incorrect Birthdate"];
                yield return ["19814x08", "Incorrect serial number"];
                yield return ["19800074", "Incorrect serial number"];
                yield return ["19912378", "Incorrect control"];
                yield return ["199123lj", "Incorrect control"];
            }
            
            [Theory]
            [MemberData(nameof(InvalidElfIdsWithReason))]
            public void ToParseInvalidElfIdsWithReasonUsingTestDrivenValidator(string? potentialElfId, string reason) =>
                TestDrivenElfIdValidator.Validate(potentialElfId)
                    .Should().BeFalse(reason);
            
            [Theory]
            [MemberData(nameof(InvalidElfIdsWithReason))]
            public void ToParseInvalidElfIdsWithReasonUsingRegexValidator(string? potentialElfId, string reason) =>
                RegexElfIdValidator.Validate(potentialElfId)
                    .Should().BeFalse(reason);
            
        }
    }
}