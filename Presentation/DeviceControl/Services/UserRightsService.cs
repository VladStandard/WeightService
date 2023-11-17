namespace DeviceControl.Services;

public class UserRightsService : IUserRightsService
{
    private SqlAccessRepository AccessRepository { get; } = new();

    public async Task<List<string>> GetUserRightsAsync(string username)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        List<string> rights = new();
        SqlAccessEntity access =  AccessRepository.GetItemByNameOrCreate(username);
        for (int i = access.Rights; i >= 0; --i)
            rights.Add($"{i}");
        return rights;
    }
}
