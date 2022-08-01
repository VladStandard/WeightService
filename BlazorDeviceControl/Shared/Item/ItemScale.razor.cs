// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Protocols;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item;

/// <summary>
/// Scale item page.
/// </summary>
public partial class ItemScale
{
    #region Public and private fields and properties

    private ScaleEntity ItemCast { get => Item == null ? new() : (ScaleEntity)Item; set => Item = value; }
    private List<PrinterEntity> PrinterItems { get; set; }
    private List<ProductionFacilityEntity> ProductionFacilityItems { get; set; }
    private List<PrinterEntity> ShippingPrinterItems { get; set; }
    private List<TemplateEntity> TemplatesDefaultItems { get; set; }
    private List<TemplateEntity> TemplatesSeriesItems { get; set; }
    private List<WorkShopEntity> WorkShopItems { get; set; }
    private List<TypeEntity<string>> ComPorts { get; set; }
    private List<HostEntity> HostItems { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ItemScale()
    {
        PrinterItems = new();
        ComPorts = new();
        HostItems = new();
        ProductionFacilityItems = new();
        ShippingPrinterItems = new();
        WorkShopItems = new();
        TemplatesDefaultItems = new();
        TemplatesSeriesItems = new();
    }

    #endregion

    #region Public and private methods

    private void Default()
    {
        IsLoaded = false;
        Table = new TableScaleEntity(ProjectsEnums.TableScale.Scales);
        ItemCast = new();
        ComPorts = new();
        ProductionFacilityItems = new();
        TemplatesDefaultItems = new();
        TemplatesSeriesItems = new();
        WorkShopItems = new();
        PrinterItems = new();
        ShippingPrinterItems = new();
        HostItems = new();
        ButtonSettings = new();
    }

    private static List<string> GetListComPorts()
    {
        List<string> result = new();
        for (int i = 1; i < 256; i++)
        {
            result.Add($"COM{i}");
        }
        return result;
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters).ConfigureAwait(true);
        RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(SetParametersAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            new Task(async () =>
            {
                Default();
                await GuiRefreshWithWaitAsync();

                ItemCast = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(
                    new FieldListEntity(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
                if (IdentityId != null && TableAction == DbTableAction.New)
                    ItemCast.IdentityId = (long)IdentityId;
                if (ItemCast.Host == null)
                    ItemCast.Host = new(0) { Name = LocaleCore.Table.FieldNull };
                if (ItemCast.PrinterMain == null)
                    ItemCast.PrinterMain = new(0) { Name = LocaleCore.Table.FieldNull };
                if (ItemCast.PrinterShipping == null)
                    ItemCast.PrinterShipping = new(0) { Name = LocaleCore.Table.FieldNull };
                if (ItemCast.TemplateDefault == null)
                    ItemCast.TemplateDefault = new(0) { Title = LocaleCore.Table.FieldNull };
                if (ItemCast.TemplateSeries == null)
                    ItemCast.TemplateSeries = new(0) { Title = LocaleCore.Table.FieldNull };
                if (ItemCast.WorkShop == null)
                    ItemCast.WorkShop = new(0) { Name = LocaleCore.Table.FieldNull };

                // ComPorts
                ComPorts = SerialPortsUtils.GetListTypeComPorts(Lang.English);
                // ScaleFactor
                ItemCast.ScaleFactor ??= 1000;
                // HostItems.
                List<HostEntity>? hostItems = AppSettings.DataAccess.Crud.GetEntities<HostEntity>(
                    new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                    new(DbField.Name, DbOrderDirection.Asc))
                    ?.ToList();
                if (hostItems is { } hostItems2)
                {
                    HostItems.Add(new(0) { Name = LocaleCore.Table.FieldNull });
                    HostItems.AddRange(hostItems2);
                }
                // PrinterItems.
                List<PrinterEntity>? printerItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
                    new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                    null)
                    ?.ToList();
                if (printerItems is IEnumerable<PrinterEntity> printerItems2)
                {
                    PrinterItems.Add(new(0) { Name = LocaleCore.Table.FieldNull });
                    PrinterItems.AddRange(printerItems2);
                }
                // ShippingPrinterItems.
                List<PrinterEntity>? shippingPrinterItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
                    new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                    null)
                    ?.ToList();
                if (shippingPrinterItems is { } shippingPrinterItems2)
                {
                    ShippingPrinterItems.Add(new(0) { Name = LocaleCore.Table.FieldNull });
                    ShippingPrinterItems.AddRange(shippingPrinterItems2);
                }
                // TemplatesDefaultItems.
                List<TemplateEntity>? templatesDefaultItems = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                    new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                    new(DbField.Title, DbOrderDirection.Asc))
                    ?.ToList();
                if (templatesDefaultItems is { } templatesDefaultItems2)
                {
                    TemplatesDefaultItems.Add(new(0) { Title = LocaleCore.Table.FieldNull });
                    TemplatesDefaultItems.AddRange(templatesDefaultItems2);
                }
                // TemplatesSeriesItems.
                List<TemplateEntity>? templatesSeriesItems = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                    new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                    new(DbField.Title, DbOrderDirection.Asc))
                    ?.ToList();
                if (templatesSeriesItems is { } templatesSeriesItems2)
                {
                    TemplatesSeriesItems.Add(new(0) { Title = LocaleCore.Table.FieldNull });
                    TemplatesSeriesItems.AddRange(templatesSeriesItems2);
                }
                // ProductionFacilityItems.
                List<ProductionFacilityEntity>? productionFacilities =
                    AppSettings.DataAccess.Crud.GetEntities<ProductionFacilityEntity>(
                        new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                        null)
                        ?.Where(x => x.IdentityId > 0).ToList();
                if (productionFacilities is { } productionFacilities2)
                {
                    ProductionFacilityItems.Add(new(0) { Name = LocaleCore.Table.FieldNull });
                    ProductionFacilityItems.AddRange(productionFacilities2);
                }
                // WorkShopItems.
                List<WorkShopEntity>? workShopItems = AppSettings.DataAccess.Crud.GetEntities<WorkShopEntity>(
                    new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                    null)
                    ?.ToList();
                if (workShopItems is { } workShopItems2)
                {
                    WorkShopItems.Add(new(0) { Name = LocaleCore.Table.FieldNull });
                    WorkShopItems.AddRange(workShopItems2);
                }

                ButtonSettings = new(false, false, false, false, false, true, true);
                IsLoaded = true;
                await GuiRefreshWithWaitAsync();
            }), true);
    }

    #endregion
}
