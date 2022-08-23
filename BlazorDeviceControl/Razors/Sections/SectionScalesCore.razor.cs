// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionScalesCore : BlazorCore.Models.RazorBase
{
	#region Public and private fields, properties, constructor

	[Parameter] public bool IsPluNew { get; set; }
	private List<ScaleEntity> ItemsCast => Items == null ? new() : Items.Select(x => (ScaleEntity)x).ToList();

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableScaleEntity(ProjectsEnums.TableScale.Scales);
        Items = new();
	}

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        RunActions(new()
        {
            () =>
            {
                Items = AppSettings.DataAccess.Crud.GetEntities<ScaleEntity>(
                    IsShowMarkedItems ? null
                        : new FilterListEntity(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                    new(DbField.Description),
                    IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                    ?.ToList<BaseEntity>();
                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
