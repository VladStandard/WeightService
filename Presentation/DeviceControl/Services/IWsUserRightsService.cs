namespace DeviceControl.Services;

public interface IWsUserRightsService
{
    Task<List<string>> GetUserRightsAsync(string username);
}
