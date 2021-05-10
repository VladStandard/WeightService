// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesCore.Models
{
    public class ResultWmiSoftware
    {
        public ResultWmiSoftware() : this (string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
        {
            //
        }

        public ResultWmiSoftware(string name, string vendor, string version, string guid, string language)
        {
            Name = name;
            Vendor = vendor;
            Version = version;
            Guid = guid;
            Language = language;
        }

        public string Name { get; set; }
        public string Vendor { get; set; }
        public string Version { get; set; }
        public string Guid { get; set; }
        public string Language { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}; Vendor: {Vendor}; Version: {Version}; Guid: {Guid}; Language: {Language}.";
        }
    }
}
