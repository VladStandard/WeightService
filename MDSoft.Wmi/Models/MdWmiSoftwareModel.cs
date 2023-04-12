// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace MDSoft.Wmi.Models;

public class MdWmiSoftwareModel
{
    public MdWmiSoftwareModel(string name, string vendor, string version, string guid, string language)
    {
        Name = name;
        Vendor = vendor;
        Version = version;
        Guid = guid;
        Language = language;
    }

    public string Guid { get; init; }
    public string Language { get; init; }
    public string Name { get; init; }
    public string Vendor { get; init; }
    public string Version { get; init; }

    public override string ToString() =>
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Vendor)}: {Vendor}. " +
        $"{nameof(Version)}: {Version}. " +
        $"{nameof(Guid)}: {Guid}. " +
        $"{nameof(Language)}: {Language}";
}