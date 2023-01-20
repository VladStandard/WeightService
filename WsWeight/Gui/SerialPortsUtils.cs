// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWeight.Gui;

public static class SerialPortsUtils
{
    public static void DefaultComPortName(ScaleModel scale, System.Windows.Controls.ComboBox fieldComPort, List<string> listComPorts)
    {
        try
        {
            if (scale is null)
                throw new ArgumentNullException(nameof(scale));

            // Текущий порт из настроек.
            string curPort = string.Empty;
            if (!string.IsNullOrEmpty(scale.DeviceComPort))
            {
                curPort = scale.DeviceComPort;
                if (!string.IsNullOrEmpty(curPort))
                {
                    bool find = false;
                    foreach (string portName in listComPorts)
                    {
                        if (portName.Equals(curPort, StringComparison.InvariantCultureIgnoreCase))
                        {
                            find = true;
                            break;
                        }
                    }
                    if (!find)
                        fieldComPort.Items.Add(curPort);
                }
            }
            // Сортировка.
            listComPorts = listComPorts.OrderBy(o => o).ToList();
            // Заполнить список.
            foreach (string portName in listComPorts)
            {
                fieldComPort.Items.Add(portName);
                fieldComPort.Text = curPort;
            }
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex, true, true);
        }
    }
}