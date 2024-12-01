namespace Communication.Tests.Builders;

public class ReinderBuilder
{
    public static ReinderBuilder AReinder() => new();

    private string? _reinderName;
    private string? _locationName;
    private int? _distanceInDays;

    public ReinderBuilder WithName(string name)
    {
        _reinderName = name;
        return this;
    }

    public ReinderBuilder WithLocation(string name, int distanceInDays)
    {
        _locationName = name;
        _distanceInDays = distanceInDays;
        return this;
    }

    public Reinder Build()
    {
        if (string.IsNullOrEmpty(_reinderName))
        {
            throw new ArgumentException("Reinder name is required.");
        }

        if (string.IsNullOrEmpty(_locationName))
        {
            throw new ArgumentException("Location name is required.");
        }
        if(_distanceInDays is null)
        {
            throw new ArgumentException("Distance in days is required.");
        }
        return new Reinder(new ReinderName(_reinderName), new Location(_locationName, _distanceInDays.Value));
    }
}