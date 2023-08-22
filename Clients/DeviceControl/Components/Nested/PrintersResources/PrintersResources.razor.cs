// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Components.Nested.PrintersResources;

public sealed partial class PrintersResources : SectionBase<WsSqlPrinterResourceFkModel>
{
    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlCrudConfigSection.AddFkIdentityFilter(nameof(WsSqlPrinterResourceFkModel.Printer), SqlItemCast);
        base.SetSqlSectionCast();
    }

    #endregion
}
