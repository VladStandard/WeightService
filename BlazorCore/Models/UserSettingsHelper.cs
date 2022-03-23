// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Toolbelt.Blazor.HotKeys;
using static DataCore.ShareEnums;

namespace BlazorCore.Models
{
    public class UserSettingsHelper
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static UserSettingsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static UserSettingsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public IdentityEntity Identity { get; private set; }
        public HotKeys? HotKeys { get; private set; }
        public HotKeysContext? HotKeysContext { get; set; }

        #endregion

        #region Constructor and destructor

        public UserSettingsHelper()
        {
            Identity = new IdentityEntity();
            HotKeys = null;
            HotKeysContext = null;
        }

        #endregion

        #region Public and private methods

        public void SetupAccessRights(DataAccessEntity dataAccess)
        {
            if (dataAccess != null)
            {
                AccessEntity? access = dataAccess.Crud.GetEntity<AccessEntity>(
                    new FieldListEntity(new Dictionary<string, object?> { { DbField.User.ToString(), Identity.Name } }),
                    null
                );
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
        }

        public void SetupHotKeys(HotKeys hotKeys)
        {
            if (hotKeys != null)
            {
                if (HotKeys != null)
                {
                    _ = Task.Run(async () =>
                    {
                        await HotKeys.DisposeAsync().ConfigureAwait(true);
                        HotKeys = hotKeys;
                    }).ConfigureAwait(false);
                }
            }
        }

        #endregion
    }
}
