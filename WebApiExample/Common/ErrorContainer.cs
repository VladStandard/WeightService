// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WebApiExample.Common;

public class ErrorContainer
{

	private List<string> ErrorList = new();

	public void Add (string error)
	{
		ErrorList.Add(error);
	}

	public void Clear()
	{
		ErrorList.Clear();
	}

	public XElement GetXElement()
	{
		XElement errMessages;
		if (ErrorList.Count > 0)
		{
			errMessages = new XElement("Errors",
				new XElement("Error", from msg in ErrorList select msg)
			);
		}
		else
		{
			errMessages = new XElement("Errors");
		}
		return errMessages;
	}

}