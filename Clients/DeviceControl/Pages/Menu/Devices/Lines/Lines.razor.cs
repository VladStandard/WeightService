namespace DeviceControl.Pages.Menu.Devices.Lines;

public sealed partial class Lines : SectionBase<WsSqlViewLineModel>
{
    #region Public and private

    private WsSqlViewLineRepository ViewLineRepository { get; } = new();

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = ViewLineRepository.GetList(SqlCrudConfigSection).ToList();
    }
    
    #endregion
}
