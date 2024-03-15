namespace DeviceControl2.Source.Shared.Services;

public class StartupService
{
    private readonly DateTime _startTime = DateTime.Now;
    public TimeSpan TimeOnline => DateTime.Now.Subtract(_startTime);
}