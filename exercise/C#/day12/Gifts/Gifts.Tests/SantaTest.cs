global using Xunit;
using FluentAssertions;
using FluentAssertions.LanguageExt;
using LanguageExt.Common;
using static Gifts.Tests.Builders.SantaBuilder;

namespace Gifts.Tests;

public class SantaTest
{
    private const string Bobby = "bobby";
    private static readonly Toy Playstation = new("playstation");
    private static readonly Toy Ball = new("ball");
    private static readonly Toy Plush = new("plush");


    private readonly Santa _santa = new();

    [Fact]
    public void GivenNaughtyChildWhenDistributingGiftsThenChildReceivesThirdChoice() =>
        Santa()
            .WithChild(child => child
                .Named(Bobby)
                .Naughty()
                .WhoWishesFor(Playstation, Plush, Ball)
            ).Build()
            .ChooseToyForChild(Bobby)
            .Should().BeSuccess()
            .Which.Should().Be(Ball);

    [Fact]
    public void GivenNiceChildWhenDistributingGiftsThenChildReceivesSecondChoice() =>
        Santa()
            .WithChild(child => child
                .Named(Bobby)
                .Nice()
                .WhoWishesFor(Playstation, Plush, Ball)
            ).Build()
            .ChooseToyForChild(Bobby)
            .Should().BeSuccess()
            .Which.Should().Be(Plush);

    [Fact]
    public void GivenVeryNiceChildWhenDistributingGiftsThenChildReceivesFirstChoice() =>
        Santa()
            .WithChild(child => child
                .Named(Bobby)
                .VeryNice()
                .WhoWishesFor(Playstation, Plush, Ball)
            ).Build()
            .ChooseToyForChild(Bobby)
            .Should().BeSuccess()
            .Which.Should().Be(Playstation);

    [Fact]
    public void GivenNonExistingChildWhenDistributingGiftsThenExceptionThrown() =>
        Santa()
            .WithChild(child => child
                .Named(Bobby)
                .VeryNice()
                .WhoWishesFor(Playstation, Plush, Ball)
            ).Build()
            .ChooseToyForChild("alice")
            .Should().BeFail()
            .Which.Should().Match((Error e) => e.Message == "No such child found");

    [Theory]
    [InlineData(Behavior.VeryNice)]
    [InlineData(Behavior.Nice)]
    [InlineData(Behavior.Naughty)]
    public void GivenExistingChildWhenNoWishListSetThenExceptionThrown(Behavior behavior) =>
        Santa().WithChild(child => child.Named(Bobby)
                .ThatBehaves(behavior))
            .Build()
            .ChooseToyForChild(Bobby)
            .Should().BeFail()
            .Which.Should().Match((Error e) => e.Message == "No toys in wishlist");
}