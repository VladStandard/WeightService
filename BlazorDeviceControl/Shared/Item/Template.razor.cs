// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL.TableModels;
using BlazorCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Template
    {
        #region Public and private fields and properties

        private TemplateEntity TemplateItem => IdItem is TemplateEntity idItem ? idItem : null;
        public List<TypeEntity<string>> TemplateCategories { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            await GetDataAsync(new Task(delegate
            {
                TemplateCategories = AppSettings.DataSource.GetTemplateCategories();
            }), false).ConfigureAwait(false);
        }

        private void OnChange(object value, string name)
        {
            switch (name)
            {
                case "TemlateCategories":
                    if (value is string strValue)
                    {
                        TemplateItem.CategoryId = strValue;
                    }
                    break;
            }
            StateHasChanged();
        }

        #endregion
    }
}
