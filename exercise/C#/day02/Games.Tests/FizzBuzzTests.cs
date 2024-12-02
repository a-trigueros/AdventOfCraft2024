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
            var whizzBang = new FizzBuzz(new Dictionary<int, string>
            {
                { 11, "Bang" },
                { 7, "Whizz" }
            });
            yield return [FizzBuzzInstance, 1, "1"];
            yield return [FizzBuzzInstance, 67, "67"];
            yield return [FizzBuzzInstance, 82, "82"];
            yield return [FizzBuzzInstance, 3, "Fizz"];
            yield return [FizzBuzzInstance, 66, "Fizz"];
            yield return [FizzBuzzInstance, 99, "Fizz"];
            yield return [FizzBuzzInstance, 5, "Buzz"];
            yield return [FizzBuzzInstance, 50, "Buzz"];
            yield return [FizzBuzzInstance, 85, "Buzz"];
            yield return [FizzBuzzInstance, 15, "FizzBuzz"];
            yield return [FizzBuzzInstance, 30, "FizzBuzz"];
            yield return [FizzBuzzInstance, 45, "FizzBuzz"];
            yield return [whizzBang, 7, "Whizz"];
            yield return [whizzBang, 8, "8"];
            yield return [whizzBang, 11, "Bang"];
            yield return [whizzBang, 14, "Whizz"];
            yield return [whizzBang, 22, "Bang"];
            yield return [whizzBang, 77, "Bang"];
            yield return [whizzBang, 88, "Bang"];
            yield return [whizzBang, 55, "Bang"];
        }

        [Theory]
        [MemberData(nameof(FizzBuzzData))]
        public void Returns_Number_Representation(FizzBuzz fizzBuzz, int input, string expectedResult)
        {
            fizzBuzz.Convert(input)
                .Should()
                .BeSome(x => x.Should().Be(expectedResult));
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