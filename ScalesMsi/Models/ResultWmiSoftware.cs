// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesMsi.Models
{
    internal class ResultWmiSoftware
    {
        public ResultWmiSoftware() : this (string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
        {
            //
        }

        public ResultWmiSoftware(string name, string vendor, string version, string id, string language)
        {
            Name = name;
            Vendor = vendor;
            Version = version;
            Id = id;
            Language = language;
        }

        public string Name { get; set; }
        public string Vendor { get; set; }
        public string Version { get; set; }
        public string Id { get; set; }
        public string Language { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}; Vendor: {Vendor}; Version: {Version}; Id: {Id}; Language: {Language}.";
        }
    }
}
