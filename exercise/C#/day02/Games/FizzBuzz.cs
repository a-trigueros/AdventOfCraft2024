using LanguageExt;

namespace Games
{
    public class FizzBuzz(Map<int, string> mapping)
    {
        public const int Min = 1;
        public const int Max = 100;
        
        private static string ConvertSafely(int input, Map<int, string> mapping)
            => mapping
                .Find(p => Is(p.Key, input))
                .Map(kvp => kvp.Value)
                .FirstOrDefault(input.ToString());

        private static bool Is(int divisor, int input) => input % divisor == 0;

        private static bool IsOutOfRange(int input) => input is < Min or > Max;

        public Option<string> Convert(int input)
            => IsOutOfRange(input)
                ? Option<string>.None
                : ConvertSafely(input, mapping);
    }
}