// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.Helpers;
using Nito.AsyncEx;
using System;
using System.Threading;
using System.Threading.Tasks;
using WeightCore.Helpers;

namespace WeightCore.Managers
{
    public class ManagerHelper
    {
        #region Design pattern "Lazy Singleton"

        private static ManagerHelper _instance;
        public static ManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        ~ManagerHelper()
        {
            Close();
            //ClosePrintManager();
        }

        #endregion

        #region Public and private fields and properties

        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        private readonly DebugHelper _debug = DebugHelper.Instance;
        public SqlViewModelEntity SqlViewModel { get; set; } = SqlViewModelEntity.Instance;
        private readonly LogHelper _log = LogHelper.Instance;

        public string MassaManagerProgressString { get; set; }
        public string MassaQueriesProgressString { get; set; }
        public string MassaRequestProgressString { get; set; }
        public string MassaResponseProgressString { get; set; }

        // PrintManager.
        public PrintManagerHelper PrintManager = PrintManagerHelper.Instance;
        public bool IsExecutePrintReopen { get; private set; }
        public bool IsExecutePrintRequest { get; private set; }
        public string PrintManagerProgressString { get; set; }
        public bool IsTscPrinter { get; private set; }

        public ManagerFactoryMassa Massa { get; private set; } = new ManagerFactoryMassa();
        public ManagerFactoryMemory Memory { get; private set; } = new ManagerFactoryMemory();
        public ManagerFactoryPrint Print { get; private set; } = new ManagerFactoryPrint();

        #endregion

        #region Public and private methods

        public void Open(SqlViewModelEntity sqlViewModel, bool isTscPrinter, ScaleDirect currentScale)
        {
            try
            {
                IsTscPrinter = isTscPrinter;

                if (sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.PrintManager))
                {
                    
                    
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(null, ref ex, false);
            }
        }

        public void Close()
        {
            PrintManager.Close();
        }

        #endregion
    }
}
