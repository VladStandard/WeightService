// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleModels.PlusScales;
using WsStorageCore.Tables.TableScaleModels.Scales;

namespace DeviceControl.Components.Nested.PlusLines;

public sealed partial class PlusLines : SectionBase<WsSqlPluScaleModel>
{
    #region Public and private fields, properties, constructor

    [Parameter] public WsSqlScaleModel Scale { get; set; }
    private bool HideNoneActivePlu { get; set; }

    public PlusLines() : base()
    {
        HideNoneActivePlu = true;
        SqlCrudConfigSection.IsResultOrder = true;
        ButtonSettings.IsShowSave = true;
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        // TODO: to repos
        if (!HideNoneActivePlu)
            SqlCrudConfigSection.ClearFilters();
        else
            SqlCrudConfigSection.AddFilters(
                    new WsSqlFieldFilterModel
                    {
                        Name = nameof(WsSqlPluScaleModel.IsActive),
                        Value = true
                    }
                );
        SqlSectionCast = WsSqlPluLineRepository.Instance.GetListByLine(Scale, SqlCrudConfigSection);
    }

    #endregion
}