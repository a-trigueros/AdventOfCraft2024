using LanguageExt;
using LanguageExt.Common;

namespace Gifts;

public class Santa
{
    private readonly Children _children = new ();

    public Fin<Toy> ChooseToyForChild(string childName) =>
        _children.GetChild(childName)
            .ToFin(Error.New("No such child found"))
            .Bind(child => child.Pick());

    public void AddChild(Child child) => _children.Add(child);
}