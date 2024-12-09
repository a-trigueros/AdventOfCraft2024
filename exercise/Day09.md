🌟🌟🌟 Day 9: Improve tests readability. 🌟🌟🌟

Even though it's Chritmas town, some tasks are not fun...

Santa needs to take some time to decide which children will receive their gifts based on several criteria.
He has prepared a program to help him takes his decisions.

He wants to guarantee the quality of his service and covered his code with automated tests.

Unfortunately those tests can be hard to read for certain elves...

how could we help him makes those tests more readable?

✅🚀 Challenge: Improve the tests. 🚀✅

💡TEO HINT: we can improve the construction of the input.

---

# Thoughts

The more I use it, the more the builder pattern is familiar.
I try to use it in a way that builds a fluent sentence.
Also, for the elves to be able to pinpoint what's done and how are made decisions, I choose to only use the variables used in the code : the behaviour and if the gift request was feasible.