// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using BlazorDeviceControl.Service;
using BlazorDownloadFile;
using BlazorInputFile;
using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemTemplateResource
    {
		#region Public and private fields, properties, constructor

		private TemplateResourceEntity ItemCast { get => Item == null ? new() : (TemplateResourceEntity)Item; set => Item = value; }
		private List<TypeEntity<string>>? ResourceTypes { get; set; }
        [Inject] private IFileUpload? FileUpload { get; set; }
        [Inject] private IFileDownload? FileDownload { get; set; }
        [Inject] private IBlazorDownloadFileService? DownloadFileService { get; set; }
        public string FileInfo { get; set; } = string.Empty;
        public double FileProgress { get; set; } = default;
        public string FileComplete { get; set; } = string.Empty;
        private int ProgressValue { get; set; } = default;
        public IFileListEntry? File { get; private set; }

        #endregion

        #region Constructor and destructor

        public ItemTemplateResource()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.TemplatesResources);
            ResourceTypes = new List<TypeEntity<string>> { new("TTF", "TTF"), new("GRF", "GRF") };
            ItemCast = new();
            ButtonSettings = new();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(SetParametersAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    ItemCast = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(
                        new(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
                    if (IdentityId != null && TableAction == DbTableAction.New)
                        ItemCast.IdentityId = (long)IdentityId;
                    ButtonSettings = new(false, false, false, false, false, true, true);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
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

        private async Task OnFileUpload(InputFileChangeEventArgs e)
        {
            foreach (IBrowserFile file in e.GetMultipleFiles(e.FileCount))
            {
                if (FileUpload != null)
                    await FileUpload.UploadAsync(ItemCast, file.OpenReadStream(10_000_000));
            }
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnFileDownload()
        {
            if (FileDownload != null)
                await FileDownload.DownloadAsync(DownloadFileService, ItemCast);

            await InvokeAsync(StateHasChanged);
        }

        #endregion
    }
}
