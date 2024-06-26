using System.Security.Claims;
using Force.DeepCloner;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Features.Arms;
using Ws.Domain.Services.Features.Plus;
using Ws.Domain.Services.Features.Users;
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

    [CascadingParameter] private ProductionSite UserProductionSite { get; set; } = default!;
    private HashSet<Plu> SelectPluEntities { get; set; } = [];
    private HashSet<Plu> SelectedPluEntities { get; set; } = [];
    private HashSet<Plu> SelectedPluEntitiesCopy { get; set; } = [];
    private bool IsAllowToEdit { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        ClaimsPrincipal userPrincipal = (await AuthState).User;

        bool isSeniorSupport = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.SeniorSupport)).Succeeded;
        IsAllowToEdit = isSeniorSupport || UserProductionSite.Equals(Arm.Warehouse.ProductionSite);

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