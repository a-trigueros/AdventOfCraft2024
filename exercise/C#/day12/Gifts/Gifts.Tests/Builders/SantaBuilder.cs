namespace Gifts.Tests.Builders;

public class SantaBuilder
{
    private readonly List<ChildBuilder> _children = new();

    private SantaBuilder()
    {
    }

    public static SantaBuilder Santa() => new();

    private SantaBuilder Do(Action action)
    {
        action();
        return this;
    }

    public SantaBuilder WithChild(Func<ChildBuilder, ChildBuilder> child) =>
        Do(() => _children.Add(child(ChildBuilder.AChild())));

    public Santa Build() =>
        _children.Select(x => x.Build())
            .Aggregate(new Santa(), (santa, child) =>
            {
                santa.AddChild(child);
                return santa;
            });
}