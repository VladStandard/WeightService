// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.DAL.TableModels;
using BlazorCore.Utils;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Printer
    {
        #region Public and private fields and properties

        private ZebraPrinterEntity PrinterItem => IdItem is ZebraPrinterEntity idItem ? idItem : null;
        public List<ZebraPrinterTypeEntity> PrinterTypeItems { get; set; } = null;

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(SetParametersAsync)}", "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        IdItem = null;
                        PrinterTypeItems = null;
                        await GuiRefreshWithWaitAsync();

                        IdItem = AppSettings.DataAccess.PrintersCrud.GetEntity(new FieldListEntity(new Dictionary<string, object>
                            { { EnumField.Id.ToString(), Id } }), null);
                        PrinterTypeItems = AppSettings.DataAccess.PrinterTypesCrud.GetEntities(null, null).ToList();
                        await GuiRefreshWithWaitAsync();
                    }),
                }, true);
        }

        private void OnChange(object value, string name)
        {
            switch (name)
            {
                case "ZebraPrinterTypeItems":
                    if (value is int id)
                    {
                        if (id <= 0)
                            PrinterItem.PrinterType = null;
                        else
                        {
                            PrinterItem.PrinterType = AppSettings.DataAccess.PrinterTypesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), id } }),
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
