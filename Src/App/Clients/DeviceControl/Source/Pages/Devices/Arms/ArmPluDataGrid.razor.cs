using System.Security.Claims;
using Force.DeepCloner;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Arms;
using Ws.Domain.Services.Features.Plus;
using Ws.Domain.Services.Features.Users;

namespace DeviceControl.Source.Pages.Devices.Arms;

public sealed partial class ArmPluDataGrid : SectionDataGridPageBase<ArmLine>
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IArmService ArmService { get; set; } = default!;
    [Inject] private IPluService PluService { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private AuthenticationStateProvider AuthProvider { get; set; } = default!;

    # endregion

    [CascadingParameter(Name = "DialogItem")] public Arm Arm { get; set; } = null!;

    private HashSet<Plu> SelectPluEntities { get; set; } = [];
    private HashSet<Plu> SelectedPluEntities { get; set; } = [];
    private HashSet<Plu> SelectedPluEntitiesCopy { get; set; } = [];
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
        SelectedPluEntities = [.. ArmService.GetLinePlus(Arm)];
        SelectedPluEntitiesCopy = SelectedPluEntities.DeepClone();
    }

    private async Task SaveSelectedPluEntities()
    {
        foreach (Plu itemToDelete in SelectedPluEntitiesCopy.Except(SelectedPluEntities))
        {
            ArmLine? pluLineItem = SectionItems.SingleOrDefault(i => i.Plu.Equals(itemToDelete));
            if (pluLineItem != null) ArmService.DeletePluLine(pluLineItem);
        }

        foreach (Plu pluEntity in SelectedPluEntities.Except(SelectedPluEntitiesCopy))
        {
            ArmLine armLine = new() { Line = Arm, Plu = pluEntity };
            ArmService.AddPluLine(armLine);
        }

        await UpdateData();

        SelectedPluEntitiesCopy = SelectedPluEntities.DeepClone();
    }

    private void ResetSelectedPluEntities() => SelectedPluEntities = SelectedPluEntitiesCopy.DeepClone();

    protected override IEnumerable<ArmLine> SetSqlSectionCast() =>
        ArmService.GetLinePlusFk(Arm);

    protected override async Task OpenItemInNewTab(ArmLine item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPlus}/{item.Plu.Uid}");
}