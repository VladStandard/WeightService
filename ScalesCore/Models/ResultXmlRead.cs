// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.ObjectModel;

namespace ScalesCore.Models
{
    public class ResultXmlRead
    {
        public ResultXmlRead() : this (false, string.Empty, new Collection<string>())
        {
            //
        }

        public ResultXmlRead(bool noError, string value) : this(noError, value, new Collection<string>())
        {
            //
        }

        public ResultXmlRead(bool noError, string value, Collection<string> str)
        {
            NoError = noError;
            Value = value;
            Str = str;
        }

        public bool NoError { get; set; }
        public string Value { get; set; }
        public Collection<string> Str { get; set; }

        public override string ToString()
        {
            return $"NoError: {NoError}; Value: {Value}; Str: {Str}.";
        }
    }
}
