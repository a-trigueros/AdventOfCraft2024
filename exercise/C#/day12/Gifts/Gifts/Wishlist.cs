namespace Gifts;

public class Wishlist
{
    private List<Toy> _toys = [];

    public void SetFavoriteToys(Toy firstChoice, Toy secondChoice, Toy thirdChoice)
    {
        _toys = [firstChoice, secondChoice, thirdChoice];
    }

    public Toy Pick(Behavior behavior) => _toys.Count > 0
        ? behavior switch
        {
            Behavior.VeryNice => _toys[0],
            Behavior.Nice => _toys[1],
            Behavior.Naughty => _toys[^1],
            _ => throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null)
        }
        : throw new InvalidOperationException("No toys in wishlist");
}