global using Xunit;
using FluentAssertions;
using FluentAssertions.LanguageExt;
using LanguageExt.Common;

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
        
        got.Should().BeSuccess()
            .Which.Should().Be(Ball);
    }

    [Fact]
    public void GivenNiceChildWhenDistributingGiftsThenChildReceivesSecondChoice()
    {
        var bobby = new Child("bobby", Behavior.Nice);
        bobby.SetWishList(Playstation, Plush, Ball);
        _santa.AddChild(bobby);
        var got = _santa.ChooseToyForChild("bobby");

        got.Should().BeSuccess()
            .Which.Should().Be(Plush);
    }

    [Fact]
    public void GivenVeryNiceChildWhenDistributingGiftsThenChildReceivesFirstChoice()
    {
        var bobby = new Child("bobby", Behavior.VeryNice);
        bobby.SetWishList(Playstation, Plush, Ball);
        _santa.AddChild(bobby);
        var got = _santa.ChooseToyForChild("bobby");

        got.Should().BeSuccess()
            .Which.Should().Be(Playstation);
    }

    [Fact]
    public void GivenNonExistingChildWhenDistributingGiftsThenExceptionThrown()
    {
        var bobby = new Child("bobby", Behavior.VeryNice);
        bobby.SetWishList(Playstation, Plush, Ball);
        _santa.AddChild(bobby);

        var chooseToyForChild = _santa.ChooseToyForChild("alice");
        chooseToyForChild.Should().BeFail()
            .Which.Should().Match((Error e) => e.Message == "No such child found");
    }
    
    [Theory]
    [InlineData(Behavior.VeryNice)]
    [InlineData(Behavior.Nice)]
    [InlineData(Behavior.Naughty)]
    public void GivenExistingChildWhenNoWishListSetThenExceptionThrown(Behavior behavior)
    {
        var bobby = new Child("bobby", behavior);
        _santa.AddChild(bobby);

        _santa.ChooseToyForChild("bobby")
            .Should().BeFail()
            .Which.Should().Match((Error e) => e.Message == "No toys in wishlist");

    }
}