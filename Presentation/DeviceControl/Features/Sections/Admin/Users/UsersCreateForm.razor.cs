using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Users;
using Ws.StorageCore.Entities.SchemaRef.WorkShops;

namespace DeviceControl.Features.Sections.Admin.Users;

public sealed partial class UsersCreateForm: SectionFormBase<SqlUserEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    private string UserPrefix { get; set; } = "KOLBASA-VS\\";

    protected override void OnInitialized()
    {
        SectionEntity.Name = UserPrefix;
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