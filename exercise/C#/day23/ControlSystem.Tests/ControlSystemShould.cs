using ControlSystem.Core;
using FluentAssertions;
using FluentAssertions.LanguageExt;
using LanguageExt.Common;

namespace ControlSystem.Tests
{
    public class ControlSystemShould : IDisposable
    {
        private readonly StringWriter _output;
        private readonly TextWriter _originalOutput;

        public ControlSystemShould()
        {
            _output = new StringWriter();
            _originalOutput = Console.Out;
            Console.SetOut(_output);
        }

        private void OutputShouldBeEquivalentTo(params string[] expected)
        {
            _output.ToString()
                .Split(Environment.NewLine)
                .Where(x => !string.IsNullOrEmpty(x))
                .Should()
                .BeEquivalentTo(expected);
        }
        
        [Fact]
        public void Start()
        {
            var controlSystem = new Core.System();
            controlSystem.StartSystem();
            controlSystem.Status.Should().Be(SleighEngineStatus.On);
            OutputShouldBeEquivalentTo("Starting the sleigh...", "System ready.");
        }


        [Fact]
        public void Ascend()
        {
            var controlSystem = new Core.System();
            controlSystem.StartSystem();
            controlSystem.Ascend()
                .Should().BeRight();
            controlSystem.Action.Should().Be(SleighAction.Flying);
            OutputShouldBeEquivalentTo("Starting the sleigh...", "System ready.", "Ascending...");
        }

        [Fact]
        public void Descend()
        {
            var controlSystem = new Core.System();
            controlSystem.StartSystem();
            controlSystem.Ascend()
                .Bind(_ => controlSystem.Descend())
                .Should().BeRight();
            
            controlSystem.Action.Should().Be(SleighAction.Hovering);
            OutputShouldBeEquivalentTo("Starting the sleigh...", "System ready.", "Ascending...", "Descending...");
        }
        
        [Fact]
        public void Park()
        {
            var controlSystem = new Core.System();
            controlSystem.StartSystem();
            controlSystem.Invoking(cs => cs.Park()).Should().NotThrow<SleighNotStartedException>();
            controlSystem.Action.Should().Be(SleighAction.Parked);
            OutputShouldBeEquivalentTo("Starting the sleigh...", "System ready.", "Parking...");
        }

        [Fact]
        public void Stop()
        {
            var controlSystem = new Core.System();
            controlSystem.StartSystem();
            controlSystem.StopSystem();
            controlSystem.Status.Should().Be(SleighEngineStatus.Off);
            OutputShouldBeEquivalentTo("Starting the sleigh...", "System ready.", "Stopping the sleigh...", "System shutdown.");
        }


        public void Dispose()
        {
            Console.SetOut(_originalOutput);
            _output.Dispose();
        }

        public class Fail
        {
            [Fact]
            public void ToAscendWhenNotStarted()
            {
                var controlSystem = new Core.System();
                controlSystem.Ascend()
                    .Should().BeLeft()
                    .Which.Should().Match<Error>(err => 
                        err.Message == "The sleigh is not started. Please start the sleigh before any other action...");
            }

            [Fact]
            public void ToDescendWhenNotStarted()
            {
                var controlSystem = new Core.System();
                controlSystem.Descend()
                    .Should().BeLeft()
                    .Which.Should().Match<Error>(err => err.Message == "The sleigh is not started. Please start the sleigh before any other action...");
            }
        }
    }
}