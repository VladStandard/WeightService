using Blazor.Heroicons;
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
            new() { Name = Localizer["SectionFormRelogin"], IconName = HeroiconName.User });
    }
    
    private SqlUserEntity ProcessItem(SqlUserEntity item)
    {
        SqlUserEntity userEntity = SectionEntity.DeepClone();
        string userName = userEntity.Name;
        userName = userName.ToUpper();
        if (!userName.Contains(UserPrefix))
            userName += UserPrefix;
        userEntity.Name = userName;
        return userEntity;
    }
}