🌟🌟🌟 Day 4: A routine with Fakes and Mocks. 🌟🌟🌟
Santa has created a program representing his daily routine.

He would like to add tests on it but does not know how to test it. He has heard a lot of stuff regarding simulating dependencies.

Here are his findings...

About Test Doubles:
- `Test Double`: replace a component on which the Subject Under Test (SUT) depends with a "test-specific equivalent".
- `Dummy`: we pass an object that has no implementation as an argument of a method called on the SUT.
- `Spy`: double to capture the indirect output calls made to another component by the system under test (SUT) for later verification by the test.
- `Fake`: replace a component that the SUT depends on with a much lighter-weight implementation.
- `Stub`: replace a real object with a test-specific object that feeds the desired indirect inputs into the system under test.
- `Mock`: replace an object the system under test (SUT) depends on with a test-specific object that verifies it is being used correctly by the SUT.

He understood that using `Test Doubles` will make his tests ensure those characteristics:
- `Isolation`: allow us to break the original dependencies by isolating the unit / System Under Test (SUT) from its collaborators.
- `Repeatability`: ensure repeatability of our tests as well by replacing external dependencies: call to an API, non deterministic data, ...
- `Fast`: keep our tests Fast.

Please write the Unit Tests for this Routine program (1 with a library and another 1 with your own fakes) to demonstrate what are Test Doubles to Santa.

✅🚀 Challenge: Demonstrate the test doubles by comparing it to using a mock library. 🚀✅


# Thoughts

I still find the Fake / Stub / Mock concepts a bit blurry.
I assume Fake means you inherit an object and override some part of it.
Stub is a manual implementation.
And mock depend on a third-party library.
I feel i'm wrong, feel free to correct me.

In the context of using (mostly) a Spy, I felt:

## Using a manual double 

More overhead, but more control.
More overhead to : 
- create the fake object
- check what is called and how
But an arrange / act / assert is more concise.

## Using FakeItEasy library
More "arrange" work (I guess it's all the way more on a double that is not mostly a spy)
More confidence to use it and check various calls.