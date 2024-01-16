using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Claims;
using Ws.StorageCore.Entities.SchemaRef.Users;

namespace DeviceControl.Features.Sections.Admin.Users;

public sealed partial class UsersCreateForm: SectionFormBase<SqlUserEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    private string UserPrefix { get; set; } = "KOLBASA-VS\\";
    private SqlClaimRepository RolesRepository { get; set; } = new();
    private IEnumerable<SqlClaimEntity> RolesEntities { get; set; } = [];
    private IEnumerable<SqlClaimEntity> SelectedRolesInternal { get; set; } = [];
    
    private IEnumerable<SqlClaimEntity> SelectedRoles
    {
        get => SelectedRolesInternal;
        set
        {
            SectionEntity.Claims = new HashSet<SqlClaimEntity>(value);
            SelectedRolesInternal = value;
        }
    }

    protected override void OnInitialized()
    {
        SectionEntity.Name = UserPrefix;
        SelectedRolesInternal = SectionEntity.Claims.ToList();
        RolesEntities = RolesRepository.GetEnumerable().ToList();
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