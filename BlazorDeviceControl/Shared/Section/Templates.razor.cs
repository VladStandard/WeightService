// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Templates
    {
        #region Public and private fields and properties

        private List<TypeEntity<string>> TemplateCategories { get; set; }
        private string TemplateCategory { get; set; }
        private List<TemplateEntity> ItemsCast => Items == null ? new List<TemplateEntity>() : Items.Select(x => (TemplateEntity)x).ToList();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    lock (Locker)
                    {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.Templates);
                        // Filter.
                        TemplateCategories = AppSettings.DataSource.GetTemplateCategories();
                        if (string.IsNullOrEmpty(TemplateCategory))
                        {
                            TemplateCategory = TemplateCategories.FirstOrDefault()?.Value;
                            Items = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Marked.ToString(), false } }),
                                new FieldOrderEntity(ShareEnums.DbField.CategoryId, ShareEnums.DbOrderDirection.Asc))
                                .ToList<BaseEntity>();
                        }
                        else
                        {
                            Items = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                                new FieldListEntity(new Dictionary<string, object> {
                                { ShareEnums.DbField.Marked.ToString(), false },
                                { ShareEnums.DbField.CategoryId.ToString(), TemplateCategory },
                                }),
                                new FieldOrderEntity(ShareEnums.DbField.CategoryId, ShareEnums.DbOrderDirection.Asc))
                                .ToList<BaseEntity>();
                        }
                        ButtonSettings = new ButtonSettingsEntity(true, true, true, true, true, false, false);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }


        #endregion
    }
}
