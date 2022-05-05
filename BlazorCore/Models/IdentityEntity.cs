// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using System;

namespace BlazorCore.Models
{
    public class IdentityEntity
    {
        #region Public and private fields and properties

        public bool AccessRightsIsAdmin => (byte)AccessRights >= (byte)ShareEnums.AccessRights.Admin;
        public bool AccessRightsIsNone => (byte)AccessRights == (byte)ShareEnums.AccessRights.None;
        public bool AccessRightsIsRead => (byte)AccessRights >= (byte)ShareEnums.AccessRights.Read;
        public bool AccessRightsIsWrite => (byte)AccessRights >= (byte)ShareEnums.AccessRights.Write;
        public ShareEnums.AccessRights AccessRights { get; private set; }
        public string? Id { get; set; }
        public string? IpAddress { get; set; }
        public string? UserName { get; set; }

        #endregion

        #region Constructor and destructor

        public IdentityEntity(ShareEnums.AccessRights accessRights, string userName, string id, string ipAddress)
        {
            AccessRights = accessRights;
            Id = id;
            IpAddress = ipAddress;
            UserName = userName;
        }

        public IdentityEntity() : this(ShareEnums.AccessRights.None, string.Empty, string.Empty, string.Empty) { }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return
                $"{nameof(UserName)}: {UserName}. " + Environment.NewLine +
                AccessRights == null ? $"{nameof(AccessRights)}: null. " : $"{nameof(AccessRights)}: {AccessRights}. ";
        }

        public void SetAccessRights(ShareEnums.AccessRights accessRights) => AccessRights = accessRights;

        public void SetAccessRights(byte accessRights) => SetAccessRights((ShareEnums.AccessRights)accessRights);

        public string GetDescriptionAccessRights(ShareEnums.AccessRights accessRights = ShareEnums.AccessRights.None)
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

        #endregion
    }
}
