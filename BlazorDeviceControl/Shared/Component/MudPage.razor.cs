using DataCore.Models;
using DataCore.Utils;
using System.Collections.Generic;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Component
{
    public partial class MudPage
    {
        private string Country { get; set; } = "Hungary";
        private string ComPort { get; set; } = "COM10";
        List<TypeEntity<string>>? ComPorts { get; set; }
        private List<string>? ListComPorts { get; set; }

        public MudPage()
        {
            ComPorts = PortUtils.GetListTypeComPorts(Lang.Russian);
            ListComPorts = PortUtils.GetListComPorts(Lang.Russian);
        }
    }
}
