🌟🌟🌟 Day 13 - Refactor your code with the Mikado method. 🌟🌟🌟
This exercise is brought to you by Damien Menanteau and Mirna Mahfoud.

Santa is shopping for gifts and Christmas items during that time. Not everything is made in the factory.

He loads them into a shopping sleigh and hands them to a Christmas Elf, who processes the purchases using special holiday offers like Three for One, Five for a Set Price and Ten Percent Off.

After checking out, the Elf provides Santa with a receipt detailing the total cost, the price and quantity of each item, along with their discounts.

The Christmas Elf is also considering adding a new discount option: Two for One.

Teo explains to you that the relevant code is in the ShoppingSleigh class and even for the elves, this code is a mess! 💥
He thought about it with the team and suggested an approach:

Your objective is to deploy a generic method to compute the X for Y discount offers, covering the existing Three for two discount, as well as the new Two for one discount.

Your starting point is santamarket.model.ShoppingSleigh.handleOffX for Yers() method.

Start with the below Mikado graph and complete it:
[ ] 👍Deploy a generic method to compute the X for Y discount offers, covering Three for two and Two for one offers
[ ] 👍Prepare the code for an easy addition of the X for Y discount type family
[ ] ...
[ ] ...
[ ] 👍Implement the Two for one discount computation
[ ] ...
[ ] 👍Refactor the existing code to use the X for Y discount computation method with the Three for two discount
[ ] Parking-Lot (any change with no direct impact on the main goal)
[ ] ...

# Resources
- https://github.com/advent-of-craft/2024/blob/main/docs/day13/challenge.md
- https://understandlegacycode.com/blog/key-points-of-working-effectively-with-legacy-code/#use-scratch-refactoring-to-get-familiar-with-the-code
---

# Thoughts 💭

## Observations
`Catalog` store product
`Elf` depends on `Catalog` and applies `Discounts` setup on the elf itself.

Handling of offers through method `thesleigh.HandleOffers(receipt, _offers, catalog);` seems not implemented in the right place.
On first thoughts, it should belong in the `Elf` class.

## Plan
[ ] 👍Deploy a generic method to compute the X for Y discount offers, covering Three for two and Two for one offers
    [ ] 👍Prepare the code for an easy addition of the X for Y discount type family
        [ ] 👍 UseStrategy pattern (IApplyDiscount - bool CanApplyDiscount(Offer, Quantity) - Discount ApplyDiscount(Offer, Quantity)
        [X] 👍 Create and use new class to handle the ThreeForTwo discount
            [X] 👍 Refactor the code to use the discount  
        [ ] 👍 Create and use new class to handle the TenPercentDiscount discount
            [ ] 👍 Refactor the code to use the discount  
        [ ] 👍 Create and use new class to handle the TwoForAmount discount
            [ ] 👍 Refactor the code to use the discount  
        [ ] 👍 Create and use new class to handle the FiveForAmount discount
            [ ] 👍 Refactor the code to use the discount             
                [ ] 👍 move the code that says if the discount should be applied in the discount class
                    [ ] 👍 first class citizen for dicount collection to retrieve the discount to apply

[ ] ...
[X] 👍 Discount selection needs
    [X] 👍 product
    [X] 👍 offers (exists + Type)
    [X] 👍 quantity

[X] 👍 Applying discount needs
    [X] 👍 product <- Catalog / Offers (Elf) / Shopping Sleight 
    [X] 👍 offers (Type) / Elf
    [X] 👍 quantity <- Shopping Sleight
    [X] 👍 Unit price <- Catalog


[ ] 👍 Strategy needs product / Offers / quantity / catalog

[ ] 👍Refactor the existing code to use the X for Y discount computation method with the Three for two discount
[ ] Parking-Lot (any change with no direct impact on the main goal)
[ ] ...
