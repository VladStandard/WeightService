// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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