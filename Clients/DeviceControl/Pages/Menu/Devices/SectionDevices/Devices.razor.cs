// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.DeviceTypesFks;
using WsStorageCore.ViewScaleModels;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionDevices;

public sealed partial class Devices : RazorComponentSectionBase<DeviceView>
{
    #region Public and private fields, properties, constructor


    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        var query = WsSqlQueriesDiags.Tables.Views.GetDevices(
            SqlCrudConfigSection.IsResultShowOnlyTop
                ? ContextManager.JsonSettings.Local.SelectTopRowsCount
                : 0,
            SqlCrudConfigSection.IsResultShowMarked
        );
        object[] objects = ContextManager.AccessManager.AccessList.GetArrayObjectsNotNullable(
            query
        );
        List<DeviceView> items = new();
        foreach (var obj in objects)
        {
            if (obj is not object[] { Length: 8 } item)
                continue;
            if (Guid.TryParse(Convert.ToString(item[0]), out var uid))
            {
                items.Add(new ()
                {
                    IdentityValueUid = uid,
                    IsMarked = Convert.ToBoolean(item[1]),
                    LoginDate = Convert.ToDateTime(item[2]),
                    LogoutDate = Convert.ToDateTime(item[3]),
                    Name = item[4] as string ?? string.Empty,
                    TypeName = item[5] as string ?? string.Empty,
                    Ip = item[6] as string ?? string.Empty,
                    Mac = item[7] as string ?? string.Empty
                });
            }
        }
        SqlSectionCast = items;
    }

    #endregion
}
