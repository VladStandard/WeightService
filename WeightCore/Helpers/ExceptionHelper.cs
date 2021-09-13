// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataProjectsCore.Helpers;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using WeightCore.Gui;

namespace WeightCore.Helpers
{
    public class ExceptionHelper
    {
        #region Design pattern "Lazy Singleton"

        private static ExceptionHelper _instance;
        public static ExceptionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public ExceptionHelper() { }

        #endregion

        #region Public and private fields and properties

        private readonly LogHelper _log = LogHelper.Instance;

        #endregion

        #region Public and private methods

        public void Catch(IWin32Window owner, ref Exception ex,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            _log.Error(ex.Message, filePath, memberName, lineNumber);
            if (ex.InnerException != null)
                _log.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
            string msg = ex.Message;
            if (ex.InnerException != null)
                msg += Environment.NewLine + ex.InnerException.Message;
            if (owner != null)
                CustomMessageBox.Show(owner, @$"{nameof(memberName)}: {memberName}. {nameof(lineNumber)}: {lineNumber}" + Environment.NewLine + msg,
                    LocalizationData.ScalesUI.Exception);
        }

        #endregion
    }
}
