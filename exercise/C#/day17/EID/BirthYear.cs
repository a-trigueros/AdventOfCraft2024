using LanguageExt;
using LanguageExt.Common;

namespace EID;

public class BirthYear(int value)
{
    public int Value => value;

    public static Either<Error, BirthYear> From(int value) =>
        value is > 0 and < 100
            ? new BirthYear(value)
            : Error.New("BirthOrder must be between 1 and 999");
}