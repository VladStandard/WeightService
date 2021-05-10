// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using log4net;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Controls;
using WPF.Utils;

// ReSharper disable UnusedMember.Global
// ReSharper disable CommentTypo
// ReSharper disable CheckNamespace
// ReSharper disable StringLiteralTypo

namespace ZplSdkExamples.Models
{
    internal enum EnumLogType
    {
        Debug,
        Error,
        Fatal,
        Info,
        Warn,
    }

    public class LogHelper
    {
        #region Public and private fields and properties

        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private TextBox _textBox;

        #endregion

        #region Design pattern "Lazy Singleton"

        private static LogHelper _instance;
        public static LogHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public LogHelper()
        {
            SetupDefault();
        }

        public void SetupDefault()
        {
            _textBox = null;
        }

        public void Setup(TextBox textBox)
        {
            _textBox = textBox;
        }

        #endregion

        #region Public and private methods

        private void Message(string message, EnumLogType logType, [CallerFilePath] string sourceFilePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            var dt = DateTime.Now;
            var msg = $"[{dt.Year}-{dt.Month}-{dt.Day} {dt.Hour}:{dt.Minute}:{dt.Second}] {message}";
            var isDebug = false;
            switch (logType)
            {
                case EnumLogType.Debug:
                    _log?.Debug(msg);
                    InvokeTextBox.AddTextFormat(_textBox, dt, "Debug. " + message);
                    isDebug = true;
                    break;
                case EnumLogType.Error:
                    _log?.Error(msg);
                    InvokeTextBox.AddTextFormat(_textBox, dt, "Error. " + message);
                    isDebug = true;
                    break;
                case EnumLogType.Fatal:
                    _log?.Fatal(msg);
                    InvokeTextBox.AddTextFormat(_textBox, dt, "Fatal. " + message);
                    isDebug = true;
                    break;
                case EnumLogType.Info:
                    _log?.Info(msg);
                    InvokeTextBox.AddTextFormat(_textBox, dt, "Info. " + message);
                    break;
                case EnumLogType.Warn:
                    _log?.Warn(msg);
                    InvokeTextBox.AddTextFormat(_textBox, dt, "Warn. " + message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
            }
            if (isDebug)
            {
                var msgDebug = $"[{dt.Year}-{dt.Month}-{dt.Day} {dt.Hour}:{dt.Minute}:{dt.Second}] Файл {sourceFilePath}. Метод {memberName}. Строка {sourceLineNumber}.";
                _log?.Debug(msgDebug);
                InvokeTextBox.AddTextFormat(_textBox, dt, msgDebug);
            }
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public void Debug(string message, [CallerFilePath] string sourceFilePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Message(message, EnumLogType.Debug, sourceFilePath, memberName, sourceLineNumber);
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public void Error(string message, [CallerFilePath] string sourceFilePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Message(message, EnumLogType.Error, sourceFilePath, memberName, sourceLineNumber);
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public void Fatal(string message, [CallerFilePath] string sourceFilePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Message(message, EnumLogType.Fatal, sourceFilePath, memberName, sourceLineNumber);
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public void Info(string message, [CallerFilePath] string sourceFilePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Message(message, EnumLogType.Info, sourceFilePath, memberName, sourceLineNumber);
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public void Warn(string message, [CallerFilePath] string sourceFilePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Message(message, EnumLogType.Warn, sourceFilePath, memberName, sourceLineNumber);
        }

        #endregion
    }
}
