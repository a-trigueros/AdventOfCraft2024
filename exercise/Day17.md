🌟🌟🌟 Day 17: Parse Elf Ids (EID) 🌟🌟🌟
Teo and the elves are happy about your previous iteration of the EID program.
They are submitting you a new system to implement with new rules.

Writing a system that parses EID

We have already worked on it to design a program validating EIDs using T.D.D.

This time we will use a different approach.

➡️ Fighting Primitive Obsession : limit of the approach
Primitive obsession is a code smell in which primitive data types are used excessively to represent data models.

By excessively using primitive types:
it is really hard to represent business concepts
they can not contain any business logic
this will disperse business logic throughout the system

➡️ Parse Don't Validate
During this new challenge you have to:
Apply Parse Don't Validate principle.
Use Property-Based Testing to drive the implementation.
Use Types to represent EID: Type-Driven Development

Your parsing function should ensure the below property:

for all (eid)
parseEID(eid.toString) == eid


With parse don't validate we want to make it impossible to represent an invalid EID in our system.

Your parser may look like this: String -> Either[Pars🌟🌟🌟 Day 17: Parse Elf Ids (EID) 🌟🌟🌟
Teo and the elves are happy about your previous iteration of the EID program.
They are submitting you a new system to implement with new rules.

Writing a system that parses EID

We have already worked on it to design a program validating EIDs using T.D.D.

This time we will use a different approach.

➡️ Fighting Primitive Obsession : limit of the approach
Primitive obsession is a code smell in which primitive data types are used excessively to represent data models.

By excessively using primitive types:
it is really hard to represent business concepts
they can not contain any business logic
this will disperse business logic throughout the system

➡️ Parse Don't Validate
During this new challenge you have to:
Apply Parse Don't Validate principle.
Use Property-Based Testing to drive the implementation.
Use Types to represent EID: Type-Driven Development

Your parsing function should ensure the below property:

for all (eid)
parseEID(eid.toString) == eid


With parse don't validate we want to make it impossible to represent an invalid EID in our system.

Your parser may look like this: String -> Either[ParsingError, EID] or a similar monad

➡️ EID rules
The rules in the previous iteration still apply.

✅🚀 Challenge: Design a system that parses EID. 🚀✅
Image
You will find details and instructions here: https://github.com/advent-of-craft/2024/blob/main/docs/day17/challenge.mdingError, EID] or a similar monad

➡️ EID rules
The rules in the previous iteration still apply.

✅🚀 Challenge: Design a system that parses EID. 🚀✅
Image
You will find details and instructions here: https://github.com/advent-of-craft/2024/blob/main/docs/day17/challenge.md