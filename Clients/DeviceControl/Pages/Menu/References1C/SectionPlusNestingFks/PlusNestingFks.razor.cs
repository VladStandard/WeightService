// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusNestingFks;
using WsStorageCore.Utils;
using WsStorageCore.TableScaleFkModels.PlusNestingFks;

namespace BlazorDeviceControl.Pages.Menu.References1C.SectionPlusNestingFks;

public sealed partial class PlusNestingFks : RazorComponentSectionBase<PluNestingFkModel>
{
    #region Public and private fields, properties, constructor

    public PlusNestingFks() : base()
    {
        ButtonSettings = new(true, true, true,
            true, true, true, false);
        SqlCrudConfigSection.NativeQuery = WsSqlQueriesScales.Tables.PluNestingFks.GetList(true);
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlCrudConfigSection.NativeParameters = new()
        {
            new("P_UID", SqlItem?.IdentityValueUid),
        };
        base.SetSqlSectionCast();
    }

    #endregion
}
