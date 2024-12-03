using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanguageExt;

namespace Games
{
    public class FizzBuzz
    {
        private readonly IDictionary<int, string> _mapping;

        public FizzBuzz(IDictionary<int, string> mapping)
        {
            _mapping = mapping
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public const int Min = 1;
        public const int Max = 100;


        private static string ConvertSafely(int input, IDictionary<int, string> mapping)
        {
            var sb = mapping
                .Where(p => Is(p.Key, input))
                .Map(kvp => kvp.Value)
                .Aggregate(new StringBuilder(), (sb, s) => sb.Append(s));
            return sb.Length > 0 ? sb.ToString() : input.ToString();
        }

        private static bool Is(int divisor, int input) => input % divisor == 0;

        private static bool IsOutOfRange(int input) => input is < Min or > Max;

        public Option<string> Convert(int input)
            => IsOutOfRange(input)
                ? Option<string>.None
                : ConvertSafely(input, _mapping);
    }
}