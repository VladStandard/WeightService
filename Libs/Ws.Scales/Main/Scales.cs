using System.IO.Ports;
using Ws.Scales.Common;
using Ws.Scales.Enums;

namespace Ws.Scales.Main;

public class Scales : IScales
{ 
    private MassaCommandsEnum ActiveCommand { get; set; }
    private SerialPort Port { get; set; }
    private IScalesCommands Commands { get; set; }
    
    public Scales(string comPort)
    {
        Commands = new MassaKCommands();
        
        ActiveCommand = MassaCommandsEnum.None;
        
        Port = new(comPort);
        Port.Open();
        
        Port.ReadTimeout = 0_100;
        Port.WriteTimeout = 0_100;
        Port.BaudRate = 4_800;
        Port.Parity = Parity.Even;
        Port.StopBits = StopBits.One;
        Port.DataBits = 8;
        Port.Handshake = Handshake.RequestToSend;

        Port.DataReceived += DataReceived;

        SubscribeToData();
    }
    
    private void SubscribeToData()
    {
        Port.DataReceived += DataReceived; 
    }
    
    private void UnSubscribeFromData()
    {
        Port.DataReceived -= DataReceived; 
    }
    
    public void DataReceived(Object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
    {

    }

    private bool Send(byte[] bytes)
    {
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
        return Send(Commands.CmdGetWeight);
    }
    
    public void Dispose()
    {
        UnSubscribeFromData();
        if (Port.IsOpen) Port.Close();
        Port.Dispose();
    }
}