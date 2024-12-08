🌟🌟🌟 Day 8: Ask me what to do. 🌟🌟🌟

Santa, Teo and his elves are still preparing toys, they use their own system to manage toys production.

They would like some help to improve the design of their code.

They heard about a concept called "Tell Don't Ask" but have no ideas where to start and how to apply it...

Could you help them?

✅🚀 Challenge: Refactor the code using the Tell Don't Ask principle. 🚀✅

---

# Thoughts

What a day !

I started realizing that the `Toy.State` property shouldn't be exposed and created methods `CanAssignToElf` and `AssignToElf` to handle the state change.
Then when writing tests and being more impregnated with the business, it struck me : these methods didn't describe what **IS** the business but what it **DOES**.
I thought more deeply about it revolved around production again.
I ended with methods such as `CanStartProduction` and `StartProduction`.
When I set the State property to private, I found that I needed a `IsInProduction` method to be able to check from my tests that all was working as expected.

Now that I used and handled the various cases, I realised that the ToyProductionServiceTest class didn't test what it should.
Since now, all my checks were already done on my `Toy` domain object, I thought that these tests should focus on how it behaves with dependencies and, helped with test coverage and [stryker](https://stryker-mutator.io/docs/stryker-net/introduction/), I refactored the tests to do so.

After thinking more about it, I decided that since the `StartProduction` method could fail, I should rename it to `TryStartProduction` and return a boolean to indicate if the production started or not.
And I could use this boolean in the code and in the test, which allowed me to remove the `IsInProduction` method.