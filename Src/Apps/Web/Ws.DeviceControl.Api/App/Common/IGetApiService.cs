namespace Ws.DeviceControl.Api.App.Common;

public interface IGetApiService<T>
{
    public Task<T> GetByIdAsync(Guid id);
    public Task<List<T>> GetAllAsync();
}