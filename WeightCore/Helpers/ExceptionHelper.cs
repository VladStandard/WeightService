// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
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

        #region Public and private fields and properties

        public DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;

        #endregion

        #region Constructor and destructor

        public ExceptionHelper() { }

        #endregion

        #region Public and private fields and properties

        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public void Catch(IWin32Window owner, ref Exception ex, bool isShowException,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            lock (_locker)
            {
                DataAccess.Log.LogError(ex, null, null, filePath, lineNumber, memberName);
                string message = ex.Message;
                if (ex.InnerException != null)
                    message += Environment.NewLine + ex.InnerException.Message;

                if (isShowException)
                {
                    GuiUtils.WpfForm.ShowNewCatch(owner, message, true, filePath, lineNumber, memberName);
                }
            }
        }

        public void Catch(IWin32Window owner, ref TaskCanceledException tcex, bool isShowException,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            Exception ex = (tcex.InnerException != null) ? new(tcex.Message, tcex.InnerException) : new(tcex.Message);
            Catch(owner, ref ex, isShowException, filePath, lineNumber, memberName);
        }

        #endregion
    }
}
