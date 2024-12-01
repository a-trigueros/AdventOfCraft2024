namespace Communication.Tests.Doubles;

public class DaysUntilChrismasCounter(int days) : ICountDaysUntilChrismas
{
    public int GetDays() => days;
}