using LanguageExt;

namespace Gifts;

public class Child(string name, Behavior behavior)
{
    public string Name { get; } = name;

    private readonly Wishlist _wishlist = new();

    public void SetWishList(Toy firstChoice, Toy secondChoice, Toy thirdChoice) => 
        _wishlist.SetFavoriteToys(firstChoice, secondChoice, thirdChoice);

    public Fin<Toy> Pick() => 
        _wishlist.Pick(behavior);
}