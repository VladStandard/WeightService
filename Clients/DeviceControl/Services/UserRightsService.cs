// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Services;

public class UserRightsService : IUserRightsService
{
    private WsSqlAccessRepository AccessRepository { get; } = new();

    public async Task<List<string>> GetUserRightsAsync(string username)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        List<string> rights = new();
        WsSqlAccessModel access =  AccessRepository.GetItemByNameOrCreate(username);
        for (int i = access.Rights; i >= 0; --i)
            rights.Add($"{i}");
        return rights;
    }
}