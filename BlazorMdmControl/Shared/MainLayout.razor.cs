//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using System;
//using System.Threading.Tasks;
//using DataCore.Utils;
//using MdmControlBlazor.Utils;
//using Microsoft.AspNetCore.Components.Authorization;
//using Radzen;

//namespace MdmControlBlazor.Shared
//{
//    public partial class MainLayout
//    {
//        #region Public and private fields and properties

//        private string DbServer => JsonAppSettings.Server.Contains(LocalizationStrings.SqlServerRelease, StringComparison.InvariantCultureIgnoreCase)
//                ? "Промышленный сервер" : "Сервер разработки";
//        private bool IsSqlServerRelease => JsonAppSettings.Server.Contains(LocalizationStrings.SqlServerRelease, StringComparison.InvariantCultureIgnoreCase);
//        private string MemoryInfo => BlazorSettings.Memory != null
//            ? $"{LocalizationStrings.MemoryUsed}: {BlazorSettings.Memory.MemorySize.Physical.MegaBytes:N0} MB  |  {StringUtils.FormatCurDtRus(true)}"
//            : $"{LocalizationStrings.MemoryUsed}: - MB";
//        private string AuthMessage => BlazorSettings.Identity != null ? BlazorSettings.Identity.Name : @"User error!";

//        #endregion

//        #region Public and private methods

//        private async Task GuiRefreshAsync()
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//            await InvokeAsync(StateHasChanged).ConfigureAwait(false);
//        }

//        protected override async Task OnInitializedAsync()
//        {
//            await base.OnInitializedAsync().ConfigureAwait(true);

//            try
//            {
//                BlazorSettings.Setup(JsonAppSettings, Notification, Dialog, Navigation, Tooltip, JsRuntime);

//                Task task = new Task(delegate { BlazorSettings.MemoryOpen(GuiRefreshAsync); });
//                task.Start();

//                Task taskAuthentication = new Task(delegate { BlazorSettings.AuthenticationOpen(AuthenticationState, GuiRefreshAsync); });
//                taskAuthentication.Start();
//            }
//            catch (Exception ex)
//            {
//                string msg = ex.Message;
//                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
//                    msg += Environment.NewLine + ex.InnerException.Message;
//                if (!string.IsNullOrEmpty(msg))
//                    Notification.Notify(NotificationSeverity.Error, LocalizationStrings.MainTitle + Environment.NewLine, msg, BlazorSettings.Delay);

//                Console.WriteLine(msg);
//                Console.WriteLine($"MainLayout.razor. {nameof(OnInitializedAsync)}.");
//            }
//            finally
//            {
//                await GuiRefreshAsync().ConfigureAwait(false);
//            }
//        }

//        #endregion
//    }
//}