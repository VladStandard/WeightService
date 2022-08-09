// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
	public partial class ItemNomenclature
	{
		#region Public and private fields, properties, constructor

		private NomenclatureEntity ItemCast { get => Item == null ? new() : (NomenclatureEntity)Item; set => Item = value; }

		#endregion

		public ItemNomenclature()
		{
			Table = new TableScaleEntity(ProjectsEnums.TableScale.Nomenclatures);
			ItemCast = new();
			ButtonSettings = new();
		}

		#region Public and private methods

		public override async Task SetParametersAsync(ParameterView parameters)
		{
			await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
			SetParametersAsyncWithAction(parameters, () => base.SetParametersAsync(parameters).ConfigureAwait(true),
				null, () =>
				{
					switch (TableAction)
					{
						case DbTableAction.New:
							ItemCast = new();
							ItemCast.ChangeDt = ItemCast.CreateDt = System.DateTime.Now;
							ItemCast.Name = "NEW NOMENCLATURE";
							break;
						default:
							ItemCast = AppSettings.DataAccess.Crud.GetEntity<NomenclatureEntity>(
								new(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
							break;
					}
					ButtonSettings = new(false, false, false, false, false, true, true);
				});
		}

		#endregion
	}
}