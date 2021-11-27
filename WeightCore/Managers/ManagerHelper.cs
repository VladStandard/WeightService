// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL;
using DataProjectsCore.DAL.TableModels;
using System.Threading;
using WeightCore.Helpers;

namespace WeightCore.Managers
{
    public class ManagerHelper
    {
        #region Design pattern "Lazy Singleton"

        private static ManagerHelper _instance;
        public static ManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public ManagerFactoryMassa Massa { get; private set; } = new ManagerFactoryMassa();
        public ManagerFactoryMemory Memory { get; private set; } = new ManagerFactoryMemory();
        public ManagerFactoryPrint Print { get; private set; } = new ManagerFactoryPrint();
        public ExceptionHelper Exception { get; set; } = ExceptionHelper.Instance;

        public string MassaManagerProgressString { get; set; }
        public string MassaQueriesProgressString { get; set; }
        public string MassaRequestProgressString { get; set; }
        public string MassaResponseProgressString { get; set; }

        #endregion

        #region Constructor and destructor

        ~ManagerHelper()
        {
            Close();
        }

        #endregion

        #region Public and private methods

        public void Init(ScaleDirect currentScale, bool isTscPrinter)
        {
            Massa.Init(currentScale);
            Memory.Init();
            Print.Init(isTscPrinter, currentScale.ZebraPrinter.Name, currentScale.ZebraPrinter.Ip, currentScale.ZebraPrinter.Port);
        }

        public void Open(SqlViewModelEntity sqlViewModel, ScaleDirect currentScale)
        {
            Massa.Open(sqlViewModel);
            Memory.Open(sqlViewModel);
            Print.Open(sqlViewModel);
        }

        public void Close()
        {
            Massa.Close();
            Memory.Close();
            Print.Close();
        }

        #endregion
    }
}
