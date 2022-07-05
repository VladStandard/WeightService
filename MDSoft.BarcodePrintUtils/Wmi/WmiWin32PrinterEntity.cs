// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace MDSoft.BarcodePrintUtils.Wmi
{
    public class WmiWin32PrinterEntity
    {
        #region Public and private fields and properties

        public string Name { get; set; }
        public string DriverName { get; set; }
        public string PortName { get; set; }
        public string PrinterState { get; set; }
        public string Status { get; set; }
        public Win32PrinterStatusEnum PrinterStatus { get; set; }

        #endregion

        #region Constructor and destructor

        public WmiWin32PrinterEntity(string name, string driverName, string portName, string status, string printerState, Win32PrinterStatusEnum printerStatus)
        {
            Name = name;
            DriverName = driverName;
            PortName = portName;
            Status = status;
            PrinterState = printerState;
            PrinterStatus = printerStatus;
        }

        #endregion

        #region Public and private methods

        //

        #endregion
    }
}
