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
            { 15, "FizzBuzz" },
            { 3, "Fizz" },
            { 5, "Buzz" }
        };
        
        private static FizzBuzz FizzBuzzInstance => new(Mapping);
        
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
        public void Returns_Number_Representation_From_Instance(int input, string expectedResult)
        {
            var fizzBuzz = new FizzBuzz(Mapping);
            fizzBuzz.Convert(input)
                .Should()
                .BeSome(x => x.Should().Be(expectedResult));
        }


        [Theory]
        [InlineData(7, "Whizz")]
        [InlineData(8, "8")]
        [InlineData(11, "Bang")]
        [InlineData(14, "Whizz")]
        [InlineData(22, "Bang")]
        [InlineData(77, "Whizz")]
        [InlineData(88, "Bang")]
        [InlineData(55, "55")]
        public void Return_Number_Representation_For_WhizzBang(int input, string representation)
        {
            var fizzBuzz = new FizzBuzz(new Dictionary<int, string>
            {
                { 11, "Bang" },
                { 7, "Whizz" }
            });
            fizzBuzz.Convert(input)
                .Should()
                .BeSome(representation);
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