🌟🌟🌟 Day 12: Collect or not collect? 🌟🌟🌟
Christmas is getting to a close, and Santa is now distributing gifts to children based on their behavior throughout the year.

For this, he uses criteria as follow:

Children who have been 'very nice' during the year will receive their first choice from their wish list
The ones who have been 'nice' (occasionally naughty) will receive their second choice
The other children who have been 'naughty' will receive their third choice.

@TeoTheElf and the elves have come across an interesting article about object calisthenics.
They wanted you to see if there was something that could be implemented in this system.

✅🚀 Challenge: Simplify the usage of collections. 🚀✅

---

# Thoughts
Observations:
- [X] There is a hard value of 3 choices for each child
- [X] Collection first class citizen for Toys & Child repository
- [X] Santa initialization can be made outside
- [X] Behavior can be made an enum
- [X] Does test builder pattern bring value ?

Commits
1. Behavior as an enum 
2. Share santa initialization
3. Create Children class -> Child repository, use Option monad instead of nullable
4. Wishlist first class collection
5. Monads, Show don't tell, hiding behavior and wishilist in Child
6. Fun : test builder pattern & all expression-body

Question:
Passed a given time, I have issues to grasp if it's clear because I bring value or if its clear because I know the codebase better.
Do you feel the same ?
