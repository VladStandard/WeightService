// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL.DataModels;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Devices
    {
        #region Public and private fields and properties

        private void ShowTooltipGetData(ElementReference elementReference, TooltipOptions options = null) => Tooltip.Open(elementReference, "Прочитать данные", options);
        public List<DeviceEntity> Entities { get; set; }
        public DeviceEntity Entity { get; set; }

        #endregion

        #region Public and private methods

        public override async Task GetDataAsync()
        {
            await GetDataAsync(new Task(delegate
            {
                Entities = AppSettings.DataAccess.DeviceCrud.GetEntities(null, null).ToList();
            }));
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            await GetDataAsync().ConfigureAwait(true);
        }

        private async Task RowSelectAsync(DeviceEntity entity)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Entity = entity;
        }

        private async Task RowDoubleClickAsync(DeviceEntity entity)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Entity = entity;

            //await Dialog.OpenAsync<DevicePage>($"Весовой пост: {entity.Scales.Description}",
            //    new Dictionary<string, object>()
            //    {
            //        { "Device", entity },
            //    },
            //    new DialogOptions() { Width = "1024px", Height = "768px" }).ConfigureAwait(false);
        }

        #endregion

        #region Public and private methods - Действия с таблицей устройств

        private async Task ActionAddAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            NotificationMessage message = new()
            {
                Severity = NotificationSeverity.Info,
                Summary = "Действие",
                Detail = "Добавить" + Environment.NewLine + "В разработке",
                Duration = AppSettings.Delay
            };
            Notification.Notify(message);
        }

        private async Task ActionCopyAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            NotificationMessage message = new()
            {
                Severity = NotificationSeverity.Info,
                Summary = "Действие",
                Detail = "Скопировать" + Environment.NewLine + "В разработке",
                Duration = AppSettings.Delay
            };
            Notification.Notify(message);
        }

        private async Task ActionDeleteAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            NotificationMessage message = new()
            {
                Severity = NotificationSeverity.Info,
                Summary = "Действие",
                Detail = "Удалить" + Environment.NewLine + "В разработке",
                Duration = AppSettings.Delay
            };
            Notification.Notify(message);
        }

        #endregion
    }
}