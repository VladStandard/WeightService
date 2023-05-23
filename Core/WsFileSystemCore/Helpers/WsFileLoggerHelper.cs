// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsFileSystemCore.Helpers;

public class WsFileLoggerHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsFileLoggerHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsFileLoggerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);
    private string LogFileName { get; set; } = "";

    #endregion

    #region Public and private methods

    public void Setup(string dir, string file)
    {
        if (!Directory.Exists(dir) || string.IsNullOrEmpty(file)) return;
        LogFileName = Path.Combine(dir, $"{file}.txt");
        StoreMessage($"Debug mode: {DebugHelper.Instance.IsDevelop}");
    }

    private void StoreCore(Action<StreamWriter> action)
    {
        if (string.IsNullOrEmpty(LogFileName)) return;

        StreamWriter streamWriter = File.Exists(LogFileName) ? File.AppendText(LogFileName) : File.CreateText(LogFileName);
        action(streamWriter);
        streamWriter.Close();
        streamWriter.Dispose();
    }

    private void StoreMessage(string message) => 
        StoreCore(streamWriter => {
            streamWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {message}");
        });

    public void StoreException(Exception ex, [CallerFilePath] string filePath = "", 
        [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        StoreCore(streamWriter => {
            streamWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}");
            streamWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {ex.Message}");
            if (ex.InnerException is not null)
                streamWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {ex.InnerException.Message}");
        });
    }

    public void StoreExceptionWithParams(Exception ex, string filePath, int lineNumber, string memberName)
    {
        StoreCore(streamWriter => {
            streamWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}");
            streamWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {ex.Message}");
            if (ex.InnerException is not null)
                streamWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {ex.InnerException.Message}");
        });
    }

    #endregion
}