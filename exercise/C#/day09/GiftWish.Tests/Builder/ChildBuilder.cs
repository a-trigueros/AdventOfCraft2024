using Ardalis.GuardClauses;
using Bogus;

namespace GiftWish.Tests.Builder;

public class ChildBuilder
{
    private const string FirstName = "Alice";
    private const string LastName = "Joie";
    private const int Age = 7;
    private const string GiftName = "Suprise Gift";
    private const Priority GiftPriority = Priority.NiceToHave;
    private Faker _faker = new();
    private Behavior? _behavior;
    private bool? _isGiftRequestFeasible;
    
    private  ChildBuilder()
    {
        
    }

    public static ChildBuilder AChild() => new();

    public ChildBuilder WhoIsNice()
    {
        _behavior = Behavior.Nice;
        return this;
    }

    public ChildBuilder WhoIsNaughty()
    {
        _behavior = Behavior.Naughty;
        return this;
    }
    
    public ChildBuilder AndWhishForAFeasibleGift()
    {
        _isGiftRequestFeasible = true;
        return this;
    }
    
    public ChildBuilder AndWishForAnInfeasibleGift()
    {
        _isGiftRequestFeasible = false;
        return this;
    }

    public Child Build()
    {
        Guard.Against.Null(_behavior, nameof(_behavior));
        Guard.Against.Null(_isGiftRequestFeasible, nameof(_isGiftRequestFeasible));

        var giftRequest = new GiftRequest(_faker.Commerce.Product(), _isGiftRequestFeasible.Value, _faker.Random.ArrayElement([Priority.Dream, Priority.NiceToHave]));
        return new Child(_faker.Name.FirstName(), _faker.Name.LastName(), _faker.Random.Number(3, 12), _behavior.Value, giftRequest);
    }

}