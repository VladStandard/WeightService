// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemTemplateResource : BlazorCore.Models.RazorBase
{
    #region Public and private fields, properties, constructor

    private TemplateResourceEntity ItemCast { get => Item == null ? new() : (TemplateResourceEntity)Item; set => Item = value; }
    [Inject] private IFileUpload? FileUpload { get; set; }
    [Inject] private IFileDownload? FileDownload { get; set; }
    [Inject] private IBlazorDownloadFileService? DownloadFileService { get; set; }
    public string FileInfo { get; set; } = string.Empty;
    public double FileProgress { get; set; } = default;
    public string FileComplete { get; set; } = string.Empty;
    private int ProgressValue { get; set; } = default;
    public IFileListEntry? File { get; private set; }

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableScaleEntity(ProjectsEnums.TableScale.TemplatesResources);
        ItemCast = new();
	}

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActions(new()
        {
            () =>
            {
                ItemCast = AppSettings.DataAccess.Crud.GetItemByIdNotNull<TemplateResourceEntity>(IdentityId);
                if (IdentityId != null && TableAction == DbTableAction.New)
                    ItemCast.IdentityId = (long)IdentityId;

                ButtonSettings = new(false, false, false, false, false, true, true);
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
            Duration = BlazorCore.Models.AppSettingsHelper.Delay
        };
        NotificationService.Notify(msg);
    }

    private void OnFileUpload(InputFileChangeEventArgs e)
    {
        foreach (IBrowserFile file in e.GetMultipleFiles(e.FileCount))
        {
            if (FileUpload != null)
                FileUpload.UploadAsync(ItemCast, file.OpenReadStream(10_000_000));
        }
        InvokeAsync(StateHasChanged);
    }

    private void OnFileDownload()
    {
        if (FileDownload != null)
            FileDownload.DownloadAsync(DownloadFileService, ItemCast);

        InvokeAsync(StateHasChanged);
    }

    #endregion
}
