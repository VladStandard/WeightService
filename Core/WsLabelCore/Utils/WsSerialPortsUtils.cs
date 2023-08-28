namespace WsLabelCore.Utils;

/// <summary>
/// Утилиты последовательного порта.
/// </summary>
#nullable enable
public static class WsSerialPortsUtils
{
    public static void DefaultComPortName(WsSqlScaleModel scale, System.Windows.Controls.ComboBox fieldComPort, List<string> listComPorts)
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
            WsFormNavigationUtils.CatchExceptionSimple(ex);
        }
    }
}