using System.Security.Claims;
using DeviceControl.Source.Shared.Auth.Policies;
using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using Force.DeepCloner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Line;
using Ws.Domain.Services.Features.Plu;
using Ws.Domain.Services.Features.User;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.Devices.Lines;

public sealed partial class LinePluDataGrid : SectionDataGridPageBase<PluLineEntity>
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private ILineService LineService { get; set; } = default!;
    [Inject] private IPluService PluService { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private AuthenticationStateProvider AuthProvider { get; set; } = default!;

    # endregion

    [CascadingParameter(Name = "DialogItem")] public LineEntity LineEntity { get; set; } = null!;

    private HashSet<PluEntity> SelectPluEntities { get; set; } = [];
    private HashSet<PluEntity> SelectedPluEntities { get; set; } = [];
    private HashSet<PluEntity> SelectedPluEntitiesCopy { get; set; } = [];
    private UserEntity User { get; set; } = new();
    private bool IsAllowToEdit { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        ClaimsPrincipal userPrincipal = (await AuthProvider.GetAuthenticationStateAsync()).User;
        if (userPrincipal is { Identity.Name: not null })
            User = UserService.GetItemByNameOrCreate(userPrincipal.Identity.Name);
        var isSeniorSupport = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.SupportSenior)).Succeeded;
        IsAllowToEdit = isSeniorSupport || (User.ProductionSite != null && User.ProductionSite.Equals(LineEntity.Warehouse.ProductionSite));

        SelectPluEntities = [.. PluService.GetAll()];
        SelectedPluEntities = [.. LineService.GetLinePlus(LineEntity)];
        SelectedPluEntitiesCopy = SelectedPluEntities.DeepClone();
    }

    private async Task SaveSelectedPluEntities()
    {
        foreach (PluEntity itemToDelete in SelectedPluEntitiesCopy.Except(SelectedPluEntities))
        {
            PluLineEntity? pluLineItem = SectionItems.SingleOrDefault(i => i.Plu.Equals(itemToDelete));
            if (pluLineItem != null) LineService.DeletePluLine(pluLineItem);
        }

        foreach (PluEntity pluEntity in SelectedPluEntities.Except(SelectedPluEntitiesCopy))
        {
            PluLineEntity pluLine = new() { Line = LineEntity, Plu = pluEntity };
            LineService.AddPluLine(pluLine);
        }

        await UpdateData();

        SelectedPluEntitiesCopy = SelectedPluEntities.DeepClone();
    }

    private void ResetSelectedPluEntities() => SelectedPluEntities = SelectedPluEntitiesCopy.DeepClone();

    protected override IEnumerable<PluLineEntity> SetSqlSectionCast() =>
        LineService.GetLinePlusFk(LineEntity);

    protected override async Task OpenItemInNewTab(PluLineEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPlus}/{item.Plu.Uid}");
}