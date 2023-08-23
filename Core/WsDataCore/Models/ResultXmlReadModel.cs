// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Models;

[DebuggerDisplay("{ToString()}")]
public class ResultXmlReadModel
{
    #region Public and private fields, properties, constructor

    public bool NoError { get; }
    public Collection<string> Str { get; }
    public string Value { get; }

    public ResultXmlReadModel() : this(false, string.Empty, new())
    {
        //
    }

    public ResultXmlReadModel(bool noError, string value) : this(noError, value, new())
    {
        //
    }

    public ResultXmlReadModel(bool noError, string value, Collection<string> str)
    {
        NoError = noError;
        Value = value;
        Str = str;
    }

    #endregion

    #region Public and private methods

    public override string ToString() => $"{NoError} | {Value} | {Str}";

    #endregion
}