namespace Gifts;

public class Santa
{
    private readonly Children _children = new ();

    public Toy? ChooseToyForChild(string childName)
    {
        Child? found = _children.GetChild(childName).IfNoneUnsafe((Child?)null);

        if (found == null)
            throw new InvalidOperationException("No such child found");

        if (found.Behavior == Behavior.Naughty)
            return found.Wishlist[^1];

        if (found.Behavior == Behavior.Nice)
            return found.Wishlist[1];

        if (found.Behavior == Behavior.VeryNice)
            return found.Wishlist[0];

        return null;
    }

    public void AddChild(Child child) => _children.Add(child);
}