﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Shared.Section
{
    public partial class PrinterTypes
    {
        #region Public and private fields and properties

        private List<PrinterTypeEntity> Items { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        Table = new TableScaleEntity(EnumTableScale.PrinterTypes);
                        IdItem = null;
                        Items = null;
                        ItemsCount = 0;
                        await GuiRefreshWithWaitAsync();

                        Items = AppSettings.DataAccess.PrinterTypesCrud.GetEntities(null, null)
                            .OrderBy(x => x.Name).ToList();
                        ItemsCount = Items.Count;
                        await GuiRefreshWithWaitAsync();
                    }),
            }, true);
        }

        #endregion
    }
}
