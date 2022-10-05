using Application.Common.Interfaces.Services;

namespace Infrastructure.Common.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.UtcNow;
}
