// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Protocols;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemScale
    {
        #region Public and private fields and properties

        public ScaleEntity ItemCast { get => Item == null ? new() : (ScaleEntity)Item; set => Item = value; }
        public List<PrinterEntity>? PrinterItems { get; set; }
        public List<PrinterEntity>? ShippingPrinterItems { get; set; }
        public List<TemplateEntity>? TemplatesDefaultItems { get; set; }
        public List<TemplateEntity>? TemplatesSeriesItems { get; set; }
        public List<WorkShopEntity>? WorkShopItems { get; set; }
        public List<TypeEntity<string>>? ComPorts { get; set; }
        public List<HostEntity>? HostItems { get; set; }

        #endregion

        #region Constructor and destructor

        public ItemScale() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.Scales);
            ItemCast = new();
            ComPorts = new();
            TemplatesDefaultItems = null;
            TemplatesSeriesItems = null;
            WorkShopItems = null;
            PrinterItems = null;
            ShippingPrinterItems = null;
            HostItems = null;
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
                        new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, IdentityId } }), null);
                    if (IdentityId != null && TableAction == DbTableAction.New)
                        ItemCast.IdentityId = (long)IdentityId;
                    // ComPorts
                    ComPorts = SerialPortsUtils.GetListTypeComPorts(Lang.English);
                    // ScaleFactor
                    ItemCast.ScaleFactor ??= 1000;
                    // Other.
                    TemplatesDefaultItems = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                        new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IsMarked, false } }),
                        new FieldOrderEntity(DbField.Title, DbOrderDirection.Asc))
                        ?.ToList();
                    TemplatesSeriesItems = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                        new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IsMarked, false } }),
                        new FieldOrderEntity(DbField.Title, DbOrderDirection.Asc))
                        ?.ToList();
                    WorkShopItems = AppSettings.DataAccess.Crud.GetEntities<WorkShopEntity>(
                        new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IsMarked, false } }),
                        null)
                        ?.ToList();
                    PrinterItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
                        new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IsMarked, false } }),
                        null)
                        ?.ToList();
                    ShippingPrinterItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
                        new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IsMarked, false } }),
                        null)
                        ?.ToList();
                    HostItems = AppSettings.DataAccess.Crud.GetEntities<HostEntity>(
                        new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IsMarked, false } }),
                        new FieldOrderEntity(DbField.Name, DbOrderDirection.Asc))
                        ?.ToList();
                    ButtonSettings = new(false, false, false, false, false, true, true);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
