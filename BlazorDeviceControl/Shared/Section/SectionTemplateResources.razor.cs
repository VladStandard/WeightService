// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Localizations;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class SectionTemplateResources
    {
        #region Public and private fields and properties

        private List<TemplateResourceEntity> ItemsCast => Items == null ? new() : Items.Select(x => (TemplateResourceEntity)x).ToList();
        
        #endregion

        #region Constructor and destructor

        public SectionTemplateResources() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.TemplatesResources);
            Items = new();
            ButtonSettings = new();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(SetParametersAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    if (AppSettings.DataAccess != null)
                        Items = AppSettings.DataAccess.Crud.GetEntities<TemplateResourceEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.IsMarked.ToString(), false } }),
                            new FieldOrderEntity(DbField.Type, DbOrderDirection.Asc), IsShowTop100 ? 100 : 0)
                        ?.ToList<BaseEntity>();
                    ButtonSettings = new(true, true, true, true, true, false, false);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
