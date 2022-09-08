// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components;

namespace BlazorCore.Models;

public class RazorConfigModel
{
	#region Public and private fields, properties, constructor

	public bool IsShowFilterAdditional { get; set; }
	public bool IsShowFilterMarked { get; set; }
	public bool IsShowFilterOnlyTop { get; set; }
	public bool IsShowMarked { get; set; }
	public bool IsShowOnlyTop { get; set; }
	public bool IsShowItemsCount { get; set; }

	public RazorConfigModel()
	{
		IsShowFilterAdditional = false;
		IsShowFilterMarked = false;
		IsShowFilterOnlyTop = true;
		IsShowItemsCount = false;
		IsShowMarked = false;
		IsShowOnlyTop = true;
	}

	#endregion
}
