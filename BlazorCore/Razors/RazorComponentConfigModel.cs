// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorCore.Razors;

public class RazorComponentConfigModel
{
	#region Public and private fields, properties, constructor

	public bool IsShowFilterAdditional { get; set; }
	public bool IsShowFilterMarked { get; set; }
	public bool IsShowFilterOnlyTop { get; set; }
	public bool IsShowMarked { get; set; }
	public bool IsShowOnlyTop { get; set; }
	public bool IsShowItemsCount { get; set; }

	public RazorComponentConfigModel()
	{
		IsShowFilterOnlyTop = true;
		IsShowOnlyTop = true;
		IsShowFilterAdditional = false;
		IsShowFilterMarked = false;
		IsShowItemsCount = false;
		IsShowMarked = false;
	}

	#endregion
}
