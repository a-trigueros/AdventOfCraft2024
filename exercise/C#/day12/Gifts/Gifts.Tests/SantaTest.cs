global using Xunit;
using FluentAssertions;

namespace Gifts.Tests;

public class SantaTest
{
    private static readonly Toy Playstation = new("playstation");
    private static readonly Toy Ball = new("ball");
    private static readonly Toy Plush = new("plush");


    private readonly Santa _santa = new();
    
    [Fact]
    public void GivenNaughtyChildWhenDistributingGiftsThenChildReceivesThirdChoice()
    {
        var bobby = new Child("bobby", Behavior.Naughty);
        bobby.SetWishList(Playstation, Plush, Ball);
        _santa.AddChild(bobby);
        var got = _santa.ChooseToyForChild("bobby");

        got.Should().Be(Ball);
    }

    [Fact]
    public void GivenNiceChildWhenDistributingGiftsThenChildReceivesSecondChoice()
    {
        var bobby = new Child("bobby", Behavior.Nice);
        bobby.SetWishList(Playstation, Plush, Ball);
        _santa.AddChild(bobby);
        var got = _santa.ChooseToyForChild("bobby");

        got.Should().Be(Plush);
    }

    [Fact]
    public void GivenVeryNiceChildWhenDistributingGiftsThenChildReceivesFirstChoice()
    {
        var bobby = new Child("bobby", Behavior.VeryNice);
        bobby.SetWishList(Playstation, Plush, Ball);
        _santa.AddChild(bobby);
        var got = _santa.ChooseToyForChild("bobby");

        got.Should().Be(Playstation);
    }

    [Fact]
    public void GivenNonExistingChildWhenDistributingGiftsThenExceptionThrown()
    {
        var bobby = new Child("bobby", Behavior.VeryNice);
        bobby.SetWishList(Playstation, Plush, Ball);
        _santa.AddChild(bobby);

        var chooseToyForChild = () => _santa.ChooseToyForChild("alice");
        chooseToyForChild.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("No such child found");
    }
}