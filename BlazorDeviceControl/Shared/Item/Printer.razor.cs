// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
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

        [Parameter] public int Id { get; set; }
        public ZebraPrinterEntity PrinterItem { get; set; }
        public List<ZebraPrinterTypeEntity> PrinterTypeItems { get; set; } = null;

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(SetParametersAsync)}", "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        Item = null;
                        PrinterItem = AppSettings.DataAccess.ZebraPrinterCrud.GetEntity(new FieldListEntity(new Dictionary<string, object>
                            { { EnumField.Id.ToString(), Id } }), null);
                        PrinterTypeItems = AppSettings.DataAccess.ZebraPrinterTypeCrud.GetEntities(null, null).ToList();
                        await GuiRefreshAsync(false).ConfigureAwait(false);
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
                            PrinterItem.PrinterType = AppSettings.DataAccess.ZebraPrinterTypeCrud.GetEntity(
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