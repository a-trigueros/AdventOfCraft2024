using LanguageExt;

namespace Gifts.Tests.Builders;

public class ChildBuilder
{
    private Option<string> _name = Option<string>.None;
    private Option<Behavior> _behavior = Option<Behavior>.None;
    private Option<(Toy firstChoice, Toy secondChoice, Toy thirdChoice)> _choices = Option<(Toy, Toy, Toy)>.None;

    private ChildBuilder()
    {
    }

    private ChildBuilder Do(Action action)
    {
        action();
        return this;
    }

    public static ChildBuilder AChild() => new();

    public ChildBuilder ThatBehaves(Behavior behavior) =>
        Do(() => _behavior = behavior);

    public ChildBuilder VeryNice() =>
        ThatBehaves(Behavior.VeryNice);

    public ChildBuilder Nice() =>
        ThatBehaves(Behavior.Nice);

    public ChildBuilder Naughty() =>
        ThatBehaves(Behavior.Naughty);

    public ChildBuilder Named(string name) =>
        Do(() => _name = name);

    public ChildBuilder WhoWishesFor(Toy firstChoice, Toy secondChoice, Toy thirdChoice) =>
        Do(() => _choices = (firstChoice, secondChoice, thirdChoice));

    public Child Build() =>
        (from name in _name
            from behavior in _behavior
            select new Child(name, behavior)
        ).Do(child =>
        {
            _choices.IfSome(choices =>
                child.SetWishList(choices.firstChoice, choices.secondChoice, choices.thirdChoice));
        })
        .IfNoneUnsafe(() => throw new InvalidOperationException("Name and behavior are required"))!;
}