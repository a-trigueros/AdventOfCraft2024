namespace Gifts;

public class Child
{
    public string Name { get; }
    public Behavior Behavior { get; }
    public Wishlist Wishlist { get; }

    public Child(string name, Behavior behavior)
    {
        Name = name;
        Behavior = behavior;
        Wishlist = new();
    }

    public void SetWishList(Toy firstChoice, Toy secondChoice, Toy thirdChoice)
        => Wishlist.SetFavoriteToys(firstChoice, secondChoice, thirdChoice);

    public Toy Pick() => Wishlist.Pick(Behavior);
}