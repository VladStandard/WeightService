namespace DeviceControl.Pages.Menu.Devices.Lines;

public sealed partial class Lines : SectionBase<WsSqlScaleEntity>
{
    #region Public and private

    private WsSqlLineRepository LineRepository { get; } = new();

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = LineRepository.GetEnumerable(SqlCrudConfigSection).ToList();
    }
    
    #endregion
}
