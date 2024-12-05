namespace EID;

public class ElfIdValidator
{
    const int ElfIdLength = 8;
    static char[] _validSexes = new[] { '1', '2', '3' };
    
    public static bool Validate(string? potentialElfId)
    { 
        return IsNotNull(potentialElfId)
            && HasCorrectLength(potentialElfId)
            && HasCorrectSex(potentialElfId)
            && HasCorrectBirthdate(potentialElfId)
            && HasCorrectSerialNumber(potentialElfId)
            && HasCorrectControlNumber(potentialElfId);
    }

    private static bool HasCorrectControlNumber(string? potentialElfId)
    {
        var key = Int32.Parse(potentialElfId[0..6]) % 97;
        return int.TryParse(potentialElfId[6..8], out var controlNumber)
            && 97 - controlNumber == key;
    }

    private static bool HasCorrectSerialNumber(string? potentialElfId)
    {
        return int.TryParse(potentialElfId[3..6], out var value)
                     && value >= 1;
    }

    private static bool HasCorrectBirthdate(string? potentialElfId)
    {
        return char.IsDigit(potentialElfId[1])
               && char.IsDigit(potentialElfId[2]);
    }

    private static bool HasCorrectSex(string? potentialElfId)
    {
        return _validSexes.Contains(potentialElfId[0]);
    }

    private static bool HasCorrectLength(string? potentialElfId)
    {
        return potentialElfId.Length == ElfIdLength;
    }

    private static bool IsNotNull(string? potentialElfId)
    {
        return potentialElfId != null;
    }
}