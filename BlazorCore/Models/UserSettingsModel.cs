// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;
using DataCore.Sql.Models;
using static DataCore.ShareEnums;

namespace BlazorCore.Models;

public class UserSettingsModel
{
    #region Public and private fields, properties, constructor

    public IdentityModel Identity { get; }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public UserSettingsModel()
    {
        Identity = new();
    }

    #endregion

    #region Public and private methods

    public void SetupAccessRights()
    {
        SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new() { new(DbField.User, DbComparer.Equal, Identity.UserName) },
            null, 0, false, false);
        AccessModel access = DataAccessHelper.Instance.Crud.GetItemNotNull<AccessModel>(sqlCrudConfig);
        Identity.SetAccessRights(access.Rights);
    }

    #endregion
}
