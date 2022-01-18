// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataProjectsCore;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.DAL.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Workshop
    {
        #region Public and private fields and properties

        public WorkshopEntity WorkshopItem { get => (WorkshopEntity)Item; set => Item = value; }
        public List<ProductionFacilityEntity> ProductionFacilityEntities { get; set; } = null;

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    Table = new TableScaleEntity(ProjectsEnums.TableScale.Printers);
                    WorkshopItem = null;
                    ProductionFacilityEntities = null;
                    await GuiRefreshWithWaitAsync();

                    WorkshopItem = AppSettings.DataAccess.WorkshopsCrud.GetEntity<WorkshopEntity>(new FieldListEntity(new Dictionary<string, object>
                        { { ShareEnums.DbField.Id.ToString(), Id } }), null);
                    ProductionFacilityEntities = AppSettings.DataAccess.Crud.GetEntities<ProductionFacilityEntity>(null, null).ToList();
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        private async Task RowSelectAsync(BaseEntity item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                //
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = AppSettingsHelper.Delay
                };
                Notification.Notify(msg);
                Console.WriteLine(msg.Detail);
                AppSettings.DataAccess.Crud.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        private async Task RowDoubleClickAsync(BaseEntity item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                //
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = AppSettingsHelper.Delay
                };
                Notification.Notify(msg);
                Console.WriteLine(msg.Detail);
                AppSettings.DataAccess.Crud.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        private void OnChange(object value, string name)
        {
            switch (name)
            {
                case "ProductionFacilities":
                    if (value is int id)
                    {
                        if (id <= 0)
                            WorkshopItem.ProductionFacility = null;
                        else
                        {
                            WorkshopItem.ProductionFacility = AppSettings.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(
                                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), id } }),
                            null);
                        }
                    }
                    break;
            }
            StateHasChanged();
        }

        #endregion
    }
}
