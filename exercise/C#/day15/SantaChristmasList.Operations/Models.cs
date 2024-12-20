using LanguageExt;

namespace SantaChristmasList.Operations;

public class SleighReport : Dictionary<Child, Either<string, string>>;
public record Gift(string Name);
public record ManufacturedGift(string BarCode);
public record Child(string Name);
