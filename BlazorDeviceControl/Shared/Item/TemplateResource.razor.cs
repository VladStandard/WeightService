// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.Models;
using BlazorDeviceControl.Service;
using BlazorDownloadFile;
using BlazorInputFile;
using DataCore.DAL;
using DataCore.DAL.TableModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class TemplateResource
    {
        #region Public and private fields and properties

        public TemplateResourceEntity TemplateResourcesItem { get => (TemplateResourceEntity)IdItem; set => SetItem(value); }
        public List<TypeEntity<string>> ResourceTypes { get; set; }
        [Inject] private IFileUpload FileUpload { get; set; }
        [Inject] private IFileDownload FileDownload { get; set; }
        [Inject] private IBlazorDownloadFileService DownloadFileService { get; set; }
        public string FileInfo { get; set; }
        public double FileProgress { get; set; }
        public string FileComplete { get; set; }
        private int ProgressValue { get; set; }
        public IFileListEntry File { get; private set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        Table = new TableScaleEntity(EnumTableScale.TemplateResources);
                        TemplateResourcesItem = null;
                        ResourceTypes = null;
                        await GuiRefreshWithWaitAsync();

                        TemplateResourcesItem = AppSettings.DataAccess.TemplateResourcesCrud.GetEntity(new FieldListEntity(new Dictionary<string, object>
                            { { EnumField.Id.ToString(), Id } }), null);
                        ResourceTypes = new List<TypeEntity<string>> { new("TTF", "TTF"), new("GRF", "GRF") };
                        await GuiRefreshWithWaitAsync();
                    }),
                }, true);
        }

        private void OnChange(object value, string name)
        {
            switch (name)
            {
                case "ResourceTypes":
                    if (value is string strValue)
                    {
                        TemplateResourcesItem.Type = strValue;
                    }
                    StateHasChanged();
                    break;
            }
        }

        private void OnError(UploadErrorEventArgs args, string name)
        {
            NotificationMessage msg = new()
            {
                Severity = NotificationSeverity.Error,
                Summary = $"Ошибка метода [{name}]!",
                Detail = args.Message,
                Duration = AppSettingsEntity.Delay
            };
            Notification.Notify(msg);
        }

        private async Task OnFileUpload(InputFileChangeEventArgs e)
        {
            foreach (IBrowserFile file in e.GetMultipleFiles(e.FileCount))
            {
                await FileUpload.UploadAsync(AppSettings.DataAccess, TemplateResourcesItem, file.OpenReadStream(10_000_000));
            }
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnFileDownload()
        {
            await FileDownload.DownloadAsync(DownloadFileService, TemplateResourcesItem);

            await InvokeAsync(StateHasChanged);
        }

        #endregion
    }
}
