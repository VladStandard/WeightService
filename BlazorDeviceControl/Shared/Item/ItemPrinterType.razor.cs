// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item;

public partial class ItemPrinterType
{
    #region Public and private fields, properties, constructor

    private PrinterTypeEntity ItemCast { get => Item == null ? new() : (PrinterTypeEntity)Item; set => Item = value; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ItemPrinterType()
    {
        Table = new TableScaleEntity(ProjectsEnums.TableScale.PrintersTypes);
        ItemCast = new();
    }

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();
		SetParametersWithAction(new()
		{
			() =>
			{
				switch (TableAction)
				{
					case DbTableAction.New:
						ItemCast = new();
						ItemCast.ChangeDt = ItemCast.CreateDt = System.DateTime.Now;
						ItemCast.IsMarked = false;
						ItemCast.Name = "NEW PRINTER";
						break;
					default:
						ItemCast = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(
							new(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
						break;
				}

				if (IdentityId != null && TableAction == DbTableAction.New)
				{
					ItemCast.IdentityId = (long)IdentityId;
					ItemCast.Name = "NEW PRINTER_TYPE";
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

    #endregion
}
