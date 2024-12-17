namespace EID;

public class ControlKey(int value)
{
    public int Value => value;

    public static ControlKey From(int value) => new(value);
    private bool Equals(ControlKey other) => other.Value == value;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ControlKey)obj);
    }

    public override int GetHashCode()
        => value.GetHashCode();

    public static bool operator ==(ControlKey? left, ControlKey? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ControlKey? left, ControlKey? right)
    {
        return !Equals(left, right);
    }
}