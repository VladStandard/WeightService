// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using static DataCore.ShareEnums;

namespace BlazorCore.Models
{
    public class IdentityEntity
    {
        public string Name { get; set; }
        public AccessRights AccessRights { get; private set; }
        public void SetAccessRights(AccessRights accessRights)
        {
            AccessRights = accessRights;
        }
        public void SetAccessRights(byte accessRights)
        {
            AccessRights = (AccessRights)accessRights;
        }

        public IdentityEntity(string name, AccessRights accessRights)
        {
            Name = name;
            AccessRights = accessRights;
        }

        public IdentityEntity() : this(string.Empty, AccessRights.None) { }

        public override string ToString()
        {
            return
                $"{nameof(Name)}: {Name}. " + Environment.NewLine +
                AccessRights == null ? $"{nameof(AccessRights)}: null. " : $"{nameof(AccessRights)}: {AccessRights}. ";
        }
    }
}
