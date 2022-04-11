// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using DataCore.Utils;
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

        public ScaleEntity? ItemCast { get => Item == null ? null : (ScaleEntity)Item; set => Item = value; }
        public List<PrinterEntity>? PrinterItems { get; set; }
        public List<PrinterEntity>? ShippingPrinterItems { get; set; }
        public List<TemplateEntity>? TemplatesDefaultItems { get; set; }
        public List<TemplateEntity>? TemplatesSeriesItems { get; set; }
        public List<WorkShopEntity>? WorkshopItems { get; set; }
        public List<TypeEntity<string>>? ComPorts { get; set; }
        public List<HostEntity>? HostItems { get; set; }

        #endregion

        #region Constructor and destructor

        public ItemScale() : base()
        {
            //Default();
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                Table = new TableScaleEntity(ProjectsEnums.TableScale.Scales);
                ItemCast = null;
                ComPorts = null;
                TemplatesDefaultItems = null;
                TemplatesSeriesItems = null;
                WorkshopItems = null;
                PrinterItems = null;
                ShippingPrinterItems = null;
                HostItems = null;
                ButtonSettings = new();
                IsBusy = false;
            }
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    if (!IsBusy)
                    {
                        IsBusy = true;
                        ItemCast = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.IdentityId.ToString(), IdentityId } }), null);
                        if (IdentityId != null && TableAction == DbTableAction.New)
                            ItemCast.IdentityId = (long)IdentityId;
                        // ComPorts
                        ComPorts = PortUtils.GetComList();
                        // ScaleFactor
                        ItemCast.ScaleFactor ??= 1000;
                        // Other.
                        TemplatesDefaultItems = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.IsMarked.ToString(), false } }),
                            new FieldOrderEntity(DbField.Title, DbOrderDirection.Asc))
                            ?.ToList();
                        TemplatesSeriesItems = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.IsMarked.ToString(), false } }),
                            new FieldOrderEntity(DbField.Title, DbOrderDirection.Asc))
                            ?.ToList();
                        WorkshopItems = AppSettings.DataAccess.Crud.GetEntities<WorkShopEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.IsMarked.ToString(), false } }),
                            null)
                            ?.ToList();
                        PrinterItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.IsMarked.ToString(), false } }),
                            null)
                            ?.ToList();
                        ShippingPrinterItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.IsMarked.ToString(), false } }),
                            null)
                            ?.ToList();
                        HostItems = AppSettings.DataAccess.Crud.GetEntities<HostEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.IsMarked.ToString(), false } }),
                            new FieldOrderEntity(DbField.Name, DbOrderDirection.Asc))
                            ?.ToList();
                        ButtonSettings = new(false, false, false, false, false, true, true);
                        IsBusy = false;
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
