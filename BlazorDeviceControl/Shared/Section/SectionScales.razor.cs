// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
    public partial class SectionScales
    {
        #region Public and private fields and properties

        private List<ScaleEntity>? ItemsCast => Items?.Select(x => (ScaleEntity)x).ToList();

        #endregion

        #region Constructor and destructor

        public SectionScales() : base()
        {
            //Default();
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                Table = new TableScaleEntity(ProjectsEnums.TableScale.Scales);
                Items = null;
                ButtonSettings = new();
                IsBusy = false;
            }
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    if (!IsBusy)
                    {
                        IsBusy = true;
                        if (AppSettings.DataAccess != null)
                            Items = AppSettings.DataAccess.Crud.GetEntities<ScaleEntity>(
                                (IsShowMarkedItems == true) ? null
                                    : new FieldListEntity(new Dictionary<string, object?> { { DbField.IsMarked.ToString(), false } }),
                                new FieldOrderEntity(DbField.Description, DbOrderDirection.Asc))
                            ?.ToList<BaseEntity>();
                        ButtonSettings = new(true, true, true, true, true, false, false);
                        IsBusy = false;
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
