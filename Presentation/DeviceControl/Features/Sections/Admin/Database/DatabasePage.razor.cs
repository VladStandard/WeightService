// ReSharper disable ClassNeverInstantiated.Global
using Microsoft.AspNetCore.Components;
using Ws.Domain.Models.Entities;
using Ws.Domain.Services.Features.DatabaseFile;

namespace DeviceControl.Features.Sections.Admin.Database;

public sealed partial class DatabasePage: ComponentBase
{
    [Inject] private IDatabaseFileService DatabaseFileService { get; set; } = null!;
    private List<DbFileSizeInfoEntity> SectionItems { get; set; } = [];
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender) return;
        SectionItems = GetSqlSectionCast();
        StateHasChanged();
    }

    private List<DbFileSizeInfoEntity> GetSqlSectionCast() => DatabaseFileService.GetAll().ToList();
}