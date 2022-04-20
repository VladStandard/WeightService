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

        public string Name { get; set; }
        public ShareEnums.AccessRights AccessRights { get; private set; }
        public string NameWithRights => $"{GetName()} [{GetDescriptionAccessRights(AccessRights)}]";
        public bool AccessRightsIsNone => (byte)AccessRights == (byte)ShareEnums.AccessRights.None;
        public bool AccessRightsIsRead => (byte)AccessRights >= (byte)ShareEnums.AccessRights.Read;
        public bool AccessRightsIsWrite => (byte)AccessRights >= (byte)ShareEnums.AccessRights.Write;
        public bool AccessRightsIsAdmin => (byte)AccessRights >= (byte)ShareEnums.AccessRights.Admin;

        #endregion

        #region Constructor and destructor

        public IdentityEntity(string name, ShareEnums.AccessRights accessRights)
        {
            Name = name;
            AccessRights = accessRights;
        }

        public IdentityEntity() : this(string.Empty, ShareEnums.AccessRights.None) { }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return
                $"{nameof(Name)}: {Name}. " + Environment.NewLine +
                AccessRights == null ? $"{nameof(AccessRights)}: null. " : $"{nameof(AccessRights)}: {AccessRights}. ";
        }

        public void SetAccessRights(ShareEnums.AccessRights accessRights) => AccessRights = accessRights;

        public void SetAccessRights(byte accessRights) => SetAccessRights((ShareEnums.AccessRights)accessRights);

        public string GetName() => string.IsNullOrEmpty(Name) ? LocaleCore.Strings.Main.DataLoading : Name;

        public static string GetDescriptionAccessRights(ShareEnums.AccessRights accessRights) => accessRights switch
        {
            ShareEnums.AccessRights.Read => LocaleCore.Strings.Main.AccessRightsRead,
            ShareEnums.AccessRights.Write => LocaleCore.Strings.Main.AccessRightsWrite,
            ShareEnums.AccessRights.Admin => LocaleCore.Strings.Main.AccessRightsAdmin,
            _ => LocaleCore.Strings.Main.AccessRightsNone,
        };

        public static string GetDescriptionAccessRights(byte accessRights) => GetDescriptionAccessRights((ShareEnums.AccessRights)accessRights);

        #endregion
    }
}
