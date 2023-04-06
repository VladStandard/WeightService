// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Plus;

namespace BlazorDeviceControl.Pages.Menu.References1C.SectionNomenclatures;

public sealed partial class Nomenclatures : RazorComponentSectionBase<PluModel>
{
	#region Public and private fields, properties, constructor

	public Nomenclatures() : base()
	{
        SqlCrudConfigSection.IsResultOrder = false;
		SqlCrudConfigSection.AddOrders(new() { Name = $"{nameof(PluModel.Number)}", Direction = WsSqlOrderDirection.Asc });
        ButtonSettings = new(false, false, true, true, false, false, false);
    }

	#endregion

	#region Public and private methods

    #endregion
}