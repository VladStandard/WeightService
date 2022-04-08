// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemLog
    {
        #region Public and private fields and properties

        public LogEntity? ItemCast { get => Item == null ? null : (LogEntity)Item; set => Item = value; }
        private readonly object _locker = new();

        #endregion

        #region Constructor and destructor

        public ItemLog()
        {
            Default();
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            lock (_locker)
            {
                Table = new TableSystemEntity(ProjectsEnums.TableSystem.Logs);
                ItemCast = null;
                ButtonSettings = new();
            }
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    lock (_locker)
                    {
                        switch (TableAction)
                        {
                            case DbTableAction.New:
                                ItemCast = new();
                                ItemCast.ChangeDt = ItemCast.CreateDt = System.DateTime.Now;
                                break;
                            default:
                                ItemCast = AppSettings.DataAccess.Crud.GetEntity<LogEntity>(
                                    new FieldListEntity(new Dictionary<string, object?> 
                                    { { DbField.IdentityUid.ToString(), Uid } }), null);
                                break;
                        }
                        ButtonSettings = new(false, false, false, false, false, false, true);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
