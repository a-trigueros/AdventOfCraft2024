using FluentAssertions;
using Routine.Tests.TestDoubles;
using Xunit;
using FakeItEasy;

namespace Routine.Tests
{
    public class RoutineTests
    {
        [Fact]
        public void StartRoutine_With_FakeItEasy()
        {
            var dummySchedule = new Schedule();
            
            var scheduleService = A.Fake<IScheduleService>();
            var emailService = A.Fake<IEmailService>();
            var reindeerFeeder = A.Fake<IReindeerFeeder>();

            A.CallTo(() =>scheduleService.TodaySchedule()).Returns(dummySchedule);
            
            var routine = new Routine(emailService, scheduleService, reindeerFeeder);
            routine.Start();
            
            A.CallTo(() => scheduleService.TodaySchedule()).MustHaveHappened();
            A.CallTo(() => scheduleService.OrganizeMyDay(dummySchedule)).MustHaveHappened();
            A.CallTo(() => emailService.ReadNewEmails()).MustHaveHappened();
            A.CallTo(() => reindeerFeeder.FeedReindeers()).MustHaveHappened();
            A.CallTo(() => scheduleService.Continue()).MustHaveHappened();
        }

        [Fact]
        public void StartRoutine_With_Manual_Test_Doubles()
        {
            var scheduleService = new ScheduleServiceDouble();
            var mailService = new MailServiceDouble();
            var reindeerFeeder = new ReindeerFeederDouble();
            
            var routine = new Routine(mailService, scheduleService, reindeerFeeder);
            routine.Start();
            
            scheduleService.WasTodayScheduleCalled.Should().BeTrue();
            scheduleService.WasOrganizeMyDayCalledWith(scheduleService.TodaySchedule()).Should().BeTrue();
            mailService.WasReadNewEmailsCalled.Should().BeTrue();
            reindeerFeeder.WasFeedReindeersCalled.Should().BeTrue();
            scheduleService.WasContinueCalled.Should().BeTrue();
        }
    }
}