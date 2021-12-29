// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataShareCore;
using DataShareCore.DAL.Interfaces;
using DataShareCore.DAL.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class TemplateResources
    {
        #region Public and private fields and properties

        private List<TemplateResourceEntity> ItemsCast => Items == null ? new List<TemplateResourceEntity>() : Items.Select(x => (TemplateResourceEntity)x).ToList();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    IdItem = null;
                    Items = null;
                    await GuiRefreshWithWaitAsync();

                    Items = AppSettings.DataAccess.Crud.GetEntities<TemplateResourceEntity>(
                        new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Marked.ToString(), false } }),
                        new FieldOrderEntity(ShareEnums.DbField.Type, ShareEnums.DbOrderDirection.Asc))
                        .ToList<BaseEntity>();
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
