using System.Security.Claims;
using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Services;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Line;
using Ws.Domain.Services.Features.User;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinesDataGrid : SectionDataGridBase<LineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private ILineService LineService { get; set; } = null!;
    [Inject] private IUserService UserDomainService { get; set; } = null!;
    [Inject] private UserService UserService { get; set; } = null!;

    private UserEntity User { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        ClaimsPrincipal? userClaims = await UserService.GetUser();
        if (userClaims is { Identity.Name: not null })
            User = UserDomainService.GetItemByNameOrCreate(userClaims.Identity.Name);
    }
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<LinesCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(LineEntity item)
        => await OpenSectionModal<LinesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(LineEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionLines}/{item.Uid.ToString()}");

    protected override IEnumerable<LineEntity> SetSqlSectionCast()
    {
        if (User.ProductionSite == null) return [];
        return LineService.GetAllByProductionSite(User.ProductionSite)
            .OrderBy(item => item.Number).ToList();
    }

    protected override IEnumerable<LineEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [LineService.GetItemByUid(itemUid)];
    }
}