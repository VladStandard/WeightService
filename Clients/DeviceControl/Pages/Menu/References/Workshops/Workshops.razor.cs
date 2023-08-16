// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.References.Workshops;

public sealed partial class Workshops : SectionBase<WsSqlWorkShopModel>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlWorkShopRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}