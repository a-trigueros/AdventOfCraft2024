namespace Travel;

public static class SantaTravelCalculator
{
    const string NumberOfReindeersMustBeAtLeast = "Number of reindeers must be at least 1";
    public static int CalculateTotalDistanceRecursively(int numberOfReindeers)
    {
        if (numberOfReindeers < 1) 
            throw new ArgumentException(NumberOfReindeersMustBeAtLeast);
        if (numberOfReindeers == 1) return 1;
        checked
        {
            return 2 * CalculateTotalDistanceRecursively(numberOfReindeers - 1) + 1;
        }
    }

    public static int CalculateTotalDistanceInALoop(int numberOfReindeers)
    {
        if (numberOfReindeers < 1) 
            throw new ArgumentException(NumberOfReindeersMustBeAtLeast);
        int totalDistance = 1;
        for (var i = 2; i <= numberOfReindeers; i++)
        {
            checked
            {
                totalDistance = AddToTotalDistance(totalDistance);
            }
        }

        return totalDistance;
    }

    private static int AddToTotalDistance(int totalDistance)
    {
        return 2 * totalDistance + 1;
    }

    public static int CalculateTotalDistanceUsingLinq(int numberOfReindeers)
    {
        if (numberOfReindeers < 1) 
            throw new ArgumentException(NumberOfReindeersMustBeAtLeast);
        return Enumerable.Range(2, numberOfReindeers - 1)
            .Aggregate(1, (acc, _) => checked(2 * acc + 1));
    }
    public static int CalculateTotalDistanceDirectly(int numberOfReindeers)
    {
        if (numberOfReindeers < 1)
            throw new ArgumentException(NumberOfReindeersMustBeAtLeast);

        return (1 << numberOfReindeers) - 1;
    }
}