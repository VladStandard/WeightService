// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace terra.Common;

public class ErrorContainer
{
    #region Public and private fields and properties

    private List<string> ErrorList { get; } = new List<string>();

    #endregion

    #region Public and private methods

    public void Add(string error)
    {
        ErrorList.Add(error);
    }

    public void Clear()
    {
        ErrorList.Clear();
    }

    public XElement GetXElement()
    {
        return ErrorList.Count > 0
            ? new XElement("Errors", new XElement("Error", from msg in ErrorList select msg))
            : new XElement("Errors");
    }

    #endregion
}