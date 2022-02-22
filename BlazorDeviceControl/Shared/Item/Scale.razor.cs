// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Scale
    {
        #region Public and private fields and properties

        public ScaleEntity ScaleItem { get => (ScaleEntity)Item; set => Item = value; }
        public string PluTitle { get; set; }
        public PluEntity PluItem { get; set; }
        public List<PluEntity> PluItems { get; set; } = null;
        public List<PrinterEntity> PrinterItems { get; set; } = null;
        public List<TemplateEntity> TemplatesDefaultItems { get; set; } = null;
        public List<TemplateEntity> TemplatesSeriesItems { get; set; } = null;
        public List<WorkshopEntity> WorkshopItems { get; set; } = null;
        public List<TypeEntity<string>> ComPorts { get; set; }
        public List<HostEntity> HostItems { get; set; } = null;
        public virtual string PageHost => $"{@LocalizationData.DeviceControl.UriRouteItem.Host}/{ScaleItem.Host.Id}";
        public virtual string PagePrinter => $"{@LocalizationData.DeviceControl.UriRouteItem.Printer}/{ScaleItem.Printer.Id}";
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    lock (_locker)
                    {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.Scales);
                        //ScaleItem = null;
                        //ComPorts = null;
                        //PluItems = null;
                        //TemplatesDefaultItems = null;
                        //TemplatesSeriesItems = null;
                        //WorkshopItems = null;
                        //PrinterItems = null;
                        //HostItems = null;
                        ScaleItem = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(new FieldListEntity(new Dictionary<string, object> {
                        { ShareEnums.DbField.Id.ToString(), Id } }), null);
                        if (Id != null && TableAction == ShareEnums.DbTableAction.New)
                            ScaleItem.Id = (long)Id;
                        // ComPorts
                        ComPorts = new List<TypeEntity<string>>();
                        for (int i = 1; i < 256; i++)
                        {
                            ComPorts.Add(new TypeEntity<string>($"COM{i}", $"COM{i}"));
                        }
                        // ScaleFactor
                        ScaleItem.ScaleFactor ??= 1000;
                        // PLU.
                        PluTitle = $"{LocalizationData.DeviceControl.SectionPlus}  [{LocalizationCore.Strings.Main.DataLoading}]";
                        PluItems = AppSettings.DataAccess.Crud.GetEntities<PluEntity>(new FieldListEntity(new Dictionary<string, object> {
                        { ShareEnums.DbField.Marked.ToString(), false },
                        { "Scale.Id", ScaleItem.Id },
                    }), new FieldOrderEntity(ShareEnums.DbField.Plu, ShareEnums.DbOrderDirection.Asc)).ToList();
                        PluTitle = $"{LocalizationData.DeviceControl.SectionPlus}  [{PluItems.Count} {LocalizationData.DeviceControl.DataRecords}]";
                        // Other.
                        TemplatesDefaultItems = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Marked.ToString(), false } }),
                            null).ToList();
                        TemplatesSeriesItems = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Marked.ToString(), false } }),
                            null).ToList();
                        WorkshopItems = AppSettings.DataAccess.Crud.GetEntities<WorkshopEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Marked.ToString(), false } }),
                            null).ToList();
                        PrinterItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
                            new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Marked.ToString(), false } }),
                            null).ToList();
                        HostItems = AppSettings.DataAccess.Crud.GetEntities<HostEntity>(new FieldListEntity(new Dictionary<string, object> {
                            { ShareEnums.DbField.Marked.ToString(), false },
                        }), new FieldOrderEntity(ShareEnums.DbField.Name, ShareEnums.DbOrderDirection.Asc)).ToList();
                        ButtonSettings = new ButtonSettingsEntity(false, false, false, false, false, true, true);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        //private async Task RowDoubleClickAsync(BaseIdEntity entity,
        //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        //{
        //    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        //    try
        //    {
        //        if (entity is PluEntity pluEntity)
        //        {
        //            PluItem = pluEntity;
        //            //await EntityActions.ActionEditAsync(ShareEnums.TableDwh.Plu, PluItem, ScaleItem).ConfigureAwait(true);
        //            Action(EnumTableScales.Plu, DbTableAction.Edit, ScaleItem, false, PluItem);
        //            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        NotificationMessage msg = new()
        //        {
        //            Severity = NotificationSeverity.Error,
        //            Summary = $"{LocalizationCore.Strings.Main.MethodError} [{memberName}]!",
        //            Detail = ex.Message,
        //            Duration = AppSettingsEntity.Delay
        //        };
        //        Notification.Notify(msg);
        //        Console.WriteLine(msg.Detail);
        //        AppSettings.DataAccess.Crud.LogExceptionToSql(ex, filePath, lineNumber, memberName);
        //    }
        //}

        private void OnChange(object value, string name,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                lock (_locker)
                {
                    switch (name)
                    {
                        case "DeviceComPort":
                            if (value is string strValue)
                            {
                                ScaleItem.DeviceComPort = strValue;
                            }
                            break;
                        case "TemplatesDefault":
                            if (value is long idDefault)
                            {
                                if (idDefault <= 0)
                                    ScaleItem.TemplateDefault = null;
                                else
                                {
                                    ScaleItem.TemplateDefault = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
                                        new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idDefault } }),
                                        null);
                                }
                            }
                            break;
                        case "TemplatesSeries":
                            if (value is long idSeries)
                            {
                                if (idSeries <= 0)
                                    ScaleItem.TemplateSeries = null;
                                else
                                {
                                    ScaleItem.TemplateSeries = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
                                        new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idSeries } }),
                                        null);
                                }
                            }
                            break;
                        case "WorkShops":
                            if (value is long idWorkShop)
                            {
                                if (idWorkShop <= 0)
                                    ScaleItem.WorkShop = null;
                                else
                                {
                                    ScaleItem.WorkShop = AppSettings.DataAccess.Crud.GetEntity<WorkshopEntity>(
                                        new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idWorkShop } }),
                                        null);
                                }
                            }
                            break;
                        case "Printers":
                            if (value is long idPrinter)
                            {
                                if (idPrinter <= 0)
                                    ScaleItem.Printer = null;
                                else
                                {
                                    ScaleItem.Printer = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(
                                        new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idPrinter } }),
                                        null);
                                }
                            }
                            break;
                        case "Hosts":
                            if (value is long idHost)
                            {
                                if (idHost <= 0)
                                    ScaleItem.Host = null;
                                else
                                {
                                    ScaleItem.Host = AppSettings.DataAccess.Crud.GetEntity<HostEntity>(
                                        new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idHost } }),
                                        null);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"{LocalizationCore.Strings.Main.MethodError} [{nameof(OnChange)}]!",
                    Detail = ex.Message,
                    Duration = AppSettingsHelper.Delay
                };
                NotificationService.Notify(msg);
                AppSettings.DataAccess.Crud.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
            finally
            {
                StateHasChanged();
            }
        }

        #endregion
    }
}
