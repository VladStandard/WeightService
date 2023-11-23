using System.IO.Ports;
using Ws.Scales.Utils;
using Ws.Scales.Common;
using Ws.Scales.Enums;

namespace Ws.Scales.Main;

public class Scales : IScales
{ 
    private MassaCommandsEnum ActiveCommand { get; set; }
    
    private const int ReadTimeoutDefault = 0_100;
    private const int WriteTimeoutDefault = 0_100;
    private const int BaudRateDefault = 4_800;
    private const Parity ParityDefault = Parity.Even;
    private const StopBits StopBitsDefault = StopBits.One;
    private const int DataBitsDefault = 8;
    private const Handshake HandshakeDefault = Handshake.RequestToSend;
    private string ComPort { get; set; } = "COM6";
    private SerialPort Port { get; set; }

    public Scales(string comPort)
    {
        ActiveCommand = MassaCommandsEnum.None;
        
        Port = new();
                
        Port.ReadTimeout = ReadTimeoutDefault;
        Port.WriteTimeout = WriteTimeoutDefault;
        Port.BaudRate = BaudRateDefault;
        Port.Parity = ParityDefault;
        Port.StopBits = StopBitsDefault;
        Port.DataBits = DataBitsDefault;
        Port.Handshake = HandshakeDefault;
        Port.DataReceived += DataReceived; 
        ComPort = comPort;
    }


    public void DataReceived(Object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
    {
        
    }
    
    public bool Send(byte[] bytes)
    {
        if (!Port.IsOpen) return false;
        try
        {
            Port.Write(bytes, 0, bytes.Length);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public bool SendGetWeight()
    {
        ActiveCommand = MassaCommandsEnum.GetWeight;
        return Send(ScalesCommands.CmdGetWeight);
    }
    
    public void Dispose()
    {
        if (Port.IsOpen) Port.Close();
        Port.Dispose();
    }
}