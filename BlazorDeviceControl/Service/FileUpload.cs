// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorInputFile;
using DataCore.Sql;
using DataCore.Sql.TableScaleModels;
using DataCore.Utils;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Service
{
    public class FileUpload : IFileUpload
    {
        public DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;
        private readonly IWebHostEnvironment _environment;
        public FileUpload(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task UploadAsync(IFileListEntry fileEntry)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            string path = Path.Combine(_environment.ContentRootPath, "Upload", fileEntry.Name);
            MemoryStream ms = new();
            await fileEntry.Data.CopyToAsync(ms);
            await using FileStream file = new(path, FileMode.Create, FileAccess.Write);
            ms.WriteTo(file);
        }

        public async Task UploadAsync(string name, Stream stream)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            string path = Path.Combine(_environment.ContentRootPath, "Upload", name);
            MemoryStream ms = new();
            await stream.CopyToAsync(ms);
            await using FileStream file = new(path, FileMode.Create, FileAccess.Write);
            ms.WriteTo(file);
        }

        public async Task UploadAsync(TemplateResourceEntity? item, Stream stream)
        {
            if (item == null)
                return;

            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            item.ImageData = new() { Value = DataUtils.GetBytes(stream, true) };
            DataAccess.Crud.UpdateEntity(item);
        }
    }
}
