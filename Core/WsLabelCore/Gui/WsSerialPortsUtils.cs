// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Gui;

/// <summary>
/// Serial port utils.
/// </summary>
public static class WsSerialPortsUtils
{
    public static void DefaultComPortName(ScaleModel scale, System.Windows.Controls.ComboBox fieldComPort, List<string> listComPorts)
    {
        try
        {
            if (scale is null)
                throw new ArgumentNullException(nameof(scale));

            // Текущий порт из настроек.
            if (!string.IsNullOrEmpty(scale.DeviceComPort))
            {
                bool isFind = false;
                foreach (string portName in listComPorts)
                {
                    if (portName.Equals(scale.DeviceComPort, StringComparison.InvariantCultureIgnoreCase))
                    {
                        isFind = true;
                        break;
                    }
                }
                if (!isFind)
                    fieldComPort.Items.Add(scale.DeviceComPort);
            }
            // Сортировка.
            listComPorts = listComPorts.OrderBy(o => o).ToList();
            // Заполнить список.
            foreach (string portName in listComPorts)
            {
                fieldComPort.Items.Add(portName);
                fieldComPort.Text = scale.DeviceComPort;
            }
        }
        catch (Exception ex)
        {
            WsWpfUtils.CatchException(ex, true, true);
        }
    }
}