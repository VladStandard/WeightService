using System.Security.Claims;
using Force.DeepCloner;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Arms;
using Ws.Domain.Services.Features.Plus;
using Ws.Domain.Services.Features.Users;
using Claim = System.Security.Claims.Claim;

namespace DeviceControl.Source.Pages.Devices.Arms;

public sealed partial class ArmPluDataGrid : SectionDataGridPageBase<ArmPlu>
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IArmService ArmService { get; set; } = default!;
    [Inject] private IPluService PluService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;

    # endregion

    [CascadingParameter(Name = "DialogItem")] public Arm Arm { get; set; } = null!;
    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;

    private HashSet<Plu> SelectPluEntities { get; set; } = [];
    private HashSet<Plu> SelectedPluEntities { get; set; } = [];
    private HashSet<Plu> SelectedPluEntitiesCopy { get; set; } = [];
    private User User { get; set; } = new();
    private bool IsAllowToEdit { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        ClaimsPrincipal userPrincipal = (await AuthState).User;
        Claim? userIdClaim = userPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        if (Guid.TryParse(userIdClaim?.Value, out Guid userUid))
            User = UserService.GetItemByUid(userUid);
        bool isSeniorSupport = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.SupportSenior)).Succeeded;
        IsAllowToEdit = isSeniorSupport || (User.ProductionSite.Equals(Arm.Warehouse.ProductionSite));

        SelectPluEntities = [.. PluService.GetAll()];
        SelectedPluEntities = [.. ArmService.GetArmAllPlus(Arm)];
        SelectedPluEntitiesCopy = SelectedPluEntities.DeepClone();
    }

    private async Task SaveSelectedPluEntities()
    {
        foreach (Plu itemToDelete in SelectedPluEntitiesCopy.Except(SelectedPluEntities))
        {
            ArmPlu? pluLineItem = SectionItems.SingleOrDefault(i => i.Plu.Equals(itemToDelete));
            if (pluLineItem != null) ArmService.DeletePluLine(pluLineItem);
        }

        foreach (Plu pluEntity in SelectedPluEntities.Except(SelectedPluEntitiesCopy))
        {
            ArmPlu armPlu = new() { Line = Arm, Plu = pluEntity };
            ArmService.AddPluLine(armPlu);
        }

        await UpdateData();

        SelectedPluEntitiesCopy = SelectedPluEntities.DeepClone();
    }

    private void ResetSelectedPluEntities() => SelectedPluEntities = SelectedPluEntitiesCopy.DeepClone();

    protected override IEnumerable<ArmPlu> SetSqlSectionCast() => ArmService.GetLinePlusFk(Arm);

    protected override async Task OpenItemInNewTab(ArmPlu item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPlus}/{item.Plu.Uid}");
}