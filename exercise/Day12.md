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
First observations:
- There is a hard value of 3 choices for each child
- Collection first class citizen for Toys & Child repository
- Santa initialization can be made outside
- Due to the volume of information, I don't feel a Builder pattern is necessary
- Behavior can be made an enum

1. commit Behavior as an enum 
