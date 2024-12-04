namespace Routine.Tests.TestDoubles;

public class ScheduleServiceDouble : IScheduleService
{
    private readonly Schedule _todaySchedule = new();
        
    private readonly List<Schedule> _organizeMyDayCallsArguments = new();

    public bool WasTodayScheduleCalled { get; private set; }
    public bool WasContinueCalled { get; private set; }
        
    public bool WasOrganizeMyDayCalledWith(Schedule schedule) => _organizeMyDayCallsArguments.Contains(schedule);
        
    public Schedule TodaySchedule()
    {
        WasTodayScheduleCalled = true;
        return _todaySchedule;
    }

    public void OrganizeMyDay(Schedule schedule) => _organizeMyDayCallsArguments.Add(schedule);

    public void Continue() => WasContinueCalled = true;
}