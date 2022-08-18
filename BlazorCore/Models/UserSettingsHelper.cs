// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using static DataCore.ShareEnums;

namespace BlazorCore.Models;

public class UserSettingsHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static UserSettingsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static UserSettingsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public IdentityEntity Identity { get; }
    public DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;

    #endregion

    #region Constructor and destructor

    public UserSettingsHelper()
    {
        Identity = new();
    }

    #endregion

    #region Public and private methods

    public void SetupAccessRights()
    {
        AccessEntity access = DataAccess.Crud.GetEntity<AccessEntity>(
            new(new() { new(DbField.User, DbComparer.Equal, Identity.UserName) }));
        Identity.SetAccessRights(access.Rights);
        //object[] objects = dataAccess.Crud.GetEntitiesNativeObject(
        //    SqlQueries.DbServiceManaging.Tables.Access.GetAccessRights(Identity.Name),
        //    filePath, lineNumber, memberName);
        //if (objects.Length == 1)
        //{
        //    if (objects[0] is object[] { Length: 5 } item)
        //    {
        //        if (Guid.TryParse(Convert.ToString(item[0]), out _))
        //        {
        //            if (item[4] != null)
        //            {
        //                Identity.SetAccessRights((AccessRights)Convert.ToByte(item[4]));
        //                return;
        //            }
        //        }
        //    }
        //}
    }

    #endregion
}
