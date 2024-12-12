# Day 11: Secure the program.

Santa and his elves are preparing the Christmas Delivery.
Last year, they had some problems to categorize the packages and some mistakes have been made.

This year they would like to be sure that they can count on the program they use to:
Split gift preparation
Categorize the gifts
Ensure toy balance

They have added a lot of Unit Tests to do so.

Help them to ensure that they won't have any problems with that program 🎅🧝.

✅🚀 Challenge: Ensure the program is 100% safe. 🚀✅

---

# Thoughts
[Readability] -> EnsureToyBalance -> Mix failure and success test
[Tooling] -> EnsureToyBalance -> Tool have issues reporting the right coverage whenever we use continuation and lambda expressions. It seems it's not the case when using the CLI.

Mutations helped me to pinpoint the first cast where the assertions where just not present. I failed to notice it in the warning messages.
I wondered of the usage of PBT to spread more the various cases, but I realized then that it may not be "this" useful, the important aspect being to test the threshold thoroughly.
