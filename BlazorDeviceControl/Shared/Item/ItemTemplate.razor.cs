// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item;

public partial class ItemTemplate
{
    #region Public and private fields, properties, constructor

    private TemplateEntity ItemCast { get => Item == null ? new() : (TemplateEntity)Item; set => Item = value; }
    private List<TypeEntity<string>>? TemplateCategories { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ItemTemplate()
    {
        Table = new TableScaleEntity(ProjectsEnums.TableScale.Templates);
        TemplateCategories = DataSourceDicsEntity.GetTemplateCategories();
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
						ItemCast.Title = "NEW TEMPLATE";
						ItemCast.IdRRef = System.Guid.Empty;
						ItemCast.CategoryId = "300 dpi";
						ItemCast.ImageData.SetTemplateValue();
						break;
					default:
						ItemCast = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
							new(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
						break;
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

    #endregion
}
