using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.LanguageExt;
using FsCheck;
using FsCheck.Xunit;
using LanguageExt;
using Xunit;

namespace Games.Tests
{
    public class FizzBuzzTests
    {
        private static readonly string[] FizzBuzzStrings = ["Fizz", "Buzz", "FizzBuzz"];

        private static readonly Dictionary<int, string> Mapping = new()
        {
            { 3, "Fizz" },
            { 5, "Buzz" }
        };

        private static FizzBuzz FizzBuzzInstance => new(Mapping);

        private static FizzBuzz WhizzBangInstance => new(new Dictionary<int, string>
        {
            { 11, "Bang" },
            { 7, "Whizz" }
        });

        public static IEnumerable<object[]> FizzBuzzData()
        {
            yield return [1, "1"];
            yield return [67, "67"];
            yield return [82, "82"];
            yield return [3, "Fizz"];
            yield return [66, "Fizz"];
            yield return [99, "Fizz"];
            yield return [5, "Buzz"];
            yield return [50, "Buzz"];
            yield return [85, "Buzz"];
            yield return [15, "FizzBuzz"];
            yield return [30, "FizzBuzz"];
            yield return [45, "FizzBuzz"];
        }


        [Theory]
        [MemberData(nameof(FizzBuzzData))]
        public void Returns_Number_Representation(int input, string expectedResult)
        {
            FizzBuzzInstance.Convert(input)
                .Should()
                .BeSome(x => x.Should().Be(expectedResult));
        }

        [Theory]
        [MemberData(nameof(WhizzBangData))]
        public void Returns_WhizzBang_Number_Representation(int input, string expectedResult)
        {
            WhizzBangInstance.Convert(input)
                .Should()
                .BeSome(x => x.Should().Be(expectedResult));
        }

        public static IEnumerable<object[]> WhizzBangData()
        {
            yield return [7, "Whizz"];
            yield return [8, "8"];
            yield return [11, "Bang"];
            yield return [14, "Whizz"];
            yield return [22, "Bang"];
            yield return [77, "WhizzBang"];
            yield return [88, "Bang"];
            yield return [55, "Bang"];
        }

        [Property]
        public Property Parse_Return_Valid_String_For_Numbers_Between_1_And_100()
            => Prop.ForAll(
                ValidInput(),
                IsConvertValid
            );

        private static Arbitrary<int> ValidInput()
            => Gen.Choose(FizzBuzz.Min, FizzBuzz.Max).ToArbitrary();

        private static bool IsConvertValid(int x)
            => FizzBuzzInstance.Convert(x).Exists(s => ValidStringsFor(x).Contains(s));

        private static IEnumerable<string> ValidStringsFor(int x)
            => FizzBuzzStrings.Append(x.ToString());

        [Property]
        public Property ParseFailForNumbersOutOfRange()
            => Prop.ForAll(
                InvalidInput(),
                x => FizzBuzzInstance.Convert(x).IsNone
            );

        private static Arbitrary<int> InvalidInput()
            => Gen.Choose(-10_000, 10_000)
                .ToArbitrary()
                .Filter(x => x is < FizzBuzz.Min or > FizzBuzz.Max);
    }
}