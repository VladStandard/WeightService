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
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class SectionTemplates
    {
        #region Public and private fields and properties

        private List<TypeEntity<string>>? TemplateCategories { get; set; }
        private string? TemplateCategory { get; set; } = string.Empty;
        private List<TemplateEntity> ItemsCast => Items == null ? new() : Items.Select(x => (TemplateEntity)x).ToList();

        #endregion

        #region Constructor and destructor

        public SectionTemplates() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.Templates);
            TemplateCategories = DataSourceDicsEntity.GetTemplateCategories();
            TemplateCategory = null;
            Items = new();
            ButtonSettings = new();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    if (string.IsNullOrEmpty(TemplateCategory))
                    {
                        TemplateCategory = TemplateCategories.FirstOrDefault()?.Value;
                        if (AppSettings.DataAccess != null)
                            Items = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                                (IsShowMarkedItems == true) ? null
                                    : new FieldListEntity(new Dictionary<string, object?> { { DbField.IsMarked.ToString(), false } }),
                                new FieldOrderEntity(DbField.CategoryId, DbOrderDirection.Asc), IsShowTop100 ? 100 : 0)
                            ?.ToList<BaseEntity>();
                    }
                    else
                    {
                        if (AppSettings.DataAccess != null)
                            Items = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                                (IsShowMarkedItems == true)
                                    ? new FieldListEntity(new Dictionary<string, object?> {
                                        { DbField.CategoryId.ToString(), TemplateCategory } })
                                    : new FieldListEntity(new Dictionary<string, object?> {
                                        { DbField.IsMarked.ToString(), false }, { DbField.CategoryId.ToString(), TemplateCategory } }),
                                new FieldOrderEntity(DbField.CategoryId, DbOrderDirection.Asc), IsShowTop100 ? 100 : 0)
                            ?.ToList<BaseEntity>();
                    }
                    ButtonSettings = new(true, true, true, true, true, false, false);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
