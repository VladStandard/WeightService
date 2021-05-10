using BlazorDownloadFile;
using DeviceControl.Core.DAL.TableModels;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Service
{
    public class FileDownload : IFileDownload
    {
        private readonly IWebHostEnvironment _environment;
        public FileDownload(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task DownloadAsync(IBlazorDownloadFileService blazorDownloadFileService, TemplateResourcesEntity entity)
        {
            if (entity == null || entity.ImageData == null || entity.ImageData.Length == 0)
                return;
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            var chars = Encoding.UTF8.GetChars(entity.ImageData);
            var bytes = entity.CloneBytes(Convert.FromBase64CharArray(chars, 0, chars.Length));
            await blazorDownloadFileService.DownloadFile(entity.Name.Contains('.') ? entity.Name : $"{entity.Name}.{entity.Type}", 
                bytes, "application/octet-stream").ConfigureAwait(false);
        }
    }
}