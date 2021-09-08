// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// ReSharper disable HollowTypeName

namespace ScalesMsi.Helpers
{
    /// <summary>
    /// Помощник процессов.
    /// </summary>
    internal class ProcHelper
    {
        #region Design pattern "Lazy Singleton"

        private static ProcHelper _instance;
        public static ProcHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        private WmiHelper _wmi = WmiHelper.Instance;

        #endregion

        #region Constructor and destructor

        public ProcHelper() { SetupDefault(); }

        public void SetupDefault()
        {
            // Default methods
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Запустить процесс.
        /// </summary>
        public void Run(string procName, string args, bool runAs, ProcessWindowStyle windowStyle, bool useShellExecute, bool waitForExit)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(procName, args)
                {
                    Verb = runAs ? "runas" : "",
                    WindowStyle = windowStyle,
                    UseShellExecute = useShellExecute,
                },
            };
            process.Start();
            if (waitForExit)
                process.WaitForExit();
        }

        public async Task RunAsync(string procName, string args = "", bool runAs = false, ProcessWindowStyle windowStyle = ProcessWindowStyle.Normal,
            bool useShellExecute = true, bool waitForExit = false, bool isSilent = false)
        {
            try
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1000)).ConfigureAwait(false);
                Run(procName, args, runAs, windowStyle, useShellExecute, waitForExit);
                await Task.Delay(TimeSpan.FromMilliseconds(1000)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (!isSilent)
                {
                    var msg = ex.Message;
                    if (!(ex.InnerException is null))
                        msg += Environment.NewLine + ex.InnerException.Message;
                    MessageBox.Show(@"Ошибка запуска программы." + Environment.NewLine + msg);
                }
                throw;
            }
        }

        public async Task RunMsiAsync(string procName)
        {
            try
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1000)).ConfigureAwait(false);
                if (procName == null) throw new ArgumentNullException(nameof(procName));
                var fileName = _wmi.GetProcessMsi(procName);
                if (System.IO.File.Exists(fileName))
                {
                    Run(@"msiexec.exe", $@"/i ""{fileName}""", true, ProcessWindowStyle.Normal, true, false);
                }
                await Task.Delay(TimeSpan.FromMilliseconds(1000)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (!(ex.InnerException is null))
                    msg += Environment.NewLine + ex.InnerException.Message;
                MessageBox.Show(@"Ошибка запуска MSI." + Environment.NewLine + msg);
                throw;
            }
        }

        #endregion
    }
}
