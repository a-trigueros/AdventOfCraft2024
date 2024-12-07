🌟🌟🌟 Day 7: Can you read and understand the tests well? 🌟🌟🌟
After the great code review from yesterday, Teo ran by you this morning to tell you to focus on a simple task he pulled from the backlog.

He said some elves were complaining because some tests were hard to understand.
But they never had time to work on it since the code is working...

He's asking what you would do with this test code.

How our tests could be more business oriented and more human-readable?

✅🚀 Challenge: Improve the assertions to be more business-oriented. 🚀✅
Image

---

# Thoughts

During this session, we were with Kristian, Marju, Cédric.
Some thoughts emerged from the discussion about bringing clarity.
- Using a Gherkin format
- Using a builder pattern
- Using customized assertions

We started creating the builder pattern to have a 1 instruction `Given`.
This brought a lot of thought about what is a workshop, what it is doing and how is it intended to work.
We concluded it was akin to a factory, creating toys from a blueprint.
So we adjusted the naming in the builder to reflect this.
We achieved business-intent-shouting methods such as 'givenAnEmptyWorkshop' or 'producesToy'.
It brought a lot of clarity to the test.

Making the assertion explicit to check if the toy was produced or not bring a bit of clarity but not as much as the builder in this exercise.

Once both were made, we achieved tests that make a lot more sense from a business view.

While it seemed a good idea to call "empty" a workshop that is not 'yet' setup to produces toys, it seems weird to keep this naming regarding a production line.

We didn't challenge this part since the session was over.

Food for thoughts : 
- we don't have any import about source types in our tests.
- we brought A LOT to each other by sharing our understanding and having different approaches to what to achieve.

