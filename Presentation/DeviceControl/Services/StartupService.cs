namespace DeviceControl.Services;

public class StartupService
{
    private readonly DateTime _startTime;
    
    public TimeSpan TimeOnline => DateTime.Now.Subtract(_startTime);


    public StartupService()
    {
        _startTime = DateTime.Now;
    }
}