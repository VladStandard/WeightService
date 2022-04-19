//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using System;
//using System.Runtime.CompilerServices;
//using System.Threading.Tasks;
//using DataCore;
//using DataCore.DAL.Models;
//using MdmControlBlazor.Utils;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Web;
//using Radzen;

//namespace MdmControlBlazor.Components
//{
//    public partial class EntityPage
//    {
//        #region Public and private fields and properties

//        [Parameter]
//        public ShareEnums.TableDwh Table { get; set; }
//        [Parameter]
//        public BaseEntity Entity { get; set; }
//        [Parameter]
//        public ShareEnums.DbTableAction Action { get; set; }
//        [Parameter]
//        public EventCallback CallbackActionSaveAsync { get; set; }
//        [Parameter]
//        public EventCallback CallbackActionCancelAsync { get; set; }

//        #endregion

//        #region Public and private methods

//        protected override async Task OnInitializedAsync()
//        {
//            await base.OnInitializedAsync().ConfigureAwait(true);
//        }

//        private bool FieldControlDeny(BaseEntity item, string field)
//        {
//            bool result = item != null;
//            if (item is NomenclatureEntity nomenclatureEntity)
//            {
//                if (nomenclatureEntity.EqualsDefault())
//                    result = false;
//            }

//            if (!result)
//            {
//                NotificationMessage msg = new NotificationMessage
//                {
//                    Severity = NotificationSeverity.Warning,
//                    Summary = "Контроль данных",
//                    Detail = $"Необходимо заполнить поле [{field}]!",
//                    Duration = LocalizationStrings.Timeout
//                };
//                Notification.Notify(msg);
//                return false;
//            }
//            return true;
//        }

//        private async Task SaveAsync(MouseEventArgs args, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//            try
//            {
//                BlazorSettings.Setup(JsonAppSettings, Notification, Dialog, Navigation, Tooltip, JsRuntime);
//                switch (Table)
//                {
//                    case ShareEnums.TableDwh.NomenclatureMaster:
//                    case ShareEnums.TableDwh.NomenclatureNonNormalize:
//                        if (Action == ShareEnums.DbTableAction.Add)
//                            BlazorSettings.SqlDataAccess.NomenclatureCrud.SaveEntity((NomenclatureEntity)Entity);
//                        else
//                            BlazorSettings.SqlDataAccess.NomenclatureCrud.UpdateEntity((NomenclatureEntity)Entity);
//                        break;
//                }
//            }
//            catch (Exception ex)
//            {
//                NotificationMessage msg = new NotificationMessage
//                {
//                    Severity = NotificationSeverity.Error,
//                    Summary = $"{LocalizationCore.Strings.Main.MethodError} [{memberName}]!",
//                    Detail = ex.Message,
//                    Duration = LocalizationStrings.Timeout
//                };
//                Notification.Notify(msg);
//                Console.WriteLine(ex.Message);
//                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
//            }
//            finally
//            {
//                Dialog.Close(true);
//            }
//        }

//        private async Task CancelAsync(MouseEventArgs args)
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//            Dialog.Close(false);
//        }

//        #endregion
//    }
//}