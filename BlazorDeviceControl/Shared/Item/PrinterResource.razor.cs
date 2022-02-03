// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class PrinterResource
    {
        #region Public and private fields and properties

        public PrinterResourceEntity PrinterResourceItem { get => (PrinterResourceEntity)Item; set => Item = value; }
        public List<PrinterEntity> PrinterItems { get; set; } = null;
        public List<TemplateResourceEntity> ResourceItems { get; set; } = null;

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    lock (Locker)
                    {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.Printers);
                        PrinterResourceItem = AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>(new FieldListEntity(new Dictionary<string, object>
                            { { ShareEnums.DbField.Id.ToString(), Id } }), null);
                        if (Id != null && TableAction == ShareEnums.DbTableAction.New)
                            PrinterResourceItem.Id = (int)Id;
                        PrinterItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(null, null).ToList();
                        ResourceItems = AppSettings.DataAccess.Crud.GetEntities<TemplateResourceEntity>(null, null).ToList();
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        private void OnChange(object value, string name)
        {
            switch (name)
            {
                case nameof(PrinterEntity):
                    if (value is int idZebraPrinter)
                    {
                        if (idZebraPrinter <= 0)
                            PrinterResourceItem.Printer = null;
                        else
                        {
                            PrinterResourceItem.Printer = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(
                                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idZebraPrinter } }),
                            null);
                        }
                    }
                    break;
                case nameof(TemplateResourceEntity):
                    if (value is int idTemplateResource)
                    {
                        if (idTemplateResource <= 0)
                            PrinterResourceItem.Printer = null;
                        else
                        {
                            PrinterResourceItem.Resource = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(
                                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idTemplateResource } }),
                            null);
                            if (string.IsNullOrEmpty(PrinterResourceItem.Description))
                            {
                                PrinterResourceItem.Description = PrinterResourceItem.Resource.Name;
                            }
                        }
                    }
                    break;
            }
            StateHasChanged();
        }

        #endregion
    }
}
