// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorInputFile;
using DeviceControl.Core.DAL;
using DeviceControl.Core.DAL.TableModels;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _environment;
        public FileUpload(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task UploadAsync(IFileListEntry fileEntry)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            var path = Path.Combine(_environment.ContentRootPath, "Upload", fileEntry.Name);
            var ms = new MemoryStream();
            await fileEntry.Data.CopyToAsync(ms);
            await using var file = new FileStream(path, FileMode.Create, FileAccess.Write);
            ms.WriteTo(file);
        }

        public async Task UploadAsync(string name, Stream stream)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            var path = Path.Combine(_environment.ContentRootPath, "Upload", name);
            var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            await using var file = new FileStream(path, FileMode.Create, FileAccess.Write);
            ms.WriteTo(file);
        }

        public async Task UploadAsync(DataAccessEntity dataAccess, TemplateResourcesEntity entity, Stream stream)
        {
            if (dataAccess == null || entity == null)
                return;

            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            entity.ImageData = await entity.GetBytes(stream, true);
            dataAccess.TemplateResourcesCrud.UpdateEntity(entity);
        }
    }
}