using Blazor.Heroicons;
using Blazorise;
using DeviceControl.Auth.Common;
using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Claims;
using Ws.StorageCore.Entities.SchemaRef.Users;

namespace DeviceControl.Features.Sections.Admin.Users;

public sealed partial class UsersUpdateForm: SectionFormBase<SqlUserEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IUserCacheService UserCacheService { get; set; } = null!;
    [Inject] private INotificationService NotificationService { get; set; } = null!;
    
    private string UserPrefix { get; set; } = "KOLBASA-VS\\";
    private SqlClaimRepository RolesRepository { get; set; } = new();
    private IEnumerable<SqlClaimEntity> RolesEntities { get; set; } = [];
    private IEnumerable<SqlClaimEntity> SelectedRoles
    {
        get => SectionEntity.Claims.ToList();
        set => SectionEntity.Claims = new HashSet<SqlClaimEntity>(value);
    }

    private IEnumerable<ActionMenuEntry> AdditionalButtons { get; set; } = [];

    protected override void OnInitialized()
    {
        SelectedRoles = SectionEntity.Claims.ToList();
        RolesEntities = RolesRepository.GetEnumerable().ToList();
        AdditionalButtons = AdditionalButtons.Append(
            new() { Name = Localizer["SectionFormRelogin"], IconName = HeroiconName.User, 
                OnClickAction = EventCallback.Factory.Create(this, ReloginCurrentUser)});
    }
    
    private async Task ReloginCurrentUser()
    {
        UserCacheService.ClearCacheForUser(SectionEntity.Name);
        await NotificationService.Info("Релогин выполнен");
    }
    
    private SqlUserEntity ReloginUser(SqlUserEntity user)
    {
        UserCacheService.ClearCacheForUser(user.Name);
        return SectionEntity;
    }
}