// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading.Tasks;
using BlazorDownloadFile;
using DataCore.DAL.TableModels;

namespace BlazorDeviceControl.Service
{
    public interface IFileDownload
    {
        Task DownloadAsync(IBlazorDownloadFileService blazorDownloadFileService, TemplateResourceEntity entity);
    }
}