//using System.Diagnostics;
//using System.Windows.Forms;
//using NUnit.Framework;
//using ScalesUI.Common;
//using ScalesUI.Helpers;

//namespace WsScalesUITests.Helpers
//{
//    internal class ThreadHelperTests
//    {
//        #region Private fields and properties

//        /// <summary>
//        /// Помощник потоков.
//        /// </summary>
//        private ThreadHelper _thread { get; set; } = ThreadHelper.Instance;
//        /// <summary>
//        /// Помощник состояния устройства.
//        /// </summary>
//        private DeviceStatus _sessionState { get; set; } = DeviceStatus.Instance;

//        #endregion

//        #region Поток мониторинга даты времени

//        [Test]
//        public void UpdatedResourcesDt_Execute_DoesNotThrow()
//        {
//            Assert.DoesNotThrow(() => _thread.UpdatedResourcesDt(new Label()));
//        }

//        #endregion

//        #region Поток мониторинга COM-порта

//        [Test]
//        public void UpdatedResourcesComPort_Execute_DoesNotThrow()
//        {
//            Assert.DoesNotThrow(() => _thread.UpdatedResourcesComPort(_sessionState, new Label(), new Button(), new Button(), new Button(), new Button()));
//        }

//        #endregion

//        #region Поток мониторинга весов

//        [Test]
//        public void LoadResourcesScale_Execute_DoesNotThrow()
//        {
//            Assert.DoesNotThrow(() => _thread.LoadResourcesScale(new Label(), new Label(), new Label(), new Label(), new Label(), new Button(), new PictureBox()));
//        }

//        [Test]
//        public void UnloadResourcesScale_Execute_DoesNotThrow()
//        {
//            Assert.DoesNotThrow(() => _thread.UnloadResourcesScale(new Label(), new Label(), new Label(), new Label(), new Label(), new Button(), new PictureBox()));
//        }

//        [Test]
//        public void UpdatedResourcesScale_Execute_DoesNotThrow()
//        {
//            Assert.DoesNotThrow(() => _thread.UpdatedResourcesScale(_sessionState, new Label(), new Label(), new Label(), new Label(), new Label(), new Button()));
//        }

//        #endregion
//    }
//}
