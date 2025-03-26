namespace Project1.Shared.Services;

public partial class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset GetCurrentDateTime()
    {
        return DateTimeOffset.UtcNow;
    }
}
