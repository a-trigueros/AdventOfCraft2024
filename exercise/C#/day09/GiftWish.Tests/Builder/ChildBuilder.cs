using Ardalis.GuardClauses;

namespace GiftWish.Tests.Builder;

public class ChildBuilder
{
    private const string FirstName = "Alice";
    private const string LastName = "Joie";
    private const int Age = 7;
    private const string GiftName = "Suprise Gift";
    private const Priority GiftPriority = Priority.NiceToHave;
    
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

        return new Child(FirstName, LastName, Age, _behavior.Value, new GiftRequest(GiftName, _isGiftRequestFeasible.Value, GiftPriority));
    }

}