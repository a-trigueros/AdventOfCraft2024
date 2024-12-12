using LanguageExt;

namespace Gifts;

public class Children
{
    private readonly Dictionary<string, Child> _children = new();

    public void Add(Child child) => _children.Add(child.Name, child);

    public Option<Child> GetChild(string name) => 
        _children.TryGetValue(name, out var child) 
            ? child 
            : Option<Child>.None;
}