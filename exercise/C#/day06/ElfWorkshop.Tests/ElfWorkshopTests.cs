using FluentAssertions;
using Xunit;

namespace ElfWorkshop.Tests
{
    public class ElfWorkshopTests
    {
        [Fact]
        public void AddTask_Should_Add_Task()
        {
            // You can extract the workshop building as a private field.
            var workshop = new ElfWorkshop();
            workshop.AddTask("Build toy train");
            workshop.TaskList.Should().Contain("Build toy train");
            // We may count that there is only one task in the list
        }
        // We may want to test it also when there are already some existing tasks 

        // This test is the same that the former one, it adds no value
        [Fact]
        public void AddTask_Should_Add_Craft_Dollhouse_Task()
        {
            var workshop = new ElfWorkshop();
            workshop.AddTask("Craft dollhouse");
            workshop.TaskList.Should().Contain("Craft dollhouse");
        }

        // This test is the same that the former one, it adds no value
        [Fact]
        public void AddTask_Should_Add_Paint_Bicycle_Task()
        {
            var workshop = new ElfWorkshop();
            workshop.AddTask("Paint bicycle");
            workshop.TaskList.Should().Contain("Paint bicycle");
        }

        // Better ot use a custom type `ChrismasTask` for instance to hold Tasks and check the empty case at creation time. 
        [Fact]
        public void AddTask_Should_Handle_Empty_Tasks_Correctly()
        {
            var workshop = new ElfWorkshop();
            workshop.AddTask("");
            workshop.TaskList.Should().BeEmpty();
        }
        
        // Better ot use a custom type `ChrismasTask` for instance to hold Tasks and check the null case at creation time. 
        [Fact]
        public void AddTask_Should_Handle_Null_Tasks_Correctly()
        {
            var workshop = new ElfWorkshop();
            workshop.AddTask(null);
            workshop.TaskList.Should().BeEmpty();
        }

        [Fact]
        public void CompleteTask_Should_Remove_Task()
        {
            var workshop = new ElfWorkshop();
            workshop.AddTask("Wrap gifts");
            var removedTask = workshop.CompleteTask();
            removedTask.Should().Be("Wrap gifts");
            workshop.TaskList.Should().BeEmpty();
        }
        
        // The case where there are no tasks to complete but the call is performed is missing
        // We may want to test it also when there are many tasks in the list 
    }
}