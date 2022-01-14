// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataProjectsCore.Models
{
    public class IdentityEntity
    {
        public string Name { get; set; }
        public bool? AccessLevel { get; set; }
        public bool IsAccess => AccessLevel == true;

        public IdentityEntity(string name, bool? accessLevel = null)
        {
            Name = name;
            AccessLevel = accessLevel;
        }

        public IdentityEntity() : this(string.Empty, null) { }

        public override string ToString()
        {
            return
                $"{nameof(Name)}: {Name}. " + Environment.NewLine +
                AccessLevel == null ? $"{nameof(AccessLevel)}: null. " : $"{nameof(AccessLevel)}: {AccessLevel}. ";
        }
    }
}
