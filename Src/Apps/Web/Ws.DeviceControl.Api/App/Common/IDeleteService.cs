namespace Ws.DeviceControl.Api.App.Common;

public interface IDeleteService<in T>
{
    Task DeleteAsync(T id);
}