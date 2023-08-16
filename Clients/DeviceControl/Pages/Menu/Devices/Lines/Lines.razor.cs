// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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