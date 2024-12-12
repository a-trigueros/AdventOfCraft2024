namespace Gifts;

public class Santa
{
    private readonly Children _children = new ();

    public Toy? ChooseToyForChild(string childName)
    {
        Child? found = _children.GetChild(childName).IfNoneUnsafe((Child?)null);

        if (found == null)
            throw new InvalidOperationException("No such child found");

        return found.Pick();
    }

    public void AddChild(Child child) => _children.Add(child);
}