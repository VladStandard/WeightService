// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorCore.CssStyles;

public class CssStyleRadzenColumnModel : CssStyleBase
{
	#region Public and private fields, properties, constructor

	public string Width { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public CssStyleRadzenColumnModel()
	{
		Width = string.Empty;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="width"></param>
	public CssStyleRadzenColumnModel(string width)
	{
		Width = width;
	}

	#endregion
}
