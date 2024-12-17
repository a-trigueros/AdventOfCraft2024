namespace EID;

public class BirthYear(int value)
{
    public int Value => value;

    public static BirthYear From(int value) => new(value);
}