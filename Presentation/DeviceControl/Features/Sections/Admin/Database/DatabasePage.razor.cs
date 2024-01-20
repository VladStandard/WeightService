// ReSharper disable ClassNeverInstantiated.Global
using Microsoft.AspNetCore.Components;
using Ws.Domain.Models.Entities;
using Ws.Domain.Models.Entities.Diag;
using Ws.StorageCore.Entities;
using Ws.StorageCore.Entities.Diag.TableSizes;

namespace DeviceControl.Features.Sections.Admin.Database;

public sealed partial class DatabasePage: ComponentBase
{
    private SqlViewTableSizeRepository SqlViewTableSizeRepository { get; } = new();
    private SqlViewDbFileSizeRepository SqlViewDbFileSizeRepository { get; } = new();
    private List<WsSqlViewDbFileSizeInfoModel> SectionItems { get; set; } = [];
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender) return;
        SectionItems = GetSqlSectionCast();
        StateHasChanged();
    }
    
    private List<WsSqlViewDbFileSizeInfoModel> GetSqlSectionCast()
    {
        List<WsSqlViewDbFileSizeInfoModel> sqlFiles = SqlViewDbFileSizeRepository.GetList();
        List<TableSizeEntity> sqlTables = SqlViewTableSizeRepository.GetEnumerable().ToList();
        foreach (WsSqlViewDbFileSizeInfoModel sqlFile in sqlFiles)
            sqlFile.Tables.AddRange(sqlTables.Where(table => table.FileName == sqlFile.FileName));
        return sqlFiles;
    }
}