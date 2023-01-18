// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Nomenclatures;

namespace BlazorDeviceControl.Razors.SectionComponents.References1C;

public partial class SectionNomenclatures : RazorComponentSectionBase<NomenclatureModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
	            SqlSectionCast = DataContext.GetListNotNullable<NomenclatureModel>(SqlCrudConfigSection);
            }
        });
    }

    #endregion
}