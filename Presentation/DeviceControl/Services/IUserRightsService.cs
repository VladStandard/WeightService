namespace DeviceControl.Services;

public interface IUserRightsService
{
    Task<List<string>> GetUserRightsAsync(string username);
}
