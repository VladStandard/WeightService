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
