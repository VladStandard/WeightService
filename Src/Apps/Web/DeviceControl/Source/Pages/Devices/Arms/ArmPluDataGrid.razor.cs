using System.Security.Claims;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c.Plus;
using Ws.Domain.Services.Features.Arms;
using Ws.Domain.Services.Features.Plus;
using Ws.Shared.Enums;

namespace DeviceControl.Source.Pages.Devices.Arms;

public sealed partial class ArmPluDataGrid : SectionDataGridPageBase<Plu>
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IArmService ArmService { get; set; } = default!;
    [Inject] private IPluService PluService { get; set; } = default!;
    [Inject] private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;

    # endregion

    [CascadingParameter(Name = "DialogItem")] public Arm Arm { get; set; } = null!;
    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;
    [CascadingParameter] private ProductionSite UserProductionSite { get; set; } = default!;

    private HashSet<Plu> SelectPluEntities { get; set; } = [];
    private Plu? SelectedPlu { get; set; }
    private bool IsAllowToEdit { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        ClaimsPrincipal userPrincipal = (await AuthState).User;

        bool isSeniorSupport = (await AuthorizationService.AuthorizeAsync(userPrincipal, PolicyEnum.SeniorSupport)).Succeeded;
        IsAllowToEdit = isSeniorSupport || UserProductionSite.Equals(Arm.Warehouse.ProductionSite);

        SelectPluEntities = [..PluService.GetAll()];

        if (Arm.Type == ArmType.Pc)
            SelectPluEntities = SelectPluEntities.Where(x => x.IsCheckWeight == false).ToHashSet();
        else if(Arm.Type == ArmType.Tablet)
            SelectPluEntities = SelectPluEntities.Where(x => x.IsCheckWeight).ToHashSet();
    }

    private void OnPluSelected()
    {
        if (SelectedPlu == null) return;
        AddArmPlu(SelectedPlu);
        SelectedPlu = null;
    }

    private List<Plu> GetPluNotInSectionItems()
    {
        HashSet<Plu> sectionPluList = SectionItems.ToHashSet();
        return SelectPluEntities.Where(plu => !sectionPluList.Contains(plu)).ToList();
    }

    private Task DeleteArmPlu(Plu item)
    {
        try
        {
            ArmService.DeletePlu(Arm, item);
            SectionItems = SectionItems.Where(x => !x.Equals(item));
            ToastService.ShowSuccess(string.Format(Localizer["ArmPluRemoved"], item.Number));
        }
        catch
        {
            ToastService.ShowError(string.Format(Localizer["ArmPluRemovedUnsuccessfully"], item.Number));
        }
        return Task.CompletedTask;
    }

    private void AddArmPlu(Plu item)
    {
        try
        {
            ArmService.AddPlu(Arm, item);
            SectionItems = SectionItems.Union([item]);
            ToastService.ShowSuccess(string.Format(Localizer["ArmPluAdded"], item.Number));
        }
        catch
        {
            ToastService.ShowError(string.Format(Localizer["ArmPluAddedUnsuccessfully"], item.Number));
        }
    }

    protected override IEnumerable<Plu> SetSqlSectionCast() => ArmService.GetArmAllPlus(Arm);

    protected override async Task OpenItemInNewTab(Plu item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionPlus}/{item.Uid}");
}