// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleModels.TemplatesResources;

namespace WsBlazorCore.Services;

public interface IFileUpload
{
    Task UploadAsync(IFileListEntry file);
    Task UploadAsync(string name, Stream stream);
    Task UploadAsync(WsSqlTemplateResourceModel? item, Stream stream);
}