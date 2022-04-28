// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorDownloadFile;
using DataCore.Sql.TableScaleModels;
using DataCore.Utils;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Service
{
    public class FileDownload : IFileDownload
    {
        //private readonly IWebHostEnvironment _environment;
        //public FileDownload(IWebHostEnvironment environment)
        //{
        //    _environment = environment;
        //}

        public async Task DownloadAsync(IBlazorDownloadFileService? blazorDownloadFileService, TemplateResourceEntity? item)
        {
            if (item == null || item.ImageData == null || item.ImageData.Value.Length == 0)
                return;
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            char[] chars = Encoding.UTF8.GetChars(item.ImageData.Value);
            byte[] bytes = DataUtils.ByteClone(Convert.FromBase64CharArray(chars, 0, chars.Length));
            if (blazorDownloadFileService != null)
                await blazorDownloadFileService.DownloadFile(item.Name.Contains('.') ? item.Name : $"{item.Name}.{item.Type}",
                    bytes, "application/octet-stream").ConfigureAwait(false);
        }
    }
}
