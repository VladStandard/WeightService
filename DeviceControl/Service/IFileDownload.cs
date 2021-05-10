using BlazorDownloadFile;
using DeviceControl.Core.DAL.TableModels;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Service
{
    public interface IFileDownload
    {
        Task DownloadAsync(IBlazorDownloadFileService blazorDownloadFileService, TemplateResourcesEntity entity);
    }
}