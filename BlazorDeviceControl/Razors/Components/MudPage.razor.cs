// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Components;

/// <summary>
/// MUD Blazor page.
/// </summary>
public partial class MudPage : RazorPageModel
{
    #region Public and private fields, properties, constructor

    private string Country { get; set; } = "Hungary";
    private string ComPort { get; set; } = "COM10";
    List<TypeModel<string>> ComPorts { get; }
    private List<string> ListComPorts { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public MudPage()
    {
        ComPorts = SerialPortsUtils.GetListTypeComPorts(Lang.Russian);
        ListComPorts = SerialPortsUtils.GetListComPorts(Lang.Russian);
    }

    #endregion

    #region Public and private methods

    //

    #endregion
}
