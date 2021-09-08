// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;

namespace ScalesCore.Win.Proc.Helpers
{
    /// <summary>
    /// Помощник процессов.
    /// </summary>
    public sealed class ProcHelper
    {
        #region Design pattern "Singleton"

        private static readonly Lazy<ProcHelper> _instance = new Lazy<ProcHelper>(() => new ProcHelper());
        public static ProcHelper Instance => _instance.Value;
        private ProcHelper() { }

        #endregion

        #region Public methods

        /// <summary>
        /// Запустить процесс.
        /// </summary>
        public void RunSilent(string procName, string args, bool runAs)
        {
            Run(procName, args, runAs, ProcessWindowStyle.Hidden, true);
        }

        /// <summary>
        /// Запустить процесс.
        /// </summary>
        public void Run(string procName, string args, bool runAs, ProcessWindowStyle windowStyle, bool useShellExecute)
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo(procName, args)
                {
                    Verb = runAs ? "runas" : "",
                    WindowStyle = windowStyle,
                    UseShellExecute = useShellExecute,
                },
            };
            process.Start();
            process.WaitForExit();
        }

        #endregion
    }
}
