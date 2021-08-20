// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared
{
    public partial class SectionPage
    {
        #region Public and private fields and properties

        ChartCountEntity[] ContragentsChartCreated { get; set; }
        ChartCountEntity[] ContragentsChartModified { get; set; }
        ChartCountEntity[] NomenclaturesChartCreated { get; set; }
        ChartCountEntity[] NomenclaturesChartModified { get; set; }
        [Parameter] public BaseIdEntity[] Items { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            //if (parameters.TryGetValue("Table", out EnumTableScale table)) { Table = table; }

            await GetDataAsync(new Task(delegate
            {
                Items = null;

                // ChartDataCreated.
                ContragentsChartCreated = GetContragentsChartEntities(EnumField.CreateDate);
                // ChartDataModified.
                ContragentsChartModified = GetContragentsChartEntities(EnumField.ModifiedDate);

                // ChartDataCreated.
                NomenclaturesChartCreated = GetNomenclaturesChartEntities(EnumField.CreateDate);
                // ChartDataModified.
                NomenclaturesChartModified = GetNomenclaturesChartEntities(EnumField.ModifiedDate);
            }), false).ConfigureAwait(false);
        }

        #endregion
    }
}
