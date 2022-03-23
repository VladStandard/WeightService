// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using System;

namespace BlazorCore.Models
{
    public class IdentityEntity
    {
        #region Public and private fields and properties

        public string Name { get; set; }
        public ShareEnums.AccessRights AccessRights { get; private set; }
        public string NameWithRights => $"{GetName()} [{GetDescriptionAccessRights(AccessRights)}]";

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

        public string GetName() => string.IsNullOrEmpty(Name) ? LocalizationCore.Strings.Main.DataLoading : Name;

        public static string GetDescriptionAccessRights(ShareEnums.AccessRights accessRights) => accessRights switch
        {
            ShareEnums.AccessRights.Read => LocalizationCore.Strings.Main.AccessRightsRead,
            ShareEnums.AccessRights.Write => LocalizationCore.Strings.Main.AccessRightsWrite,
            ShareEnums.AccessRights.Admin => LocalizationCore.Strings.Main.AccessRightsAdmin,
            _ => LocalizationCore.Strings.Main.AccessRightsNone,
        };

        public static string GetDescriptionAccessRights(byte accessRights) => GetDescriptionAccessRights((ShareEnums.AccessRights)accessRights);

        #endregion
    }
}
