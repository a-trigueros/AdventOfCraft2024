🌟🌟🌟 Day 6: Review some code. 🌟🌟🌟

Teo and the elves are working on the system to track tasks to prepare Christmas.

They seem to trust you as you did a good job on the previous tasks.

They feel happy with their code but would like to be challenged by an external dev.

So they ask you to review it, it is composed of:
ElfWorkshop
And the related file test

We propose to simulate a code review by adding comments in the code.

Have a good review 🎅

✅🚀 Challenge: Review the code. 🚀✅

💡TEO HINT: View the elves as your colleagues to address the comments.
Image


---

# Thoughts

## Tests

- Duplication: The tests AddTask_Should_Add_Craft_Dollhouse_Task and AddTask_Should_Add_Paint_Bicycle_Task are duplicates of the test AddTask_Should_Add_Task and add no value.
- Extraction: The creation of the workshop (ElfWorkshop) can be set as a private field and be used in all methods to avoid repetition.
- List Verification: Add a verification to ensure there is only one task in the list after adding.
- Primitive Obsession: Use a custom type (ChristmasTask) to manage tasks and check for empty or null tasks at creation.
- Missing test case: Test adding tasks when tasks already exist in the list.
- Deletion test cases: Test deleting tasks when there are no tasks to complete and when multiple tasks exist in the list.
  
## Implementation
- Encapsulation: Do not publicly expose the task list. As it is mutable, it can be changed from the outside of the calsse. Prefer using a private field and expose a read-only version via a method or property.
- PrimitiveObsession: use a type to instantiate the Task and check their Not null or empty invariant (`ChrismasTask` ?).
- Task Completion: Be more explicit about potential task completion failure. Dont return string. Instead use a monadic approach or use a `bool TryCompleteTask(out string task)` method signature to highlight the possibility of failure.
- Processing Order: Clarify if FIFO order is relevant for task processing and if not, provide a parameter to the `CompleteTask` method.
- Happy Path last: Set the happy path at the end of the function and exit early. This allows the most of your code to be more readable since it stays at the first level of indentation. 
- Intent: For readability, you should use the `.Any()` extension method instead of checking the size of the list : the JIT runtime will do its optimizations to ensure it get the best of your code.