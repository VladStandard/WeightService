﻿// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Printer
    {
        #region Public and private fields and properties

        public PrinterEntity PrinterItem { get => (PrinterEntity)IdItem; set => SetItem(value); }
        public List<PrinterTypeEntity> PrinterTypeItems { get; set; } = null;

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        Table = new TableScaleEntity(EnumTableScale.Printers);
                        PrinterItem = null;
                        PrinterTypeItems = null;
                        await GuiRefreshWithWaitAsync();

                        PrinterItem = AppSettings.DataAccess.PrintersCrud.GetEntity(new FieldListEntity(new Dictionary<string, object>
                            { { EnumField.Id.ToString(), Id } }), null);
                        PrinterTypeItems = AppSettings.DataAccess.PrinterTypesCrud.GetEntities(null, null).ToList();
                        await GuiRefreshWithWaitAsync();
                    }),
                }, true);
        }

        #endregion
    }
}
