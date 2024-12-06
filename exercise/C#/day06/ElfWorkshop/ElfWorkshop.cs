namespace ElfWorkshop;



// Is it a workshop or a tasklist ? 
// If it's a workshop, set the task related behavior of AddTask and CompleteTask methods in a WorkshopTask class and consume it from the workshop
// Should I assume there are no concurrency scenarios here?
public class ElfWorkshop
{
    // Don't expose publicly the task list, since it's mutable, another actor could change it.
    // Use the list internally, as a private field and expose it's readonly version through a method or a property.
    public List<string> TaskList { get; } = [];
    
    public void AddTask(string task)
    {
        // Use a container to store information and handle these verifications at instantiation and fail if it's not respected.
        // How about 'ChrismasTask' ?
        if (task != null && task != "")
        {
            TaskList.Add(task);
        }
    }

    // Since it can fail, you should be more explicit about it
    // Use bool TryCompleteTask(out string task) or a Monadic approach instead.
    // Is it relevant do use a FIFO approach here? Can't the task be processed in any order?

    public string CompleteTask()
    {
        // Try to be more explicit here (if CanRemoveTask) this way, the call will still be valid if the task is specified
        // Fail fast if there are no tasks, try to have the happy path at the end of the function
        // Prefer ".Any()" for readability purposes except if you have millisecond order constraint regarding performances 
        if (TaskList.Any())
        {
            var task = TaskList[0];
            TaskList.RemoveAt(0);
            return task;
        }

        return null;
    }
}