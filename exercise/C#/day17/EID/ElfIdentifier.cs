using LanguageExt;
using LanguageExt.Common;
using LanguageExt.Parsec;
using static LanguageExt.Parsec.Prim;
using static LanguageExt.Parsec.Char;
using static LanguageExt.Prelude;


namespace EID;

public class ElfIdentifier
{
    
    private readonly ControlKey _controlKey;

    private ElfIdentifier(Sex sex, BirthYear birthYear, BirthOrder birthOrder)
    {
        var numbers = int.Parse($"{(int)sex}{birthYear.Value:00}{birthOrder.Value:000}");
        var controlKeyValue = 97 - (numbers % 97);
        _controlKey = ControlKey.From(controlKeyValue);
    }
    
    static readonly char[] ValidSex = ['1', '2', '3'];

    private static readonly Parser<Sex> SexParser =
        from digit in satisfy(c => ValidSex.Contains(c))
        let value = int.Parse($"{digit}")
        select (Sex)value;

    private static readonly Parser<int> BirthYearParser =
        from years in manyn(digit, 2).label("BirthYear must be 2 digits long")
        select int.Parse(new string(years.ToArray()));

    private static readonly Parser<int> BirthOrderParser =
        from birthOrder in manyn(digit, 3).label("BirthOrder must be 3 digits long")
        select int.Parse(new string(birthOrder.ToArray()));

    private static readonly Parser<int> ControlKeyParser =
        from controlKey in manyn(digit, 2)
        select int.Parse(new string(controlKey.ToArray()));

    private static readonly Parser<(Sex sex, int birthYear, int birthOrder, int controlKey)> ElfIdentifierParser =
        from sex in SexParser.label("Sex must be either 1 for Sloubi, 2 for Gagna  or 3 for Catact")
        from birthYear in BirthYearParser
        from birthOrder in BirthOrderParser
        from controlKey in ControlKeyParser
        select (sex, birthYear, birthOrder, controlKey);

    public static Either<Error, ElfIdentifier> Parse(string elfIdentifierCandidate) =>
        from checkNumber in ValidateLength(elfIdentifierCandidate)
        from tuple in ElfIdentifierParser.Parse(elfIdentifierCandidate).ToEither(Error.New)
        from BirthOrder in BirthOrder.From(tuple.birthOrder)
        let birthYear = BirthYear.From(tuple.birthYear)
        let controlKey = ControlKey.From(tuple.controlKey)
        let potentialIdentifier = new ElfIdentifier(tuple.sex, birthYear, BirthOrder)
        from checkControlKey in ValidateControlKey(potentialIdentifier, controlKey)
        select potentialIdentifier;

    private static Either<Error, Unit> ValidateControlKey(ElfIdentifier potentialIdentifier, ControlKey controlKey) =>
        potentialIdentifier._controlKey == controlKey
            ? unit
            : Error.New("ControlKey is invalid");

    private static Either<Error, Unit> ValidateLength(string elfIdentifierCandidate) =>
        elfIdentifierCandidate.Length == 8
            ? unit
            : Error.New("ElfIdentifier must be 8 characters long");
}