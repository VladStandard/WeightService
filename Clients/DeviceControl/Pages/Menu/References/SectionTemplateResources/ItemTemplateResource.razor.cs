// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;
using BlazorDownloadFile;
using WsDataCore.Enums;
using WsStorageCore.Enums;
using WsStorageCore.TableScaleModels.TemplatesResources;
using WsStorageCore.TableScaleModels.TemplatesResources;

namespace BlazorDeviceControl.Pages.Menu.References.SectionTemplateResources;

public sealed partial class ItemTemplateResource : RazorComponentItemBase<TemplateResourceModel>
{
    #region Public and private fields, properties, constructor
    
    [Inject] private IFileUpload? FileUpload { get; set; }
    [Inject] private IFileDownload? FileDownload { get; set; }
    [Inject] private IBlazorDownloadFileService? DownloadFileService { get; set; }
    
    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<TemplateResourceModel>(IdentityUid);
                //if (IdentityId is not null && TableAction == DbTableAction.New)
                //    SqlItemCast.IdentityValueId = (long)IdentityId;
            }
        });
    }

    private void OnError(UploadErrorEventArgs args, string name)
    {
        NotificationMessage msg = new()
        {
            Severity = NotificationSeverity.Error,
            Summary = $"{LocaleCore.Strings.MethodError} [{name}]!",
            Detail = args.Message,
            Duration = BlazorAppSettingsHelper.Delay
        };
        NotificationService?.Notify(msg);
    }

    private void OnFileUpload(InputFileChangeEventArgs e)
    {
        foreach (IBrowserFile file in e.GetMultipleFiles(e.FileCount))
        {
            if (FileUpload is not null)
                FileUpload.UploadAsync(SqlItemCast, file.OpenReadStream(10_000_000));
        }
        InvokeAsync(StateHasChanged);
    }

    private void OnFileDownload()
    {
        if (FileDownload is not null)
            FileDownload.DownloadAsync(DownloadFileService, SqlItemCast);

        InvokeAsync(StateHasChanged);
    }

    private bool IsNotBlackType(string type)
    {
        foreach (TemplateResourceBlackType value in Enum.GetValues(typeof(TemplateResourceBlackType)))
        {
            if (type.Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase))
                return false;
        }
        return true;
    }

    #endregion
}