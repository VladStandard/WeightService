using Microsoft.AspNetCore.Components;
using Ws.StorageCore.Views.ViewDiagModels.TableSize;
using Ws.StorageCore.Views.ViewOtherModels.DbFileSizeInfo;


namespace DeviceControl.Features.Sections.Admin.Database;

public sealed partial class DatabasePage: ComponentBase
{
    private SqlViewTableSizeRepository SqlViewTableSizeRepository { get; } = new();
    private SqlViewDbFileSizeRepository SqlViewDbFileSizeRepository { get; } = new();
    private List<WsSqlViewDbFileSizeInfoModel> SectionItems { get; set; } = new();
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender) return;
        SectionItems = GetSqlSectionCast();
        StateHasChanged();
    }
    
    private List<WsSqlViewDbFileSizeInfoModel> GetSqlSectionCast()
    {
        List<WsSqlViewDbFileSizeInfoModel> sqlFiles = SqlViewDbFileSizeRepository.GetList();
        List<SqlViewTableSizeModel> sqlTables = SqlViewTableSizeRepository.GetEnumerable(new()).ToList();
        foreach (WsSqlViewDbFileSizeInfoModel sqlFile in sqlFiles)
            sqlFile.Tables.AddRange(sqlTables.Where(table => table.FileName == sqlFile.FileName));
        return sqlFiles;
    }
}