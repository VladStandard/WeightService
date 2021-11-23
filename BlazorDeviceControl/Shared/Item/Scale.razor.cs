﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.Models;
using DataCore;
using DataShareCore;
using DataShareCore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using DataProjectsCore;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Scale
    {
        #region Public and private fields and properties

        public ScaleEntity ScaleItem { get => (ScaleEntity)IdItem; set => SetItem(value); }
        public string PluTitle { get; set; }
        public PluEntity PluItem { get; set; }
        public List<PluEntity> PluItems { get; set; } = null;
        public List<PrinterEntity> PrinterItems { get; set; } = null;
        public List<TemplateEntity> TemplatesDefaultItems { get; set; } = null;
        public List<TemplateEntity> TemplatesSeriesItems { get; set; } = null;
        public List<WorkshopEntity> WorkshopItems { get; set; } = null;
        public List<TypeEntity<string>> ComPorts { get; set; }
        public List<HostDirect> HostItems { get; set; } = null;

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.Scales);
                        ScaleItem = null;
                        ComPorts = null;
                        PluItems = null;
                        TemplatesDefaultItems = null;
                        TemplatesSeriesItems = null;
                        WorkshopItems = null;
                        PrinterItems = null;
                        HostItems = null;
                        await GuiRefreshWithWaitAsync();

                        ScaleItem = AppSettings.DataAccess.ScalesCrud.GetEntity(new FieldListEntity(new Dictionary<string, object> {
                            { ShareEnums.DbField.Id.ToString(), Id },
                        }), null);
                        // ComPorts
                        ComPorts = new List<TypeEntity<string>>();
                        for (int i = 1; i < 256; i++)
                        {
                            ComPorts.Add(new TypeEntity<string>($"COM{i}", $"COM{i}"));
                        }
                        // ScaleFactor
                        ScaleItem.ScaleFactor ??= 1000;
                        // PLU.
                        PluTitle = $"{LocalizationData.DeviceControl.SectionPlus}  [{LocalizationCore.Strings.DataLoading}]";
                        PluItems = AppSettings.DataAccess.PlusCrud.GetEntities(new FieldListEntity(new Dictionary<string, object> {
                            { ShareEnums.DbField.Marked.ToString(), false },
                            { "Scale.Id", ScaleItem.Id },
                        }), new FieldOrderEntity(ShareEnums.DbField.Plu, ShareEnums.DbOrderDirection.Asc)).ToList();
                        PluTitle = $"{LocalizationData.DeviceControl.SectionPlus}  [{PluItems.Count} {LocalizationData.DeviceControl.DataRecords}]";
                        // Other.
                        TemplatesDefaultItems = AppSettings.DataAccess.TemplatesCrud.GetEntities(
                            new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Marked.ToString(), false } }),
                            null).ToList();
                        TemplatesSeriesItems = AppSettings.DataAccess.TemplatesCrud.GetEntities(
                            new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Marked.ToString(), false } }),
                            null).ToList();
                        WorkshopItems = AppSettings.DataAccess.WorkshopsCrud.GetEntities(
                            new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Marked.ToString(), false } }),
                            null).ToList();
                        PrinterItems = AppSettings.DataAccess.PrintersCrud.GetEntities(
                            new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Marked.ToString(), false } }),
                            null).ToList();
                        HostItems = AppSettings.DataAccess.HostsCrud.GetFreeHosts(ScaleItem.Host?.Id);
                        await GuiRefreshWithWaitAsync();
                    }),
                }, true);
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
        //            Action(EnumTableScales.Plu, ShareEnums.DbTableAction.Edit, ScaleItem, false, PluItem);
        //            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        NotificationMessage msg = new()
        //        {
        //            Severity = NotificationSeverity.Error,
        //            Summary = $"Ошибка метода [{memberName}]!",
        //            Detail = ex.Message,
        //            Duration = AppSettingsEntity.Delay
        //        };
        //        Notification.Notify(msg);
        //        Console.WriteLine(msg.Detail);
        //        AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
        //    }
        //}

        private void OnChange(object value, string name)
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
                    if (value is int idDefault)
                    {
                        if (idDefault <= 0)
                            ScaleItem.TemplateDefault = null;
                        else
                        {
                            ScaleItem.TemplateDefault = AppSettings.DataAccess.TemplatesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idDefault } }),
                                null);
                        }
                    }
                    break;
                case "TemplatesSeries":
                    if (value is int idSeries)
                    {
                        if (idSeries <= 0)
                            ScaleItem.TemplateSeries = null;
                        else
                        {
                            ScaleItem.TemplateSeries = AppSettings.DataAccess.TemplatesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idSeries } }),
                                null);
                        }
                    }
                    break;
                case "WorkShops":
                    if (value is int idWorkShop)
                    {
                        if (idWorkShop <= 0)
                            ScaleItem.WorkShop = null;
                        else
                        {
                            ScaleItem.WorkShop = AppSettings.DataAccess.WorkshopsCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idWorkShop } }),
                                null);
                        }
                    }
                    break;
                case "Printers":
                    if (value is int idPrinter)
                    {
                        if (idPrinter <= 0)
                            ScaleItem.Printer = null;
                        else
                        {
                            ScaleItem.Printer = AppSettings.DataAccess.PrintersCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idPrinter } }),
                                null);
                        }
                    }
                    break;
                case "Hosts":
                    if (value is int idHost)
                    {
                        if (idHost <= 0)
                            ScaleItem.Host = null;
                        else
                        {
                            ScaleItem.Host = AppSettings.DataAccess.HostsCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idHost } }),
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
