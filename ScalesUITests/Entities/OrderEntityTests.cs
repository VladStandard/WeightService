//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ScalesUI.Common;
//using WeightCore.Db;

//namespace ScalesUITests.Entities
//{
//    [TestClass()]
//    public class OrderEntityTests
//    {
//        #region Private fields and properties

//        private Dictionary<string, object> _dict;
//        private OrderEntity _refOrder;

//        #endregion

//        #region Test methods

//        [TestCleanup]
//        public void Clean()
//        {
//            _refOrder = null;
//            _dict = null;
//        }

//        [TestInitialize]
//        public void Init()
//        {
//            _dict = new Dictionary<string, object>();
//            var plu = PLUHelper.GetPLU("F1A90176-894E-11EA-9E4C-4CCC6A93A440", 101);

//            _dict.Add("Id", 26);
//            _dict.Add("OrderType", 0);
//            _dict.Add("RRefID", "D7051D4B-94FC-11EA-9E4C-4CCC6A93A440");
//            _dict.Add("PLU", plu);
//            _dict.Add("PlaneBoxCount", 1);
//            _dict.Add("FactBoxCount", 0);
//            _dict.Add("PlanePalletCount", 1);
//            _dict.Add("PlanePackingOperationBeginDate", DateTime.Parse("2020-05-13 00:00:00.000"));
//            _dict.Add("PlanePackingOperationEndDate", DateTime.Parse("2020-05-13 11:59:59.000"));

//            _dict.Add("ScaleID", "F1A90176-894E-11EA-9E4C-4CCC6A93A440");
//            _dict.Add("TemplateID", "A72B1386-8B5B-47CA-B604-1405C7A1E0EC");
//            _dict.Add("ProductDate", DateTime.Parse("2020-05-13 12:00:00.000"));

//            _dict.Add("CreateDate", DateTime.Parse("2020-05-18 12:00:00.000"));
//            _dict.Add("Status", OrderStatus.New);

//            _refOrder = OrderEntity.ObjectFromDictionary<OrderEntity>(_dict);
//        }

//        #endregion

//        #region Public methods

//        [TestMethod()]
//        public void EqualsTest()
//        {
//            List<OrderEntity> orderList = OrderHelper.GetOrderList(_dict["ScaleID"].ToString(),
//                    ((DateTime)_dict["CreateDate"]).AddDays(-2),
//                    ((DateTime)_dict["CreateDate"]).AddDays(+2)
//            );

//            foreach (OrderEntity o in orderList)
//            {
//                if (_refOrder.Equals(o))
//                {
//                    Assert.IsTrue(true);
//                    return;
//                }
//            }
//            Assert.Fail();
//        }

//        [TestMethod()]
//        public void ToStringTest()
//        {

//            StringBuilder sb = new StringBuilder();
//            sb.Append($"({_dict["ProductDate"]})");
//            sb.Append($"{_dict["PLU"]}");
//            Assert.AreEqual(_refOrder.ToString().Length, sb.ToString().Length);
//            Assert.AreEqual(_refOrder.ToString(), sb.ToString());

//        }

//        [TestMethod()]
//        public void SerializeObjectTest()
//        {
//            string ds = _refOrder.SerializeObject();
//            Assert.IsNotNull(ds);
//        }

//        [TestMethod()]
//        public void GetStatusTest()
//        {
//            OrderStatus st = OrderHelper.GetStatus(_refOrder);
//            Assert.AreEqual(st, (OrderStatus)_dict["Status"]);
//        }

//        [TestMethod()]
//        public void SetStatusTest()
//        {
//            OrderHelper.SetStatus(_refOrder, OrderStatus.InProgress);
//            Assert.AreEqual(OrderHelper.GetStatus(_refOrder), OrderStatus.InProgress);
//            Thread.Sleep(1250);

//            OrderHelper.SetStatus(_refOrder, OrderStatus.Paused);
//            Assert.AreEqual(OrderHelper.GetStatus(_refOrder), OrderStatus.Paused);
//            Thread.Sleep(1250);

//            OrderHelper.SetStatus(_refOrder, OrderStatus.Performed);
//            Assert.AreEqual(OrderHelper.GetStatus(_refOrder), OrderStatus.Performed);
//            Thread.Sleep(1250);

//            OrderHelper.SetStatus(_refOrder, OrderStatus.Canceled);
//            Assert.AreEqual(OrderHelper.GetStatus(_refOrder), OrderStatus.Canceled);
//            Thread.Sleep(1250);

//            OrderHelper.SetStatus(_refOrder, OrderStatus.New);
//            Assert.AreEqual(OrderHelper.GetStatus(_refOrder), OrderStatus.New);
//            Thread.Sleep(1250);
//        }

//        #endregion
//    }
//}