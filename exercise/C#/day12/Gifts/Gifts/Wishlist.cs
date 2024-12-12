using LanguageExt;
using LanguageExt.Common;

namespace Gifts;

public class Wishlist
{
    private List<Toy> _toys = [];

    public void SetFavoriteToys(Toy firstChoice, Toy secondChoice, Toy thirdChoice)
    {
        _toys = [firstChoice, secondChoice, thirdChoice];
    }

    public Fin<Toy> Pick(Behavior behavior) =>
        IsEmpty()
            ? Error.New("No toys in wishlist")
            : PickToy(behavior);

    private Toy PickToy(Behavior behavior)
    {
        return behavior switch
        {
            Behavior.VeryNice => _toys[0],
            Behavior.Nice => _toys[1],
            Behavior.Naughty => _toys[^1],
            _ => throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null)
        };
    }

    private bool IsEmpty()
    {
        return _toys.Count == 0;
    }
}