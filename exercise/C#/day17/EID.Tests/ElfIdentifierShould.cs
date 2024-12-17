using FluentAssertions;
using FluentAssertions.LanguageExt;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace EID.Tests
{
    public class ElfIdentifierShould
    {
        [Theory]
        [InlineData("19845606")]
        [InlineData("30600233")]
        [InlineData("29999922")]
        [InlineData("11111151")]
        [InlineData("19800767")]
        public void ParseElfIdentifier(string potentialElfIdentifier) =>
            ElfIdentifier.Parse(potentialElfIdentifier).Should().BeRight();

        public class Fail
        {
            private Gen<char> GenerateADigit() => Gen.Choose('0', '9').Select(x => (char)x);
            private Gen<char> GenerateNotADigit() => Arb.Default.Char().Filter(x => !char.IsDigit(x)).Generator;

            private Arbitrary<string> InvalidLengthString() => Arb.Default.String()
                .Filter(s => s != null && s.Length != 8);

            [Property]
            public Property ToParseWhenLenghtIsNot8() =>
                Prop.ForAll(
                    InvalidLengthString(),
                    potentialElfIdentifier => ElfIdentifier.Parse(potentialElfIdentifier)
                        .Should().BeLeft()
                        .Which.Message.Should().Be("ElfIdentifier must be 8 characters long"));


            private Arbitrary<string> StringContainingAtLeastANonDigitCharacter() =>
                Arb.Default.String()
                    .Filter(s => s != null && s.Any(c => !char.IsDigit(c)));

            [Property]
            public Property ToParseWhenNotAllNumbersAreDigit() =>
                Prop.ForAll(
                    StringContainingAtLeastANonDigitCharacter(),
                    potentialElfIdentifier => ElfIdentifier.Parse(potentialElfIdentifier)
                        .Should().BeLeft());

            private Arbitrary<string> ValidStringWithUnknownSex() =>
            (
                from invalidFirstDigit in GenerateADigit().Where(x => x != '1' && x != '2' && x != '3')
                from remainingNumbers in GenerateADigit().ListOf(7)
                select $"{invalidFirstDigit}{new string(remainingNumbers.ToArray())}"
            ).ToArbitrary();

            private Gen<Char> ValidSexCharGen() => Gen.Elements('1', '2', '3');

            [Property]
            public Property ToParseWhenSexIsInvalid() =>
                Prop.ForAll(ValidStringWithUnknownSex(),
                    potentialElfIdentifier => ElfIdentifier.Parse(potentialElfIdentifier)
                        .Should().BeLeft()
                        .Which.Message.Should()
                        .EndWith("Sex must be either 1 for Sloubi, 2 for Gagna  or 3 for Catact"));


            public Arbitrary<string> InvalidCharactersInBirthYear() =>
            (
                from sexChar in ValidSexCharGen()
                from yearOfBirth in GenerateNotADigit().ListOf(2)
                from remaining in GenerateADigit().ListOf(5)
                select $"{sexChar}{new string(yearOfBirth.ToArray())}{new string(remaining.ToArray())}"
            ).ToArbitrary();

            [Property]
            public Property ToParseWhenBirthYearNotANumber()
            {
                return Prop.ForAll(InvalidCharactersInBirthYear(),
                    potentialElfIdentifier => ElfIdentifier.Parse(potentialElfIdentifier)
                        .Should().BeLeft()
                        .Which.Message.Should().EndWith("BirthYear must be 2 digits long"));
            }
            
            private Arbitrary<string> InvalidCharactersInBirthOrder() =>
            (
                from sexChar in ValidSexCharGen()
                from yearOfBirth in GenerateADigit().ListOf(2)
                from birthOrder in GenerateNotADigit().ListOf(3)
                from remaining in GenerateADigit().ListOf(2)
                select $"{sexChar}{new string(yearOfBirth.ToArray())}{new string(birthOrder.ToArray())}{new string(remaining.ToArray())}"
            ).ToArbitrary();

            [Property]
            public Property ToParseWhenBirthOrderNotANumber() =>
                Prop.ForAll(InvalidCharactersInBirthOrder(),
                    potentialElfIdentifier => ElfIdentifier.Parse(potentialElfIdentifier)
                        .Should().BeLeft()
                        .Which.Message.Should().EndWith("BirthOrder must be 3 digits long"));

            private Arbitrary<string> ValidStringWithInvalidBirthOrder() =>
            (
                from sexChar in ValidSexCharGen()
                from yearOfBirth in GenerateADigit().ListOf(2)
                from remaining in GenerateADigit().ListOf(2)
                select $"{sexChar}{new string(yearOfBirth.ToArray())}000{new string(remaining.ToArray())}"
            ).ToArbitrary();


            [Property]
            public Property ToParseWhenBirthOrderIsInvalidValue() =>
                Prop.ForAll(ValidStringWithInvalidBirthOrder(),
                    potentialElfIdentifier => ElfIdentifier.Parse(potentialElfIdentifier).Should().BeLeft()
                        .Which.Message.Should().Be("BirthOrder must be between 1 and 999"));

            [Theory]
            [InlineData("19912378")]
            public void ToParseWhenControlKeyIsInvalid(string potentialElfIdentifier) =>
                ElfIdentifier.Parse(potentialElfIdentifier).Should().BeLeft()
                    .Which.Message.Should().Be("ControlKey is invalid");
        }
    }
}