namespace DeviceControl.Pages.Menu.Devices.Lines;

public sealed partial class Lines : SectionBase<SqlScaleEntity>
{
    #region Public and private

    private SqlLineRepository LineRepository { get; } = new();

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = LineRepository.GetEnumerable(SqlCrudConfigSection).ToList();
    }
    
    #endregion
}
