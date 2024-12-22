🌟🌟🌟 Day 22: Bulletproof our code with "EID mutations". 🌟🌟🌟
The EID program is ready to be released but it does not mean we can make it even more solid!

We could challenge the API by creating mutants.

We have designed a robust EID parser making impossible to represent an invalid state.

We reached that, by driving our implementation through the below property:

for all (eid)
parseEID(eid.toString) == eid

Mutators...
Now, let's add a new property that demonstrates that an invalid EID can never be parsed:

for all (validEid)
mutate(eid.toString) == error


We propose to generate a valid EID and mutate its string representation to create an invalid one. For that we will create some mutators.

We have already started to work on it in a java sample (you could adapt it to your stack):

// A record that will represent a mutation to make on the EID
private record Mutator(String name, Function1<EID, Gen<String>> func) {
public String mutate(EID eid) {
return func.apply(eid).apply(random);
}
}

// Define mutators here
// This arbitrary will be used to randomly select a mutation to apply to the EID String representation
private static final Arbitrary<Mutator> mutators = Gen.choose((Mutator) null).arbitrary();

@Test
void invalidEIDsCanNotBeParsed() {
Property.def("mutate(eid.toString) == error")
.forAll(validEID, mutators)
.suchThat((eid, mutator) -> EID.parse(mutator.mutate(eid)).isLeft())
.check()
.assertIsSatisfied();
}

Read more about it here.

✅🚀 Challenge the EID implementation by creating some mutants... 🚀✅
Arnaud Bailly - Mutation-Based TDD
Crafting code since 1994