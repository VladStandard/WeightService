// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Helpers;
using WsStorageCore.TableScaleModels.TemplatesResources;

namespace WsBlazorCore.Services;

public class FileUpload : IFileUpload
{
    private WsSqlAccessManagerHelper AccessManager => WsSqlAccessManagerHelper.Instance;
    //private readonly IWebHostEnvironment _environment;
    private readonly IHostingEnvironment _environment;
    //public FileUpload(IWebHostEnvironment environment)
    public FileUpload(IHostingEnvironment environment)
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

    public async Task UploadAsync(WsSqlTemplateResourceModel? item, Stream stream)
    {
        if (item == null)
            return;

        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        item.Data = new() { Value = DataUtils.GetBytes(stream, true) };
        AccessManager.SqlCoreItem.Update(item);
    }
}