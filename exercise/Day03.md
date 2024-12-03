Day 3: Fuzzy or not?
Today is another day on the production of the toys and Santa summoned you earlier to talk to you directly about a
special assignment. 📦

He is still working on the delivery organization and from time to time likes to optimize his older system.

He has written some tests and just heard about a technique called Fuzzing. 💫

He wonders how to apply this principle to the code below...

Let's try to help him. 🎅

Of course any help optimizing the program is welcome as well.

✅🚀 Challenge: Use fuzzing in the tests... 🚀✅

# Goals

- Check we don't accept gifts too heavy
- Set and retrieve random attribute on gift

# Thoughts:

## Regarding the Fuzzy testing AKA Property Based Testing

- Properties allows to check a property and not a value
- The framework require a bit or trial & error before deciding how to use it
  (I struggled a LOT before returning the `Property` type from the test methods).
- The framework exposes two strucutres that seems similar: Gen and Arbitrary.

|                                      | Gen<T> | Arbitrary<T> |
|--------------------------------------|--------|--------------|
| Can be combined using LINQ           | ✅      | ❌            |
| Is used in the `Props.ForAll` method | ❌      | ✅            |

I have chosen to go with Gen as the test class properties and convert them when I need to use them in the
`Props.ForAll` methods.

## Regarding the code cleanup:

- I extracted the weight validation in a separate method
- The `_preparedGifts` collection is not used. I assume it's to highlight a call to a remote service in the context of
  the exercise.
  Otherwise, I would have removed it.

