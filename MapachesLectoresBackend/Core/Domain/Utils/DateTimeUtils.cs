namespace MapachesLectoresBackend.Core.Domain.Utils;

public class DateTimeUtils
{
    public static DateTime GetDateTimeUtcWithMiliseconds()
    {
        var currentDateTime = DateTime.UtcNow;
        return new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second, currentDateTime.Millisecond, DateTimeKind.Utc);
    }
}