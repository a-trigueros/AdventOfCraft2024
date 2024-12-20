using LanguageExt;
using LanguageExt.Common;

namespace EID;

public class BirthOrder(int value)
{
    public int Value => value;

    public static Either<Error, BirthOrder> From(int value) =>
        value is > 0 and < 1_000
            ? new BirthOrder(value)
            : Error.New("BirthOrder must be between 1 and 999");
}