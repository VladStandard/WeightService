// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;

namespace BlazorCore.Models;

public class IdentityModel
{
    #region Public and private fields, properties, constructor

    public bool AccessRightsIsAdmin => (byte)AccessRights >= (byte)ShareEnums.AccessRights.Admin;
    public bool AccessRightsIsNone => (byte)AccessRights == (byte)ShareEnums.AccessRights.None;
    public bool AccessRightsIsRead => (byte)AccessRights >= (byte)ShareEnums.AccessRights.Read;
    public bool AccessRightsIsWrite => (byte)AccessRights >= (byte)ShareEnums.AccessRights.Write;
    public ShareEnums.AccessRights AccessRights { get; private set; }
    public string Id { get; set; }
    public string IpAddress { get; set; }
    public string UserName { get; set; }

    #endregion

    #region Constructor and destructor

    public IdentityModel(ShareEnums.AccessRights accessRights, string userName, string id, string ipAddress)
    {
        AccessRights = accessRights;
        Id = id;
        IpAddress = ipAddress;
        UserName = userName;
    }

    public IdentityModel() : this(ShareEnums.AccessRights.None, string.Empty, string.Empty, string.Empty) { }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        return
            $"{nameof(UserName)}: {UserName}. " + Environment.NewLine +
            $"{nameof(AccessRights)}: {AccessRights}. ";
    }

    private void SetAccessRights(ShareEnums.AccessRights accessRights) => AccessRights = accessRights;

    public void SetAccessRights(byte accessRights) => SetAccessRights((ShareEnums.AccessRights)accessRights);

    public string GetDescriptionAccessRights(ShareEnums.AccessRights accessRights)
    {
        if (accessRights == ShareEnums.AccessRights.None)
            accessRights = AccessRights;
        return accessRights switch
        {
            ShareEnums.AccessRights.Read => LocaleCore.Strings.AccessRightsRead,
            ShareEnums.AccessRights.Write => LocaleCore.Strings.AccessRightsWrite,
            ShareEnums.AccessRights.Admin => LocaleCore.Strings.AccessRightsAdmin,
            _ => LocaleCore.Strings.AccessRightsNone,
        };
    }

    public string GetDescriptionAccessRights(byte accessRights) => GetDescriptionAccessRights((ShareEnums.AccessRights)accessRights);

    private string GetColorAccessRights(ShareEnums.AccessRights accessRights, uint rowCounter)
    {
        if (accessRights == ShareEnums.AccessRights.None)
            accessRights = AccessRights;
        return accessRights switch
        {
            ShareEnums.AccessRights.Read => rowCounter % 2 == 0 ? "rz-datatable-even-read" : "rz-datatable-odd-read",
            ShareEnums.AccessRights.Write => rowCounter % 2 == 0 ? "rz-datatable-even-write" : "rz-datatable-odd-write",
            ShareEnums.AccessRights.Admin => rowCounter % 2 == 0 ? "rz-datatable-even-admin" : "rz-datatable-odd-admin",
            _ => rowCounter % 2 == 0 ? "rz-datatable-even-none" : "rz-datatable-odd-none",
        };
    }

    public string GetColorAccessRights(byte accessRights, uint rowCounter) => GetColorAccessRights((ShareEnums.AccessRights)accessRights, rowCounter);

    public string GetColorAccessRights(ShareEnums.AccessRights accessRights)
    {
        if (accessRights == ShareEnums.AccessRights.None)
            accessRights = AccessRights;
        return accessRights switch
        {
            ShareEnums.AccessRights.Read => "rz-datatable-read",
            ShareEnums.AccessRights.Write => "rz-datatable-write",
            ShareEnums.AccessRights.Admin => "rz-datatable-admin",
            _ => ".rz-datatable-none",
        };
    }

    public string GetColorAccessRights(byte accessRights) => GetColorAccessRights((ShareEnums.AccessRights)accessRights);

    #endregion
}
