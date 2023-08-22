// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.References.PlusStorage;

public sealed partial class PlusStorage : SectionBase<WsSqlPluStorageMethodModel>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlPluStorageMethodRepository().GetList(SqlCrudConfigSection);
    }
}
