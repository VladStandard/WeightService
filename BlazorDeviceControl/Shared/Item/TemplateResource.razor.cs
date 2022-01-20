// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorDeviceControl.Service;
using BlazorDownloadFile;
using BlazorInputFile;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using System.Collections.Generic;
using DataProjectsCore;
using System.Threading.Tasks;
using BlazorCore.Models;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class TemplateResource
    {
        #region Public and private fields and properties

        public TemplateResourceEntity TemplateResourcesItem { get => (TemplateResourceEntity)Item; set => Item = value; }
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
                new Task(async() => {
                    lock (Locker)
                    {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.TemplateResources);
                        TemplateResourcesItem = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(new FieldListEntity(new Dictionary<string, object>
                            { { ShareEnums.DbField.Id.ToString(), Id } }), null);
                        ResourceTypes = new List<TypeEntity<string>> { new("TTF", "TTF"), new("GRF", "GRF") };
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
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
                Duration = AppSettingsHelper.Delay
            };
            NotificationService.Notify(msg);
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
