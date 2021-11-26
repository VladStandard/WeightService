//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataProjectsCore;
//using DataProjectsCore.DAL;
//using DataProjectsCore.DAL.TableModels;

//namespace WeightCore.Managers
//{
//    public class ManagerFactory<T> where T : ManagerBase
//    {
//        #region Public and private methods

//        public void Open(T item, ProjectsEnums.TaskType taskType, SqlViewModelEntity sqlViewModel, ScaleDirect currentScale,
//            ManagerBase.InitCallback initCallback, ManagerBase.OpenCallback openCallback)
//        {
//            if (typeof(T) == typeof(ManagerFactoryMassa))
//            {
//                ((ManagerFactoryMassa)(object)item).Open(taskType, sqlViewModel, currentScale, initCallback, openCallback);
//            }
//            else if (typeof(T) == typeof(ManagerFactoryMemory))
//            {
//                ((ManagerFactoryMemory)(object)item).Open(taskType, sqlViewModel, currentScale, initCallback, openCallback);
//            }
//            else if (typeof(T) == typeof(ManagerFactoryPrint))
//            {
//                ((ManagerFactoryPrint)(object)item).Open(taskType, sqlViewModel, currentScale, initCallback, openCallback);
//            }
//        }

//        #endregion
//    }
//}
