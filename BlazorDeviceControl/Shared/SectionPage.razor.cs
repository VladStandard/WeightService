// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorShareCore.Models;
using DataShareCore;
using DataShareCore.DAL.Models;
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

            //if (parameters.TryGetValue("Table", out ProjectsEnums.TableScale table)) { Table = table; }

            await GetDataAsync(new Task(delegate
            {
                Items = null;

                // ChartDataCreated.
                ContragentsChartCreated = GetContragentsChartEntities(ShareEnums.DbField.CreateDate);
                // ChartDataModified.
                ContragentsChartModified = GetContragentsChartEntities(ShareEnums.DbField.ModifiedDate);

                // ChartDataCreated.
                NomenclaturesChartCreated = GetNomenclaturesChartEntities(ShareEnums.DbField.CreateDate);
                // ChartDataModified.
                NomenclaturesChartModified = GetNomenclaturesChartEntities(ShareEnums.DbField.ModifiedDate);
            }), false).ConfigureAwait(false);
        }

        #endregion
    }
}
