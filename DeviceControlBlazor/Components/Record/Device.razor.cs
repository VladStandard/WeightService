// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControlCore.DAL.DataModels;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Threading.Tasks;

namespace DeviceControlBlazor.Components.Record
{
    public partial class Device
    {
        #region Public and private fields and properties

        [Parameter] public DeviceEntity Item { get; set; }
        [Parameter] public EventCallback CallbackActionSaveAsync { get; set; }
        [Parameter] public EventCallback CallbackActionCancelAsync { get; set; }

        private string _state = @"Отключено";

        #endregion

        #region Public and private methods

        private async Task SaveAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            bool result = false;
            // ...
            Dialog.Close(true);
            NotificationMessage message = result
                ? new NotificationMessage
                {
                    Severity = NotificationSeverity.Info,
                    Summary = $"Устройство {Item.Scales.Description}",
                    Detail = "Сохранено успешно",
                    Duration = AppSettings.Delay
                }
                : new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Устройство {Item.Scales.Description}",
                    Detail = "Ошибка сохранения!",
                    Duration = AppSettings.Delay
                };
            Notification.Notify(message);
        }

        private async Task CancelAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Dialog.Close(false);
        }

        private void Change<T>(T value, string name) where T : class
        {
            if (name.Equals("State"))
            {
                _state = Convert.ToBoolean(value) ? @"Работает" : @"Отключено";
            }
            StateHasChanged();
        }

        #endregion
    }
}