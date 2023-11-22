namespace Ws.MassaCore.Utils;

public static class SerialPortUtils
{
    #region Public and private methods
    
    public static SerialPort GetSerialPort(string portName, int readTimeout, int writeTimeout) => new(portName)
    {
        BaudRate = 4_800,
        Parity = Parity.Even,
        DataBits = 8,
        StopBits =  StopBits.One,
        Handshake = Handshake.RequestToSend,
        ReadTimeout = readTimeout,
        WriteTimeout = writeTimeout,
    };

    #endregion
}