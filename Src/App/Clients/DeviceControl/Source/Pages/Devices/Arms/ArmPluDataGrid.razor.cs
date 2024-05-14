using System.Security.Claims;
using Force.DeepCloner;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Line;
using Ws.Domain.Services.Features.Plu;
using Ws.Domain.Services.Features.User;

namespace DeviceControl.Source.Pages.Devices.Arms;

public sealed partial class ArmPluDataGrid : SectionDataGridPageBase<ArmLine>
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

    [CascadingParameter(Name = "DialogItem")] public Arm Arm { get; set; } = null!;

    private HashSet<PluEntity> SelectPluEntities { get; set; } = [];
    private HashSet<PluEntity> SelectedPluEntities { get; set; } = [];
    private HashSet<PluEntity> SelectedPluEntitiesCopy { get; set; } = [];
    private User User { get; set; } = new();
    private bool IsAllowToEdit { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        ClaimsPrincipal userPrincipal = (await AuthProvider.GetAuthenticationStateAsync()).User;
        if (userPrincipal is { Identity.Name: not null })
            User = UserService.GetItemByNameOrCreate(userPrincipal.Identity.Name);
        var isSeniorSupport = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.SupportSenior)).Succeeded;
        IsAllowToEdit = isSeniorSupport || (User.ProductionSite != null && User.ProductionSite.Equals(Arm.Warehouse.ProductionSite));

        SelectPluEntities = [.. PluService.GetAll()];
        SelectedPluEntities = [.. LineService.GetLinePlus(Arm)];
        SelectedPluEntitiesCopy = SelectedPluEntities.DeepClone();
    }

    private async Task SaveSelectedPluEntities()
    {
        foreach (PluEntity itemToDelete in SelectedPluEntitiesCopy.Except(SelectedPluEntities))
        {
            ArmLine? pluLineItem = SectionItems.SingleOrDefault(i => i.Plu.Equals(itemToDelete));
            if (pluLineItem != null) LineService.DeletePluLine(pluLineItem);
        }

        foreach (PluEntity pluEntity in SelectedPluEntities.Except(SelectedPluEntitiesCopy))
        {
            ArmLine armLine = new() { Line = Arm, Plu = pluEntity };
            LineService.AddPluLine(armLine);
        }

        await UpdateData();

        SelectedPluEntitiesCopy = SelectedPluEntities.DeepClone();
    }

    private void ResetSelectedPluEntities() => SelectedPluEntities = SelectedPluEntitiesCopy.DeepClone();

    protected override IEnumerable<ArmLine> SetSqlSectionCast() =>
        LineService.GetLinePlusFk(Arm);

    protected override async Task OpenItemInNewTab(ArmLine item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPlus}/{item.Plu.Uid}");
}