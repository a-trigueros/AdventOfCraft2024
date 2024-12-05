using System.Text.RegularExpressions;

namespace EID;

public static class RegexElfIdValidator
{
    private static readonly Regex ElfIdParserRegex = new (@"(?<Gender>^[123]){1}(?<Year>\d{2})(?<SerialNumber>\d{3})(?<Key>\d{2})");

    public static bool Validate(string? potentialElfId)
    {
        var match = ElfIdParserRegex.Match(potentialElfId ?? string.Empty);
        return match.Success
               && HasCorrectSerialNumber(match.Groups["SerialNumber"].Value)
               && HasCorrectControlNumber($"{match.Groups["Gender"].Value}{match.Groups["Year"].Value}{match.Groups["SerialNumber"].Value}", match.Groups["Key"].Value);
    }

    private static bool HasCorrectSerialNumber(string serialNumber) =>
        int.Parse(serialNumber) > 0;

    private static bool HasCorrectControlNumber(string baseNumberAsString, string keyAsString) =>
        int.TryParse(baseNumberAsString, out var baseNumber)
        && int.TryParse(keyAsString, out var key)
        && 97 - (baseNumber % 97) == key;
}