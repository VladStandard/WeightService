// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.TemplatesResources;

namespace WsBlazorCore.Services;

public class FileDownload : IFileDownload
{
    //private readonly IWebHostEnvironment _environment;
    //public FileDownload(IWebHostEnvironment environment)
    //{
    //    _environment = environment;
    //}

    public async Task DownloadAsync(IBlazorDownloadFileService? blazorDownloadFileService, WsSqlTemplateResourceModel? item)
    {
        if (item == null || item.DataValue.Length == 0)
            return;
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        char[] chars = Encoding.UTF8.GetChars(item.DataValue);
        byte[] bytes = DataUtils.ByteClone(Convert.FromBase64CharArray(chars, 0, chars.Length));
        if (blazorDownloadFileService != null)
            await blazorDownloadFileService.DownloadFile(item.Name.Contains('.') ? item.Name : $"{item.Name}.{item.Type}",
                bytes, "application/octet-stream").ConfigureAwait(false);
    }
}