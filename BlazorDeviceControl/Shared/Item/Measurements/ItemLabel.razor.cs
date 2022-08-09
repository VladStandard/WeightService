// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item.Measurements
{
    public partial class ItemLabel
    {
		#region Public and private fields, properties, constructor

		private LabelEntity ItemCast { get => Item == null ? new() : (LabelEntity)Item; set => Item = value; }

		public ItemLabel()
		{
            Table = new TableScaleEntity(ProjectsEnums.TableScale.Labels);
            ItemCast = new();
            ButtonSettings = new();
		}

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
	        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);
	        SetParametersAsyncWithAction(parameters, () => base.SetParametersAsync(parameters).ConfigureAwait(true),
		        null, () =>
                new Task(() =>
                {
					switch (TableAction)
                    {
                        case DbTableAction.New:
                            ItemCast = new();
                            ItemCast.ChangeDt = ItemCast.CreateDt = System.DateTime.Now;
                            break;
                        default:
                            ItemCast = AppSettings.DataAccess.Crud.GetEntity<LabelEntity>(
                                new(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
                            break;
                    }
                    ButtonSettings = new(false, false, false, false, false, false, true);
				});
        }

        #endregion
    }
}
