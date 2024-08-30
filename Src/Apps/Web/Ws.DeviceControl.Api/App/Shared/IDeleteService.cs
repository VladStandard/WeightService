namespace Ws.DeviceControl.Api.App.Shared;

public interface IDeleteService<in T>
{
    Task DeleteAsync(T id);
}