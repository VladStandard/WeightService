﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Template
    {
        #region Public and private fields and properties

        public TemplateDirect TemplateItem { get => (TemplateDirect)IdItem; set => SetItem(value); }
        public List<TypeEntity<string>> TemplateCategories { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.Templates);
                        TemplateItem = null;
                        TemplateCategories = null;
                        await GuiRefreshWithWaitAsync();

                        TemplateItem = AppSettings.DataAccess.TemplatesCrud.GetEntity(new FieldListEntity(new Dictionary<string, object>
                            { { ShareEnums.DbField.Id.ToString(), Id } }), null);
                        TemplateCategories = AppSettings.DataSource.GetTemplateCategories();
                        await GuiRefreshWithWaitAsync();
                    }),
                }, true);
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
