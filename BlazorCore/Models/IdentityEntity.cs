using System;

namespace BlazorCore.Models
{
    public class IdentityEntity
    {
        public string Name { get; set; }
        public bool? AccessLevel { get; set; }
        public bool IsAccess { get; set; }

        public IdentityEntity(string name, bool? accessLevel = null)
        {
            Name = name;
            AccessLevel = accessLevel;
            IsAccess = false;
        }

        public override string ToString()
        {
            return
                $"{nameof(Name)}: {Name}. " + Environment.NewLine +
                $"{nameof(AccessLevel)}: {AccessLevel}. ";
        }
    }
}