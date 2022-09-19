// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ExampleComponents;

/// <summary>
/// MUD Blazor page.
/// </summary>
public partial class ExampleMud : RazorComponentBase
{
    #region Public and private fields, properties, constructor

    private string Country { get; set; } = "Hungary";
    private string ComPort { get; set; } = "COM10";
    private List<TypeModel<string>> ComPorts { get; set; }
    private List<string> ListComPorts { get; set; }

    public ExampleMud()
    {
	    ComPorts = new();
	    ListComPorts = new();
    }

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        RunActionsInitialized(new()
		{
			() =>
			{
		        ComPorts = SerialPortsUtils.GetListTypeComPorts(LangEnum.Russian);
		        ListComPorts = SerialPortsUtils.GetListComPorts(LangEnum.Russian);
			}
		});
	}

    #endregion
}
