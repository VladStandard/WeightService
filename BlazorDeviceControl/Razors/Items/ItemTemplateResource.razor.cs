// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemTemplateResource : RazorPageModel
{
    #region Public and private fields, properties, constructor

    private TemplateResourceModel ItemCast { get => Item == null ? new() : (TemplateResourceModel)Item; set => Item = value; }
    [Inject] private IFileUpload? FileUpload { get; set; }
    [Inject] private IFileDownload? FileDownload { get; set; }
    [Inject] private IBlazorDownloadFileService? DownloadFileService { get; set; }
    //public string FileInfo { get; set; } = string.Empty;
    //public double FileProgress { get; set; } = default;
    //public string FileComplete { get; set; } = string.Empty;
    //private int ProgressValue { get; set; } = default;
    //public IFileListEntry? File { get; private set; }

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

		RunActionsInitialized(new()
		{
			() =>
			{
		        Table = new TableScaleModel(ProjectsEnums.TableScale.TemplatesResources);
		        ItemCast = new();
			}
		});
	}

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActionsParametersSet(new()
        {
            () =>
            {
                ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<TemplateResourceModel>(IdentityId);
                //if (IdentityId != null && TableAction == DbTableAction.New)
                //    ItemCast.Identity.Id = (long)IdentityId;

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
            Duration = AppSettingsHelper.Delay
        };
        NotificationService?.Notify(msg);
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
