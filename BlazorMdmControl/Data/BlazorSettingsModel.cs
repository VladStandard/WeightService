//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using MdmControlCore;
//using Microsoft.AspNetCore.Components;
//using Radzen;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Security.Principal;
//using System.Threading;
//using System.Threading.Tasks;
//using MdmControlBlazor.Utils;
//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.JSInterop;
//using DataProjectsCore.DAL.TableDwhModels;
//using DataProjectsCore.DAL.Models;
//using DataCore;

//namespace MdmControlBlazor.Data
//{
//    public class BlazorSettingsEntity
//    {
//        #region Public and private fields and properties

//        public NavigationManager Navigation { get; private set; }
//        public NotificationService Notification { get; private set; }
//        public DialogService Dialog { get; private set; }
//        public TooltipService Tooltip { get; private set; }
//        public DataAccessEntity SqlDataAccess { get; set; }
//        public IJSRuntime JsRuntime { get; set; }
//        //public ShareEnums.AccessRights AccessRights { get; set; }
//        public bool ChartSmooth { get; set; }
//        public int Delay { get; } = 5_000;
//        public DataCore.MemoryEntity Memory { get; set; }
//        public delegate Task DelegateGuiRefresh();
//        public int GridPageSize { get; set; } = 20;
//        public AuthenticationState State { get; set; }
//        public IIdentity Identity { get; set; }
//        public bool? UserAccessLevel { get; set; }

//        #endregion

//        #region Public and private fields and properties - Tooltip

//        public void ShowTooltipMasterGet(ElementReference elementReference, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, LocalizationStrings.TableMasterRead, options);
//        public void ShowTooltipMasterClear(ElementReference elementReference, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, LocalizationStrings.TableMasterClear, options);
//        public void ShowTooltipMasterCreate(ElementReference elementReference, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, LocalizationStrings.TableMasterCreate, options);
//        public void ShowTooltipMasterDelete(ElementReference elementReference, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, LocalizationStrings.TableMasterDelete, options);
//        public void ShowTooltipMasterEdit(ElementReference elementReference, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, LocalizationStrings.TableMasterEdit, options);
//        public void ShowTooltipMasterInclude(ElementReference elementReference, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, LocalizationStrings.TableMasterInclude, options);
//        public void ShowTooltipNonNormalizeGet(ElementReference elementReference, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, LocalizationStrings.TableNonNormalizeRead, options);
//        public void ShowTooltipNonNormalizeEdit(ElementReference elementReference, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, LocalizationStrings.TableNonNormalizeEdit, options);
//        public void ShowTooltipNonNormalizeClear(ElementReference elementReference, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, LocalizationStrings.TableNonNormalizeClear, options);
//        public void ShowTooltipNonNormalizeSetRelevanceTrue(ElementReference elementReference, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, LocalizationStrings.TableNonNormalizeSetRelevanceTrue, options);
//        public void ShowTooltipNonNormalizeSetRelevanceFalse(ElementReference elementReference, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, LocalizationStrings.TableNonNormalizeSetRelevanceFalse, options);
//        public void ShowTooltipNonNormalizeSetIsProduct(ElementReference elementReference, bool useIsProductNonNormilize, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, useIsProductNonNormilize 
//                ? LocalizationStrings.TableNonNormalizeSetIsProductFalse
//                : LocalizationStrings.TableNonNormalizeSetIsProductTrue, options);

//        public void ShowTooltipTableRead(ElementReference elementReference, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, LocalizationStrings.TableRead, options);
//        public void ShowTooltipTableEdit(ElementReference elementReference, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, LocalizationStrings.TableEdit, options);
//        public void ShowTooltipTableExclude(ElementReference elementReference, TooltipOptions options = null) =>
//            Tooltip.Open(elementReference, LocalizationStrings.TableMasterExclude, options);

//        #endregion

//        #region Constructor and destructor

//        public BlazorSettingsEntity()
//        {
//            //
//        }

//        public void Setup(JsonAppSettingsEntity dataAccessService, NotificationService notification, DialogService dialog, NavigationManager navigation,
//            TooltipService tooltip, IJSRuntime jsRuntime)
//        {
//            AppSettingsEntity appSettings = new(dataAccessService.Server, dataAccessService.Db, dataAccessService.Trusted, dataAccessService.Username, dataAccessService.Password);
//            SqlDataAccess = new DataAccessEntity(appSettings);
//            Notification = notification;
//            //AccessRights = ShareEnums.AccessRights.Guest;
//            Dialog = dialog;
//            Navigation = navigation;
//            ChartSmooth = false;
//            Tooltip = tooltip;
//            JsRuntime = jsRuntime;
//        }

//        #endregion

//        #region Public and private methods

//        public async Task ActionAsync(ShareEnums.TableDwh table, ShareEnums.DbTableAction tableAction, TableModel item, string page, bool isNewWindow,
//            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//            try
//            {
//                if (table == ShareEnums.TableDwh.Default)
//                    return;
//                if (item == null || item.EqualsDefault())
//                    return;

//                switch (tableAction)
//                {
//                    case ShareEnums.DbTableAction.Add:
//                    case ShareEnums.DbTableAction.Edit:
//                    case ShareEnums.DbTableAction.Copy:
//                        switch (table)
//                        {
//                            case ShareEnums.TableDwh.NomenclatureMaster:
//                            case ShareEnums.TableDwh.NomenclatureNonNormalize:
//                                //if (!isNewWindow)
//                                Navigation.NavigateTo($"{page}/{item.Id}");
//                                //else
//                                //    await JsRuntime?.InvokeAsync<object>("open", $"{page}/{item.Id}", "_blank").ConfigureAwait(false);
//                                break;
//                        }
//                        break;
//                    case ShareEnums.DbTableAction.Delete:
//                        SqlDataAccess.Crud.DeleteEntity(item);
//                        break;
//                    case ShareEnums.DbTableAction.Mark:
//                        SqlDataAccess.Crud.MarkedEntity(item);
//                        break;
//                }
//            }
//            catch (Exception ex)
//            {
//                NotificationMessage msg = new()
//                {
//                    Severity = NotificationSeverity.Error,
//                    Summary = $"{LocaleCore.Strings.MethodError} [{memberName}]!",
//                    Detail = ex.Message,
//                    Duration = LocalizationStrings.Timeout
//                };
//                Notification.Notify(msg);
//                Console.WriteLine(ex.Message);
//                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
//            }
//        }

//        public string ChartDataFormat(object value)
//        {
//            return ((int)value).ToString("####", CultureInfo.InvariantCulture);
//        }

//        public ChartCountEntity[] GetNomenclaturesChartEntities(ShareEnums.DbField field, int count)
//        {
//            ChartCountEntity[] result = Array.Empty<ChartCountEntity>();
//            NomenclatureModel[] entities = SqlDataAccess.NomenclatureCrud.GetEntities(null,
//                new FieldOrderModel(ShareEnums.DbField.CreateDate, ShareEnums.DbOrderDirection.Asc), count);
//            int i = 0;
//            switch (field)
//            {
//                case ShareEnums.DbField.CreateDate:
//                    List<ChartCountEntity> entitiesDateCreated = new();
//                    foreach (NomenclatureModel item in entities)
//                    {
//                        entitiesDateCreated.Add(new ChartCountEntity(item.CreateDate.Date, 1));
//                        i++;
//                    }
//                    IGrouping<DateTime, ChartCountEntity>[] entitiesGroupCreated = entitiesDateCreated.GroupBy(item => item.Date).ToArray();
//                    result = new ChartCountEntity[entitiesGroupCreated.Length];
//                    i = 0;
//                    foreach (IGrouping<DateTime, ChartCountEntity> item in entitiesGroupCreated)
//                    {
//                        result[i] = new ChartCountEntity(item.Key, item.Count());
//                        i++;
//                    }
//                    break;
//            }
//            return result;
//        }

//        public ConfirmOptions GetConfirmOptions()
//        {
//            return new ConfirmOptions
//            {
//                ShowTitle = true,
//                ShowClose = true,
//                OkButtonText = LocalizationStrings.DialogButtonYes,
//                CancelButtonText = LocalizationStrings.DialogButtonCancel,
//                Bottom = null,
//                ChildContent = null,
//                Height = null,
//                Left = null,
//                Style = null,
//                Top = null,
//                Width = null,
//            };
//        }

//        public async Task RunFuncs(string title, string detailSuccess, string detailFail, string detailCancel, List<Func<Task>> actions, CancellationTokenSource cts,
//            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

//            try
//            {
//                if (cts != null && !cts.IsCancellationRequested)
//                {
//                    if (actions != null)
//                    {
//                        foreach (Func<Task> action in actions)
//                        {
//                            await action().ConfigureAwait(false);
//                        }
//                    }
//                    if (!string.IsNullOrEmpty(detailSuccess))
//                        Notification.Notify(NotificationSeverity.Success, title + Environment.NewLine, detailSuccess, Delay);
//                }
//                else
//                {
//                    cts?.Cancel();
//                    if (!string.IsNullOrEmpty(detailCancel))
//                        Notification.Notify(NotificationSeverity.Info, title + Environment.NewLine, detailCancel, Delay);
//                }
//            }
//            catch (Exception ex)
//            {
//                string msg = ex.Message;
//                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
//                    msg += Environment.NewLine + ex.InnerException.Message;
//                if (!string.IsNullOrEmpty(detailFail))
//                {
//                    if (!string.IsNullOrEmpty(msg))
//                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail + Environment.NewLine + msg, Delay);
//                    else
//                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail, Delay);
//                }
//                else
//                {
//                    if (!string.IsNullOrEmpty(msg))
//                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, msg, Delay);
//                }
//                Console.WriteLine(msg);
//                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
//            }
//        }

//        public async Task RunTasks(string title, string detailSuccess, string detailFail, string detailCancel, List<Task> tasks, 
//            DelegateGuiRefresh callRefresh, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

//            try
//            {
//                if (tasks != null)
//                {
//                    foreach (Task task in tasks)
//                    {
//                        if (task != null)
//                        {
//                            task.Start();
//                            task.Wait();
//                        }
//                    }
//                }

//                if (!string.IsNullOrEmpty(detailSuccess))
//                    Notification.Notify(NotificationSeverity.Success, title + Environment.NewLine, detailSuccess, Delay);
//                else
//                {
//                    if (!string.IsNullOrEmpty(detailCancel))
//                        Notification.Notify(NotificationSeverity.Info, title + Environment.NewLine, detailCancel, Delay);
//                }
//            }
//            catch (Exception ex)
//            {
//                string msg = ex.Message;
//                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
//                    msg += Environment.NewLine + ex.InnerException.Message;
//                if (!string.IsNullOrEmpty(detailFail))
//                {
//                    if (!string.IsNullOrEmpty(msg))
//                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail + Environment.NewLine + msg, Delay);
//                    else
//                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail, Delay);
//                }
//                else
//                {
//                    if (!string.IsNullOrEmpty(msg))
//                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, msg, Delay);
//                }
//                Console.WriteLine(msg);
//                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
//            }
//            finally
//            {
//                if (callRefresh != null)
//                    await callRefresh().ConfigureAwait(false);
//            }
//        }

//        public async Task RunFuncsWithQeustion(string title, string detailSuccess, string detailFail, string detailCancel,
//            List<Func<Task>> actions, CancellationTokenSource cts, string questionAdd = "",
//            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//            ConfirmOptions confirmOptions = GetConfirmOptions();

//            try
//            {
//                string question = string.IsNullOrEmpty(questionAdd) ? LocalizationStrings.DialogQuestion : questionAdd;
//                bool? result = Dialog.Confirm(question, title, confirmOptions).Result;
//                if (result == true && !cts.IsCancellationRequested)
//                {
//                    if (actions != null)
//                    {
//                        foreach (Func<Task> action in actions)
//                        {
//                            await action().ConfigureAwait(true);
//                        }
//                    }
//                    if (!string.IsNullOrEmpty(detailSuccess))
//                        Notification.Notify(NotificationSeverity.Success, title + Environment.NewLine, detailSuccess, Delay);
//                }
//                else
//                {
//                    if (!string.IsNullOrEmpty(detailCancel))
//                        Notification.Notify(NotificationSeverity.Info, title + Environment.NewLine, detailCancel, Delay);
//                }
//            }
//            catch (Exception ex)
//            {
//                string msg = ex.Message;
//                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
//                    msg += Environment.NewLine + ex.InnerException.Message;
//                if (!string.IsNullOrEmpty(detailFail))
//                {
//                    if (!string.IsNullOrEmpty(msg))
//                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail + Environment.NewLine + msg, Delay);
//                    else
//                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail, Delay);
//                }
//                else
//                {
//                    if (!string.IsNullOrEmpty(msg))
//                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, msg, Delay);
//                }
//                Console.WriteLine(msg);
//                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
//            }
//        }

//        public async Task RunTasksWithQeustion(string title, string detailSuccess, string detailFail, string detailCancel,
//            List<Task> tasks, DelegateGuiRefresh callRefresh, string questionAdd = "", 
//            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//            ConfirmOptions confirmOptions = GetConfirmOptions();

//            try
//            {
//                string question = string.IsNullOrEmpty(questionAdd) ? LocalizationStrings.DialogQuestion : questionAdd;
//                bool? result = Dialog.Confirm(question, title, confirmOptions).Result;
//                if (result == true)
//                {
//                    if (tasks != null)
//                    {
//                        //Task.WaitAll(tasks.ToArray());
//                        foreach (Task task in tasks)
//                        {
//                            task.Start();
//                            task.Wait();
//                        }
//                    }
//                    if (!string.IsNullOrEmpty(detailSuccess))
//                        Notification.Notify(NotificationSeverity.Success, title + Environment.NewLine, detailSuccess, Delay);
//                }
//                else
//                {
//                    if (!string.IsNullOrEmpty(detailCancel))
//                        Notification.Notify(NotificationSeverity.Info, title + Environment.NewLine, detailCancel, Delay);
//                }
//            }
//            catch (Exception ex)
//            {
//                string msg = ex.Message;
//                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
//                    msg += Environment.NewLine + ex.InnerException.Message;
//                if (!string.IsNullOrEmpty(detailFail))
//                {
//                    if (!string.IsNullOrEmpty(msg))
//                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail + Environment.NewLine + msg, Delay);
//                    else
//                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, detailFail, Delay);
//                }
//                else
//                {
//                    if (!string.IsNullOrEmpty(msg))
//                        Notification.Notify(NotificationSeverity.Error, title + Environment.NewLine, msg, Delay);
//                }

//                Console.WriteLine(msg);
//                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
//            }
//            finally
//            {
//                if (callRefresh != null)
//                    await callRefresh().ConfigureAwait(false);
//            }
//        }

//        #endregion

//        #region Public and private methods - Memory manager

//        public void MemoryOpen(DataCore.MemoryEntity.DelegateGuiRefreshAsync callRefreshAsync)
//        {
//            if (Memory != null)
//                return;
//            //Memory = new DataShareCore.MemoryEntity(1_000, 5_000, Convert.ToUInt64(100 * 1_048_576));
//            Memory = new DataShareCore.MemoryEntity(1_000, 5_000);
//            Memory.Open(callRefreshAsync);
//        }

//        #endregion

//        #region Public and private methods - Authentication

//        public void AuthenticationOpen(AuthenticationStateProvider stateProvider, DataCore.MemoryEntity.DelegateGuiRefresh callRefresh)
//        {
//            if (stateProvider == null)
//                return;
//            Identity = stateProvider.GetAuthenticationStateAsync().Result.User.Identity;
//            SetUserAccessLevel(Identity?.Name);
//        }

//        private void SetUserAccessLevel(string userName,
//            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
//        {
//            UserAccessLevel = null;
//            //object[] objects = DataAccess.GetEntitiesNativeObject(SqlQueries.GetAccessUser(userName), filePath, lineNumber, memberName);
//            //if (objects.Length == 1)
//            //{
//            //    if (objects[0] is object[] { Length: 5 } item)
//            //    {
//            //        if (Guid.TryParse(Convert.ToString(item[0]), out var uid))
//            //        {
//            //            if (item[4] != null)
//            //            {
//            //                UserAccessLevel = Convert.ToBoolean(item[4]);
//            //            }
//            //        }
//            //    }
//            //}
//            UserAccessLevel = true;
//        }

//        #endregion
//    }
//}
