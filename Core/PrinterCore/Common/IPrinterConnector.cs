using PrinterCore.Enums;

namespace PrinterCore.Common;

public interface IPrinterConnector : IDisposable
{

    public bool IsConnected { get; } 
    public void Connect(string ip);
    
    /// <summary>
    /// Отправляет команду на принтер
    /// </summary>
    /// <param name="cmd">Команда, которую необходимо отправить.</param>
    /// <returns>Возвращает <b>true</b>, если команда успешно выполнена; в противном случае возвращает <b>false</b>.</returns>
    public bool SendCommand(string cmd);
    
    public PrinterStates GetState();
    public void UpdateStatus();
}